using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherApp.Services.GeoData
{
    public interface IGeoDataService
    {
        /// <summary>
        /// Returns a filtered list of city names possibly matching the [search] string, limited to PageSize.
        /// </summary>
        /// <param name="search">name of city</param>
        /// <param name="cancellationToken"></param>
        Task<IEnumerable<string>> QueryCitiesForName(string search, CancellationToken cancellationToken);

        /// <summary>
        /// Returns a list of all city names, limited to PageSize.
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task<IEnumerable<string>> GetAllCities(CancellationToken cancellationToken);
    }
}
