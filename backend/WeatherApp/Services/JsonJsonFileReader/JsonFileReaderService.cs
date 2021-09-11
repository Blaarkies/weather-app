using System.IO;
using System.Threading.Tasks;
using WeatherApp.Domain;

namespace WeatherApp.Services.JsonJsonFileReader
{
    public class JsonFileReaderService : IJsonFileReaderService
    {
        public async Task<string> Read(string path)
        {
            using var reader = new StreamReader(path);
            var json = await reader.ReadToEndAsync();
            return json;
        }
    }
}
