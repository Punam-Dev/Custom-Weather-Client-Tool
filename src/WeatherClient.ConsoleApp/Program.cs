using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using WeatherClient.ConsoleApp.Interfaces;
using WeatherClient.ConsoleApp.Models;
using WeatherClient.ConsoleApp.Services;

namespace WeatherClient.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var builder = new ConfigurationBuilder();
                BuildConfig(builder);

                IConfiguration configuration = builder.Build();

                var serviceCollection = new ServiceCollection();

                serviceCollection.AddHttpClient(HttpClientName.GetWeather, client =>
                {
                    client.BaseAddress = new Uri(configuration["OpenMetroAPiBaseUrl"]);
                })
                .AddTransientHttpErrorPolicy(policyBuilder =>
                    policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 4)));

                serviceCollection.AddScoped<IFileManager, FileManager>();
                serviceCollection.AddScoped<IWeatherService, WeatherService>();
                serviceCollection.AddScoped<IDbFileService, DbFileService>();

                string cityName = GetCityName(true);

                var serviceProvider = serviceCollection.BuildServiceProvider();

                var dbFileService = serviceProvider.GetService<IDbFileService>();

                var x = string.Compare("kolkata", "kolkata", CultureInfo.InvariantCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) == 0;
                x = string.Compare("Kolkta", "kolkata", CultureInfo.InvariantCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) == 0;

                var city = dbFileService.GetCityInfo(cityName);

                while (city is null)
                {
                    Console.WriteLine("City name not found in local json file.");

                    cityName = GetCityName(false);

                    city = dbFileService.GetCityInfo(cityName);
                }

                var weatherService = serviceProvider.GetService<IWeatherService>();
                var weather = await weatherService.GetWeatherAsync(city.Lat, city.Lng);

                if (weather is null || weather.CurrentWeather is null)
                {
                    throw new Exception($"Weather not found for city: {cityName}");
                }

                Console.WriteLine($"Weather of {cityName} is below.");

                Console.WriteLine($"Temperature = {weather.CurrentWeather.Temperature}");
                Console.WriteLine($"WindSpeed = {weather.CurrentWeather.WindSpeed}");
                Console.WriteLine($"WindDirection = {weather.CurrentWeather.WindDirection}");

            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occured : " + ex.Message);
            }
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }

        static string GetCityName(bool isInitial)
        {
            if(!isInitial)
            {
                Console.WriteLine("");
            }
            Console.Write($"Enter{(isInitial ? "": " another")} city name: ");

            string cityName = string.Empty;

            cityName = Console.ReadLine();

            while (string.IsNullOrEmpty(cityName))
            {
                Console.Write("City name can't be empty. Please enter city name: ");

                cityName = Console.ReadLine();
            }

            return cityName;
        }
    }
}
