using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherClient.ConsoleApp.Models
{
    public class IndiaCity
    {
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public decimal Lat { get; set; }

        [JsonProperty(PropertyName = "lng")]
        public decimal Lng { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "iso2")]
        public string Iso2 { get; set; }

        [JsonProperty(PropertyName = "admin_name")]
        public string AdminName { get; set; }

        [JsonProperty(PropertyName = "capital")]
        public string Capital { get; set; }

        [JsonProperty(PropertyName = "population")]
        public string Population { get; set; }

        [JsonProperty(PropertyName = "population_proper")]
        public string PopulationProper { get; set; }
    }
}
