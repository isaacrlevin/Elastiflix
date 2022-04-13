using Microsoft.Extensions.Options;
using Shared;
using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace TMDBImport
{
    public class Runner
    {
        private readonly Settings _settings;
        private readonly HttpClient _httpClient;
        public Runner(IOptionsMonitor<Settings> optionsAccessor, HttpClient client)
        {
            _settings = optionsAccessor.CurrentValue;
            _httpClient = client;

            _httpClient.BaseAddress = new Uri(_settings.ElasticTenantUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
        }

        public async Task<ConcurrentBag<Movie>> GetMovies()
        {

            TMDbClient tmdbClient = new TMDbClient(_settings.TMDBToken);


            ConcurrentBag<Movie> movies = new ConcurrentBag<Movie>();

            // This process will build a list of integers representing a Movie ID in The Internet Movie Database
            // Not all IDs will return a movie so there is a blanket catch if there is an API error. You can do better
            // Specify the amount of movies you want to collect
            var moviesToDownload = 5;
            var groupByAmount = 10;

            // Build list of integers based on variable than chunk into lists of the same size
            var parallelGroups = Enumerable.Range(0, moviesToDownload)
                                           .GroupBy(r => (r % groupByAmount));


            // Build collection of Tasks to run in parallel
            // Each task will call TMDB API and add API reponse to collection of movies in memory
            var parallelTasks = parallelGroups.Select(groups =>
            {
                return Task.Run(async () =>
                {
                    foreach (var i in groups)
                    {
                        try
                        {
                            var movie = await tmdbClient.GetMovieAsync(i, MovieMethods.Credits);
                            movies.Add(movie);
                            Console.WriteLine($"Added: {movie.Title}");
                        }
                        catch { }
                    }
                });
            });

            // Await until all tasks are complete
            await Task.WhenAll(parallelTasks);
            return movies;
        }

        public async Task AddMovies(ConcurrentBag<Movie> movies)
        {
            JsonSerializerOptions options = new() // setting serialization configuration
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = new LowerCaseNamingPolicy() //make all serialized json properties lower case
            };


            // Take list of Movies and chunk into lists of 100 to post to API. 
            // The Elastic API has a limit of 100 documents to index at once
            var chunks = movies
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 100)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();

            foreach (var chunk in chunks)
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{_settings.ElasticEngineName}/documents");
                // get API Key from Elastic Cloud to have authenticated request
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ElasticEngineToken);
                // serialize C# object to string for post to API
                string jsonString = JsonSerializer.Serialize(chunk, options);
                request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                await _httpClient.SendAsync(request);
            }
        }
    }
}
