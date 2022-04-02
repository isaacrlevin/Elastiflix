using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using TMDBImport;



IServiceCollection services = new ServiceCollection();

// Configuration Section
var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


var Configuration = builder.Build();
services.Configure<Settings>(Configuration);
services.AddSingleton<Runner, Runner>();
services.AddHttpClient();


var ServiceProvider = services.BuildServiceProvider();
var runner = ServiceProvider.GetRequiredService<Runner>();


var movies = await runner.GetMovies();
await runner.AddMovies(movies);