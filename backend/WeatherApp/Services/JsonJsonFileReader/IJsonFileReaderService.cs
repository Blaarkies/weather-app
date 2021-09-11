using System.Threading.Tasks;

namespace WeatherApp.Services.JsonJsonFileReader
{
    public interface IJsonFileReaderService
    {
        Task<string> Read(string path);
    }
}
