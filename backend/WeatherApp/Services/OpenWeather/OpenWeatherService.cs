using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using WeatherApp.Domain;
using WeatherApp.Domain.OpenWeather;

namespace WeatherApp.Services.OpenWeather
{
    /// <inheritdoc/>
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly ILogger<OpenWeatherService> _logger;
        private readonly HttpClient _client;
        private readonly OpenWeatherSettings _openWeatherSettings;

        public OpenWeatherService(
            ILogger<OpenWeatherService> logger,
            HttpClient client,
            IOptions<OpenWeatherSettings> openWeatherSettings)
        {
            _logger = logger;
            _client = client;
            _openWeatherSettings = openWeatherSettings.Value;

            client.BaseAddress = new Uri($"{_openWeatherSettings.Url}/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
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
        public async Task<OpenWeatherResponse> Get5DayForecast(
            string city,
            string zipCode,
            CancellationToken cancellationToken)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            if (city != null)
            {
                queryString.Add("q", city);
            }

            if (zipCode != null)
            {
                queryString.Add("zip", zipCode);
            }

            queryString.Add("appid", _openWeatherSettings.ServiceApiKey);
            var query = $"forecast?{queryString}";

            var response = await _client.GetAsync(query, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var jsonPayload = await response.Content.ReadAsStringAsync(cancellationToken);
                var errorMessage = JObject.Parse(jsonPayload)["message"].ToString();

                WriteMessage($"Could not get forecast for city:[{city}] zipcode:[{zipCode}]. " + errorMessage);

                throw new ArgumentException(errorMessage);
            }

            var result = await response.Content.ReadAsAsync<OpenWeatherResponse>(cancellationToken);

            return result;
        }
    }
}
