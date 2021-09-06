using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherApp.Services.OpenWeather;
using WeatherApp.Services.Serializer;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IOpenWeatherService _openWeatherService;
        private readonly ISerializerService _serializerService;

        public WeatherController(
            ILogger<WeatherController> logger,
            IOpenWeatherService openWeatherService,
            ISerializerService serializerService)
        {
            _logger = logger;
            _openWeatherService = openWeatherService;
            _serializerService = serializerService;
        }

        [HttpGet("forecast")]
        public async Task<IActionResult> GetForecast(string city, string zipCode, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _openWeatherService.Get5DayForecast(city, zipCode, cancellationToken);
                var weatherSummary = new WeatherPayload(response);
                return new OkObjectResult(_serializerService.Serialize(weatherSummary));
            }
            catch (Exception e)
            {
                return new ContentResult
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Content = e.Message,
                };
            }
        }
    }
}
