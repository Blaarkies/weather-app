using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using WeatherApp.Domain;
using WeatherApp.Domain.OpenWeather;

namespace WeatherApp.Services.OpenWeather
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly ILogger<OpenWeatherService> _logger;
        private readonly HttpClient _client;
        private readonly OpenWeatherSettings _openWeatherSettings;

        public OpenWeatherService(
            ILogger<OpenWeatherService> logger,
            HttpClient client,
            OpenWeatherSettings openWeatherSettings)
        {
            _logger = logger;
            _client = client;
            _openWeatherSettings = openWeatherSettings;

            client.BaseAddress = new Uri($"{openWeatherSettings.Url}/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void WriteMessage(string message)
        {
            _logger.LogInformation($"MyDependency2.WriteMessage Message: {message}");
        }

        public async Task<OpenWeatherResponse> Get5DayForecast(string city, CancellationToken cancellationToken)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("q", city);
            queryString.Add("appid", _openWeatherSettings.ServiceApiKey);
            var query = $"forecast?{queryString}";

            var response = await _client.GetAsync(query, cancellationToken);
            var result = await response.Content.ReadAsAsync<OpenWeatherResponse>(cancellationToken);

            return result;
        }
    }
}
