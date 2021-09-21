using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Domain.OpenWeather;

namespace WeatherApp.Services.OpenWeather
{
    /// <summary>
    /// Provides weather forecast information for specific cities.
    /// </summary>
    public interface IOpenWeatherService
    {
        /// <summary>
        /// Returns a 5 day forecast for the matching [city].
        /// </summary>
        /// <param name="city">Name of city, use null for [zipCode] request</param>
        /// <param name="cancellationToken"></param>
        Task<OpenWeatherResponse> Get5DayForecastForCity(string city, CancellationToken cancellationToken);

        /// <summary>
        /// Returns a 5 day forecast for the matching [zipcode].
        /// </summary>
        /// <param name="zipCode">Zip Code of city, use null for [city] requests</param>
        /// <param name="cancellationToken"></param>
        Task<OpenWeatherResponse> Get5DayForecastForZipCode(string zipCode, CancellationToken cancellationToken);
    }
}
