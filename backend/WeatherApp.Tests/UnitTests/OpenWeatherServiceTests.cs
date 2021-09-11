using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using WeatherApp.Domain;
using WeatherApp.Services.OpenWeather;

namespace WeatherApp.Tests.UnitTests
{
    public class OpenWeatherServiceTests
    {
        private MockHttpMessageHandler _mockHttp;
        private HttpClient _mockHttpClient;

        [SetUp]
        public void Setup()
        {
            _mockHttp = new MockHttpMessageHandler();
            _mockHttp
                .When("*")
                .Respond("application/json", "{'test-error' : 'no-content'}");
            _mockHttpClient = new HttpClient(_mockHttp);
        }

        private OpenWeatherService CreateService()
        {
            var mockLogger = Mock.Of<ILogger<OpenWeatherService>>();

            var mockOpenWeatherSettings = Options.Create(new OpenWeatherSettings
            {
                Url = "https://a.a.org",
                ServiceApiKey = "apikey",
            });

            return new OpenWeatherService(
                mockLogger,
                _mockHttpClient,
                mockOpenWeatherSettings);
        }

        [Test]
        public async Task Get5DayForecast_ByCity_ReturnsResults()
        {
            var service = CreateService();

            var result = await service.Get5DayForecast("test-city", null, CancellationToken.None);

            Assert.NotNull(result);
            _mockHttp.Expect(HttpMethod.Get, "");
        }

        [Test]
        public async Task Get5DayForecast_ByZipCode_ReturnsResults()
        {
            var service = CreateService();

            var result = await service.Get5DayForecast(null, "90001", CancellationToken.None);

            Assert.NotNull(result);
            _mockHttp.Expect(HttpMethod.Get, "");
        }
    }
}
