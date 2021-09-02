using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherApp.Services.OpenWeather;
using WeatherApp.Services.Serializer;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IOpenWeatherService _openWeatherService;
        private readonly ISerializerService _serializerService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IOpenWeatherService openWeatherService,
            ISerializerService serializerService)
        {
            _logger = logger;
            _openWeatherService = openWeatherService;
            _serializerService = serializerService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> Get(String city, CancellationToken cancellationToken)
        {
            var response = await _openWeatherService.Get5DayForecast(city, cancellationToken);
            var weatherSummary = new WeatherPayload(response);
            return new OkObjectResult(_serializerService.Serialize(weatherSummary));
        }
    }
}
