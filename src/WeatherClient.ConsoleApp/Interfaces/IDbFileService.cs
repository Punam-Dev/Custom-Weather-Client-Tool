using System;
using System.Collections.Generic;
using System.Text;
using WeatherClient.ConsoleApp.Models;

namespace WeatherClient.ConsoleApp.Interfaces
{
    public interface IDbFileService
    {
        IndiaCity GetCityInfo(string cityName);
    }
}
