using System.Collections.Generic;
using System.Linq;
using WeatherApp.Domain.OpenWeather;

namespace WeatherApp.Controllers
{
    public class WeatherPayload
    {
        public IEnumerable<ForecastSection> WeatherList;
        public CitySection City;

        public WeatherPayload(OpenWeatherResponse openWeatherResponse)
        {
            WeatherList = openWeatherResponse.List.Select(w => new ForecastSection(w));
            City = new CitySection(openWeatherResponse);
        }
    }
}
