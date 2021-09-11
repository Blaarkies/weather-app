using System.Threading.Tasks;

namespace WeatherApp.Services.JsonJsonFileReader
{
    /// <summary>
    /// Provides file reads into string json format.
    /// </summary>
    public interface IJsonFileReaderService
    {
        /// <summary>
        /// Reads a file at the given [path] and returns the contents in string.
        /// </summary>
        /// <param name="path">Path to file</param>
        Task<string> Read(string path);
    }
}
