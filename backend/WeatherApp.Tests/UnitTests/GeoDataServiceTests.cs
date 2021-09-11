using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using WeatherApp.Domain;
using WeatherApp.Services.GeoData;
using WeatherApp.Services.JsonJsonFileReader;
using WeatherApp.Services.Settings;

namespace WeatherApp.Tests.UnitTests
{
    public class GeoDataServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        private const string CityNamesJson = "[\"Aach\",\"Aachen\"]";

        private static GeoDataService CreateService()
        {
            var mockLogger = Mock.Of<ILogger<GeoDataService>>();

            var mockSettings = Mock.Of<ISettingsService>();
            Mock.Get(mockSettings)
                .Setup(m => m.PageSize)
                .Returns(20);

            var mockHttpClient = Mock.Of<HttpClient>();

            var mockStreamReader = Mock.Of<StreamReader>();
            Mock.Get(mockStreamReader)
                .Setup(m => m.ReadToEndAsync())
                .Returns(Task.FromResult(CityNamesJson));

            var mockStreamReaderGenerator = Mock.Of<JsonFileReaderService>();
            Mock.Get(mockStreamReaderGenerator)
                .Setup(m => m.Read(It.IsAny<string>()))
                .Returns(mockStreamReader);

            return new GeoDataService(
                mockLogger,
                mockSettings,
                mockHttpClient,
                mockStreamReaderGenerator);
        }

        [TestCase("Bielefeld", 1, 1)]
        public async Task QueryCitiesForName_Search_ReturnsExpectedResults(
            string search,
            int minimumCount,
            int maximumCount)
        {
            var service = CreateService();

            var results = await service.QueryCitiesForName(search);
            var resultsList = results.ToList();

            Assert.GreaterOrEqual(resultsList.Count, minimumCount);
            Assert.LessOrEqual(resultsList.Count, minimumCount);
            Assert.IsTrue(resultsList.Any(c => c == search));
        }
    }
}
