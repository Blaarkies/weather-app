using System.IO;
using System.Threading.Tasks;

namespace WeatherApp.Services.JsonFileReader
{
    /// <inheritdoc/>
    public class JsonFileReaderService : IJsonFileReaderService
    {
        /// <inheritdoc/>
        public async Task<string> Read(string path)
        {
            using var reader = new StreamReader(path);
            var json = await reader.ReadToEndAsync();
            return json;
        }
    }
}
