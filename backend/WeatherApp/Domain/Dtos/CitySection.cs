using System;
using WeatherApp.Domain.Dtos.OpenWeather;

namespace WeatherApp.Domain.Payloads
{
    /// <summary>
    /// Frontend payload with details about the specific city that weather has been forecasted for.
    /// </summary>
    public class CitySection
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public TimeSpan Sunrise { get; set; }
        public TimeSpan Sunset { get; set; }
        public TimeSpan Timezone { get; set; }

        public CitySection(OpenWeatherResponse openWeatherResponse)
        {
            Latitude = openWeatherResponse.City.Coord.Lat;
            Longitude = openWeatherResponse.City.Coord.Lon;
            Country = openWeatherResponse.City.Country;
            Name = openWeatherResponse.City.Name;
            Population = openWeatherResponse.City.Population;
            Sunrise = new TimeSpan(openWeatherResponse.City.Sunrise);
            Sunset = new TimeSpan(openWeatherResponse.City.Sunset);
            Timezone = new TimeSpan(openWeatherResponse.City.Timezone);
        }
    }
}
