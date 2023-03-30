using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherClient.ConsoleApp.Models;

namespace WeatherClient.ConsoleApp.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherInfo> GetWeatherAsync(decimal latitude, decimal longitude);
    }
}