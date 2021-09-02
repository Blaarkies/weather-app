using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherApp.Services.GeoData;
using WeatherApp.Services.OpenWeather;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeoDataController : ControllerBase
    {
        private readonly ILogger<GeoDataController> _logger;
        private readonly IGeoDataService _geoDataService;

        public GeoDataController(
            ILogger<GeoDataController> logger,
            IGeoDataService geoDataService)
        {
            _logger = logger;
            _geoDataService = geoDataService;
        }

        [HttpGet("cities-by-name/{search}")]
        public async Task<IActionResult> Get(String search, CancellationToken cancellationToken)
        {
            var response = _geoDataService.QueryCitiesForName(search);

            return new OkObjectResult(JsonConvert.SerializeObject(response));
        }
    }
}
