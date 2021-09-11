using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services.GeoData;
using WeatherApp.Services.Serializer;

namespace WeatherApp.Controllers
{

    /// <summary>
    /// Handles geographical data requests, like city names.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GeoDataController : ControllerBase
    {
        private readonly IGeoDataService _geoDataService;
        private readonly ISerializerService _serializerService;

        public GeoDataController(
            IGeoDataService geoDataService,
            ISerializerService serializerService)
        {
            _geoDataService = geoDataService;
            _serializerService = serializerService;
        }

        /// <summary>
        /// Return a list of city names that partially/fully matches the [search] argument, case-insensitive.
        /// </summary>
        [HttpGet("cities-by-name/{search}")]
        public async Task<IActionResult> GetCitiesByName(string search, CancellationToken cancellationToken)
        {
            var cities = await _geoDataService.QueryCitiesForName(search, cancellationToken);

            return new OkObjectResult(_serializerService.Serialize(cities));
        }

        /// <summary>
        /// Returns a list of all city names, limited by [PageSize].
        /// </summary>
        [HttpGet("cities")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var cities = await _geoDataService.GetAllCities(cancellationToken);
            return new OkObjectResult(_serializerService.Serialize(cities));
        }
    }
}
