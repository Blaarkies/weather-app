using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using WeatherApp.Domain;
using WeatherApp.Services.OpenWeather;

namespace WeatherApp.Tests.UnitTests
{
    public class OpenWeatherServiceTests
    {

        [SetUp]
        public void Setup()
        {
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
                mockOpenWeatherSettings);
        }

        [Test]
        public async Task Get5DayForecast_ByCity_ReturnsResults()
        {
            var service = CreateService();

            var result = await service.Get5DayForecastForCity("test-city", CancellationToken.None);

            Assert.NotNull(result);
        }

        [Test]
        public async Task Get5DayForecast_ByZipCode_ReturnsResults()
        {
            var service = CreateService();

            var result = await service.Get5DayForecastForZipCode("90001", CancellationToken.None);

            Assert.NotNull(result);
        }
    }
}
