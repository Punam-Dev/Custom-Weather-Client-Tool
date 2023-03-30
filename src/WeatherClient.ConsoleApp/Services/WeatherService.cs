using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherClient.ConsoleApp.Interfaces;
using WeatherClient.ConsoleApp.Models;

namespace WeatherClient.ConsoleApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public WeatherService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<WeatherInfo> GetWeatherAsync(decimal latitude, decimal longitude)
        {
            var client = _httpClientFactory.CreateClient(HttpClientName.GetWeather);

            var response = await client.GetAsync($"v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true");

            response.EnsureSuccessStatusCode();

            var weather = JsonConvert.DeserializeObject<WeatherInfo>(await response.Content.ReadAsStringAsync());

            return weather;
        }
    }
}