using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApp.Services.GeoData
{
    public interface IGeoDataService
    {
        Task<IEnumerable<string>> QueryCitiesForName(string search);
        Task<IEnumerable<string>> AllCities();
    }
}
