using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherClient.ConsoleApp.Interfaces;
using WeatherClient.ConsoleApp.Models;
using WeatherClient.ConsoleApp.Services;
using Xunit;

namespace WeatherClient.UnitTests
{
    public class WeatherServiceTests
    {
        private readonly Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        private readonly IWeatherService _weatherService;
        public WeatherServiceTests()
        {
            _weatherService = new WeatherService(_mockHttpClientFactory.Object);
        }

        [Fact]
        public async Task GetWeather_Should_Return_WeatherInfo_When_ApiCallIsSuccessfull()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            var weatherInfo = new WeatherInfo
            {
                Latitude = 18.125M,
                Longitude = 73.0M,
                GenerationtimeMs = 4.784941673278809M,
                UtcOffsetSeconds = 0,
                Timezone = "GMT",
                TimezoneAbbreviation = "GMT",
                Elevation = 0.0M,
                CurrentWeather = new CurrentWeather
                {
                    Temperature = 29.0M,
                    WindSpeed = 16.6M,
                    WindDirection = 288,
                    WeatherCode = 1,
                    Time = "2023-03-30T11:00"
                }
            };

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(r =>
                    r.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(weatherInfo))
                }));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://testapi.open-meteo.com/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var result = await _weatherService.GetWeatherAsync(18, 72);

            result.Should().NotBeNull();
            result.CurrentWeather.Should().NotBeNull();
        }

        [Fact]
        public async Task GetWeather_Should_Throw_Error_When_ApiCallIsUnSuccessfull()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(r =>
                    r.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Content = new StringContent("")
                }));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://testapi.open-meteo.com/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            Func<Task> act = async () => await _weatherService.GetWeatherAsync(18, 72);

            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task GetWeather_Should_Return_Null_When_FailedToParseResponse()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(r =>
                    r.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("")
                }));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("https://testapi.open-meteo.com/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var result = await _weatherService.GetWeatherAsync(18, 72);

            result.Should().BeNull();
        }
    }
}