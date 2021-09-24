using System.Collections.Generic;
using System.Linq;
using WeatherApp.Domain.Dtos.OpenWeather;
using WeatherApp.Domain.Payloads;

namespace WeatherApp.Domain.Dtos
{
    /// <summary>
    /// Frontend payload containing the selected city and it's forecast period.
    /// </summary>
    public class WeatherPayload
    {
        public IEnumerable<ForecastSection> WeatherList { get; set; }
        public CitySection City { get; set; }

        public WeatherPayload(OpenWeatherResponse openWeatherResponse)
        {
            WeatherList = openWeatherResponse.List.Select(w => new ForecastSection(w));
            City = new CitySection(openWeatherResponse);
        }
    }
}
