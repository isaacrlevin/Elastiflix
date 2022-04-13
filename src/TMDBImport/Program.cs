using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using TMDBImport;



// Wireup Dependency Injection and typed settings for console app
IServiceCollection services = new ServiceCollection();

var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


var Configuration = builder.Build();
services.Configure<Settings>(Configuration);
services.AddSingleton<Runner, Runner>();
services.AddHttpClient();

var ServiceProvider = services.BuildServiceProvider();
var runner = ServiceProvider.GetRequiredService<Runner>();



// Get collection of movies
var movies = await runner.GetMovies();


// Run Indexing Process
await runner.AddMovies(movies);