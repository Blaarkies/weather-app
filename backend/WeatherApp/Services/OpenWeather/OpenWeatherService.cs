using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeatherApp.Domain;
using WeatherApp.Domain.Dtos.OpenWeather;

namespace WeatherApp.Services.OpenWeather
{
    /// <inheritdoc/>
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly ILogger<OpenWeatherService> _logger;
        private readonly OpenWeatherSettings _openWeatherSettings;

        public OpenWeatherService(
            ILogger<OpenWeatherService> logger,
            IOptions<OpenWeatherSettings> openWeatherSettings)
        {
            _logger = logger;
            _openWeatherSettings = openWeatherSettings.Value;
        }

        /// <summary>
        /// Logs information relevant to the service's behaviour.
        /// </summary>
        private void WriteMessage(string message, string stack = null)
        {
            _logger.LogInformation($"OpenWeatherService: {message} \n {stack}");
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">When OpenWeatherApi responds with a fail status code</exception>
        public async Task<OpenWeatherResponse> Get5DayForecastForCity(string city, CancellationToken cancellationToken)
        {
            try
            {
                return await GetOpenWeatherForecastQuery()
                    .SetQueryParams(new { q = city })
                    .GetJsonAsync<OpenWeatherResponse>(cancellationToken);
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync<OpenWeatherMessage>();
                var errorMessage = error.Message;

                WriteMessage($"Could not get forecast for city:[{city}]. " + errorMessage);

                throw new ArgumentException(errorMessage);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">When OpenWeatherApi responds with a fail status code</exception>
        public async Task<OpenWeatherResponse> Get5DayForecastForZipCode(
            string zipCode,
            CancellationToken cancellationToken)
        {
            try
            {
                return await GetOpenWeatherForecastQuery()
                    .SetQueryParams(new { zip = $"{zipCode},DE" })
                    .GetJsonAsync<OpenWeatherResponse>(cancellationToken);
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync<OpenWeatherMessage>();
                var errorMessage = error.Message;

                WriteMessage($"Could not get forecast for zip code:[{zipCode}]. " + errorMessage);

                throw new ArgumentException(errorMessage);
            }
        }

        private Url GetOpenWeatherForecastQuery()
        {
            return _openWeatherSettings.Url
                .AppendPathSegment("forecast")
                .SetQueryParams(new { appid = _openWeatherSettings.ServiceApiKey });
        }
    }
}
