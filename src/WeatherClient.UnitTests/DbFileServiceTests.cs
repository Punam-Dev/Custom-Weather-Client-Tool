using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WeatherClient.ConsoleApp.Interfaces;
using WeatherClient.ConsoleApp.Models;
using WeatherClient.ConsoleApp.Services;
using Xunit;

namespace WeatherClient.UnitTests
{
    public class DbFileServiceTests
    {
        Mock<IFileManager> _mockFileManager = new Mock<IFileManager>();

        private readonly IDbFileService _dbFileService;
        public DbFileServiceTests()
        {
            _dbFileService = new DbFileService(_mockFileManager.Object);
        }

        [Fact]
        public void GetCityInfo_Should_Return_CityInfo_When_CityName_Is_Present_In_LocalJsonFile()
        {
            var expectedCity = new IndiaCity
            {
                City = "Kolkata",
                Lat = 28.6600M,
                Lng = 77.2300M,
                Country = "India",
                Iso2 = "IN",
                AdminName = "Delhi",
                Capital = "admin",
                Population = "29617000",
                PopulationProper = "16753235"
            };

            var cities = new List<IndiaCity>
            {
                new IndiaCity
                {
                    City = "Kolkata",
                    Lat = 28.6600M,
                    Lng = 77.2300M,
                    Country = "India",
                    Iso2 = "IN",
                    AdminName = "Delhi",
                    Capital = "admin",
                    Population = "29617000",
                    PopulationProper = "16753235"
                },
                new IndiaCity
                {
                    City = "Kolkata1",
                    Lat = 28.6600M,
                    Lng = 77.2300M,
                    Country = "India",
                    Iso2 = "IN",
                    AdminName = "Delhi",
                    Capital = "admin",
                    Population = "29617000",
                    PopulationProper = "16753235"
                }
            };

            MemoryStream fakeMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cities)));

            _mockFileManager.Setup(fileManager => fileManager.StreamReader(It.IsAny<string>()))
                           .Returns(() => new StreamReader(fakeMemoryStream));

            var result = _dbFileService.GetCityInfo("Kolkata");

            result.Should().BeEquivalentTo(expectedCity);
        }

        [Fact]
        public void GetCityInfo_Should_Return_Null_When_CityName_IsNotPresent_In_LocalJsonFile()
        {
            var expectedCity = new IndiaCity
            {
                City = "Kolkata",
                Lat = 28.6600M,
                Lng = 77.2300M,
                Country = "India",
                Iso2 = "IN",
                AdminName = "Delhi",
                Capital = "admin",
                Population = "29617000",
                PopulationProper = "16753235"
            };

            var cities = new List<IndiaCity>
            {
                new IndiaCity
                {
                    City = "Kolkata",
                    Lat = 28.6600M,
                    Lng = 77.2300M,
                    Country = "India",
                    Iso2 = "IN",
                    AdminName = "Delhi",
                    Capital = "admin",
                    Population = "29617000",
                    PopulationProper = "16753235"
                },
                new IndiaCity
                {
                    City = "Kolkata1",
                    Lat = 28.6600M,
                    Lng = 77.2300M,
                    Country = "India",
                    Iso2 = "IN",
                    AdminName = "Delhi",
                    Capital = "admin",
                    Population = "29617000",
                    PopulationProper = "16753235"
                }
            };

            MemoryStream fakeMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cities)));

            _mockFileManager.Setup(fileManager => fileManager.StreamReader(It.IsAny<string>()))
                           .Returns(() => new StreamReader(fakeMemoryStream));
            var result = _dbFileService.GetCityInfo("kolkāta12");

            result.Should().BeNull();
        }
    }
}