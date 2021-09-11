using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Domain.Payloads;
using WeatherApp.Services.OpenWeather;
using WeatherApp.Services.Serializer;

namespace WeatherApp.Controllers
{
    /// <summary>
    /// Handles requests related to weather forecasts for a particular location.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IOpenWeatherService _openWeatherService;
        private readonly ISerializerService _serializerService;

        public WeatherController(
            IOpenWeatherService openWeatherService,
            ISerializerService serializerService)
        {
            _openWeatherService = openWeatherService;
            _serializerService = serializerService;
        }

        /// <summary>
        /// Returns a 5 day forecast for the matching [city] or [zipcode]. Upstream exceptions are transformed
        /// into HttpContentResult messages for frontends to display.
        /// </summary>
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
