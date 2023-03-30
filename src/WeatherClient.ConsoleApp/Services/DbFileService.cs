using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using WeatherClient.ConsoleApp.Interfaces;
using WeatherClient.ConsoleApp.Models;

namespace WeatherClient.ConsoleApp.Services
{
    public class DbFileService : IDbFileService
    {
        private readonly IFileManager _fileManager;
        List<IndiaCity> cities = new List<IndiaCity>();

        public DbFileService(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public IndiaCity GetCityInfo(string cityName)
        {
            if(cities.Count == 0)
            {
                using (StreamReader r = _fileManager.StreamReader("in.json"))
                {
                    string json = r.ReadToEnd();
                    cities = JsonConvert.DeserializeObject<List<IndiaCity>>(json);
                }
            }

            // The below commented code not working when cityName = kolkata and in json file it has Kolkāta. latin character(ā) is there.
            //approach-1:
            //return cities.Where(x => x.City.Equals(cityName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            //approach-2:
            //return cities.Where(x => x.City.Equals(cityName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            //approach-3:
            return cities.Where(x => string.Compare(x.City, cityName, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) == 0).FirstOrDefault();
        }
    }
}
