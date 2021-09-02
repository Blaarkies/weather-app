using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Domain;
using WeatherApp.Domain.OpenWeather;

namespace WeatherApp.Services.OpenWeather
{
    public interface IOpenWeatherService
    {
        // by city or zipCode.
        Task<OpenWeatherResponse> Get5DayForecast(String city, CancellationToken cancellationToken);
    }
}
