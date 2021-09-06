using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Domain.OpenWeather;

namespace WeatherApp.Services.OpenWeather
{
    public interface IOpenWeatherService
    {
        Task<OpenWeatherResponse> Get5DayForecast(String city, String zipCode, CancellationToken cancellationToken);
    }
}
