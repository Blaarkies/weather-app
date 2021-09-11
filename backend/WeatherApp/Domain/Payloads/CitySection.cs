using System;
using WeatherApp.Domain.OpenWeather;

namespace WeatherApp.Controllers
{
    public class CitySection
    {
        public decimal Latitude;
        public decimal Longitude;
        public string Country;
        public string Name;
        public int Population;
        public TimeSpan Sunrise;
        public TimeSpan Sunset;
        public TimeSpan Timezone;

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
