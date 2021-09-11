using System;
using WeatherApp.Domain.OpenWeather;
using WeatherApp.Extensions;

namespace WeatherApp.Domain.Payloads
{
    /// <summary>
    /// Frontend payload with information used to display the weather forecast for a 3 hour period.
    /// </summary>
    public class ForecastSection
    {
        public DateTime DateTime;
        public decimal TemperatureFeel;
        public decimal Temperature;
        public decimal TemperatureMax;
        public decimal TemperatureMin;
        public decimal Humidity;
        public decimal WindDirectionDegrees;
        public decimal WindSpeed;
        public decimal WindGusts;
        public string Icon;
        public string Description;
        public string WindDirection;

        public ForecastSection(OpenWeatherForecast openWeatherForecast)
        {
            DateTime = Convert.ToDateTime(openWeatherForecast.Dt_txt);
            TemperatureFeel = openWeatherForecast.Main.Feels_like.KelvinToCelsius();
            Temperature = openWeatherForecast.Main.Temp.KelvinToCelsius();
            TemperatureMax = openWeatherForecast.Main.Temp_max.KelvinToCelsius();
            TemperatureMin = openWeatherForecast.Main.Temp_min.KelvinToCelsius();
            Humidity = openWeatherForecast.Main.Humidity;
            WindDirection = openWeatherForecast.Wind.Deg.DegreesToCardinal();
            WindDirectionDegrees = openWeatherForecast.Wind.Deg;
            WindSpeed = openWeatherForecast.Wind.Speed.MpsToKmph();
            WindGusts = openWeatherForecast.Wind.Gust.MpsToKmph();
            Icon = openWeatherForecast.Weather[0].Icon;
            Description = openWeatherForecast.Weather[0].Description;
        }
    }
}
