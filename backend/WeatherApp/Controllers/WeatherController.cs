using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Domain.Payloads;
using WeatherApp.Services.OpenWeather;

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

        public WeatherController(IOpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
        }

        /// <summary>
        /// Returns a 5 day forecast for the matching [city]. Upstream exceptions are transformed
        /// into HttpContentResult messages for frontends to display.
        /// </summary>
        [HttpGet("forecast/city")]
        public async Task<IActionResult> GetForecastByCity(string city, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _openWeatherService.Get5DayForecastForCity(city, cancellationToken);
                var weatherSummary = new WeatherPayload(response);
                return Ok(weatherSummary);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Returns a 5 day forecast for the matching [zipcode]. Upstream exceptions are transformed
        /// into HttpContentResult messages for frontends to display.
        /// </summary>
        [HttpGet("forecast/zip-code")]
        public async Task<IActionResult> GetForecastByZipCode(string zipCode, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _openWeatherService.Get5DayForecastForZipCode(zipCode, cancellationToken);
                var weatherSummary = new WeatherPayload(response);
                return Ok(weatherSummary);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
