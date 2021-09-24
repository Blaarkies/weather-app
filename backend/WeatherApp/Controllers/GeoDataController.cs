using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Attributes;
using WeatherApp.Services.GeoData;

namespace WeatherApp.Controllers
{

    /// <summary>
    /// Handles geographical data requests, like city names.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GeoDataController : ControllerBase
    {
        private readonly IGeoDataService _geoDataService;

        public GeoDataController(IGeoDataService geoDataService)
        {
            _geoDataService = geoDataService;
        }

        /// <summary>
        /// Return a list of city names that partially/fully matches the [search] argument, case-insensitive.
        /// </summary>
        [HttpGet("cities-by-name/{search}")]
        public async Task<IActionResult> GetCitiesByName(string search, CancellationToken cancellationToken)
        {
            var cities = await _geoDataService.QueryCitiesForName(search, cancellationToken);

            return Ok(cities);
        }

        /// <summary>
        /// Returns a list of all city names, limited by [PageSize].
        /// </summary>
        [HttpGet("cities")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var cities = await _geoDataService.GetAllCities(cancellationToken);

            return Ok(cities);
        }
    }
}
