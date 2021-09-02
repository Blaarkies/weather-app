using System;
using ExtensionMethods;
using WeatherApp.Domain.OpenWeather;

namespace WeatherApp.Controllers
{
    public class ForecastSection
    {
        public DateTime Time;
        public decimal TemperatureFeel;
        public decimal Temperature;
        public decimal TemperatureMax;
        public decimal TemperatureMin;
        public String Icon;
        public String Description;
        public String WindDirection;
        public decimal WindDirectionDegrees;
        public decimal WindSpeed;
        public decimal WindGusts;

        public ForecastSection(OpenWeatherForecast openWeatherForecast)
        {
            Time = Convert.ToDateTime(openWeatherForecast.Dt_txt);
            TemperatureFeel = openWeatherForecast.Main.Feels_like.KelvinToCelsius();
            Temperature = openWeatherForecast.Main.Temp.KelvinToCelsius();
            TemperatureMax = openWeatherForecast.Main.Temp_max.KelvinToCelsius();
            TemperatureMin = openWeatherForecast.Main.Temp_min.KelvinToCelsius();
            Icon = openWeatherForecast.Weather[0].Icon;
            Description = openWeatherForecast.Weather[0].Description;
            WindDirection = openWeatherForecast.Wind.Deg.DegreesToCardinal();
            WindDirectionDegrees = openWeatherForecast.Wind.Deg;
            WindSpeed = openWeatherForecast.Wind.Speed.MpsToKmph();
            WindGusts = openWeatherForecast.Wind.Gust.MpsToKmph();
        }
    }
}
