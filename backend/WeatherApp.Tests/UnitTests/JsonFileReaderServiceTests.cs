using System.Threading.Tasks;
using NUnit.Framework;
using WeatherApp.Services.JsonJsonFileReader;

namespace WeatherApp.Tests.UnitTests
{
    public class JsonFileReaderServiceTests
    {
        private static JsonFileReaderService CreateService()
        {
            return new JsonFileReaderService();
        }

        [Test]
        public async Task Read_TestFile_ReturnsContent()
        {
            var service = CreateService();

            var results = await service.Read("Assets/test-file.json");

            Assert.IsTrue(results.Contains("test-key")
                          && results.Contains("test-value"));
        }
    }
}
