using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherApp.Services.GeoData;
using WeatherApp.Services.Serializer;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeoDataController : ControllerBase
    {
        private readonly ILogger<GeoDataController> _logger;
        private readonly IGeoDataService _geoDataService;
        private readonly ISerializerService _serializerService;

        public GeoDataController(
            ILogger<GeoDataController> logger,
            IGeoDataService geoDataService,
            ISerializerService serializerService)
        {
            _logger = logger;
            _geoDataService = geoDataService;
            _serializerService = serializerService;
        }

        [HttpGet("cities-by-name/{search}")]
        public async Task<IActionResult> Get(string search, CancellationToken cancellationToken)
        {
            var cities = await _geoDataService.QueryCitiesForName(search);

            return new OkObjectResult(_serializerService.Serialize(cities));
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var cities = await _geoDataService.GetAllCities();
            return new OkObjectResult(_serializerService.Serialize(cities));
        }
    }
}
