using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherClient.ConsoleApp.Models
{
    public class WeatherInfo
    {
        [JsonProperty(PropertyName = "latitude")]
        public decimal Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public decimal Longitude { get; set; }

        [JsonProperty(PropertyName = "generationtime_ms")]
        public decimal GenerationtimeMs { get; set; }

        [JsonProperty(PropertyName = "utc_offset_seconds")]
        public int UtcOffsetSeconds { get; set; }

        [JsonProperty(PropertyName = "timezone")]
        public string Timezone { get; set; }

        [JsonProperty(PropertyName = "timezone_abbreviation")]
        public string TimezoneAbbreviation { get; set; }

        [JsonProperty(PropertyName = "elevation")]
        public decimal Elevation { get; set; }

        [JsonProperty(PropertyName = "current_weather")]
        public CurrentWeather CurrentWeather { get; set; } = new CurrentWeather();
    }
    public class CurrentWeather
    {
        [JsonProperty(PropertyName = "temperature")]
        public decimal Temperature { get; set; }

        [JsonProperty(PropertyName = "windspeed")]
        public decimal WindSpeed { get; set; }

        [JsonProperty(PropertyName = "winddirection")]
        public decimal WindDirection { get; set; }

        [JsonProperty(PropertyName = "weathercode")]
        public int WeatherCode { get; set; }

        [JsonProperty(PropertyName = "time")]
        public string Time { get; set; }
    }
}
