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
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IOpenWeatherService _openWeatherService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IOpenWeatherService openWeatherService,
            IGeoDataService data)
        {
            _logger = logger;
            _openWeatherService = openWeatherService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> Get(String city, CancellationToken cancellationToken)
        {
            var response = await _openWeatherService.Get5DayForecast(city, cancellationToken);
            return new OkObjectResult(JsonConvert.SerializeObject(response));
        }
    }
}
