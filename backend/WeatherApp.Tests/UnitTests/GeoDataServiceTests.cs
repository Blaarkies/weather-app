using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using WeatherApp.Services.GeoData;
using WeatherApp.Services.JsonJsonFileReader;
using WeatherApp.Services.Settings;

namespace WeatherApp.Tests.UnitTests
{
    public class GeoDataServiceTests
    {
        private const string CityNamesJson = "[\"Aach\",\"Stuttgart\",\"Bremen\",\"Essen\"]";

        private static GeoDataService CreateService()
        {
            var mockLogger = Mock.Of<ILogger<GeoDataService>>();

            var mockSettings = Mock.Of<ISettingsService>();
            Mock.Get(mockSettings)
                .Setup(m => m.PageSize)
                .Returns(20);

            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .When("*")
                .Respond("application/json", "{'test-error' : 'no-content'}");
            var mockHttpClient = new HttpClient(mockHttp);

            var mockJsonFileReaderService = Mock.Of<IJsonFileReaderService>();
            Mock.Get(mockJsonFileReaderService)
                .Setup(m => m.Read(It.IsAny<string>()))
                .ReturnsAsync(CityNamesJson);

            return new GeoDataService(
                mockLogger,
                mockSettings,
                mockHttpClient,
                mockJsonFileReaderService);
        }

        [TestCase("Bielefeld", 0, 0)]
        [TestCase("Stuttgart", 1, 1)]
        [TestCase("e", 2, 4)]
        [TestCase("eme", 1, 1)]
        public async Task QueryCitiesForName_Search_ReturnsExpectedResults(
            string search,
            int minimumCount,
            int maximumCount)
        {
            var service = CreateService();
            var shouldExist = maximumCount > 0;

            var results = await service.QueryCitiesForName(search);
            var resultsList = results.ToList();

            Assert.GreaterOrEqual(resultsList.Count, minimumCount);
            Assert.LessOrEqual(resultsList.Count, minimumCount);

            var isSearchStringInCityList = !shouldExist
                                           || resultsList.Any(c => c.ToLower().Contains(search.ToLower()));
            Assert.IsTrue(isSearchStringInCityList);
        }

        [Test]
        public async Task GetAllCities_ReturnsCityNamesJson()
        {
            var service = CreateService();

            var results = await service.GetAllCities();
            var resultsList = results.ToList();
            var areAllCitiesInList = resultsList.All(c => CityNamesJson.Contains(c));

            Assert.IsTrue(areAllCitiesInList);
        }
    }
}
