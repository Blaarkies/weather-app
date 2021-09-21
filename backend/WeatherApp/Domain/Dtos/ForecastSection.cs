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
        public DateTime DateTime  { get; set; }
        public decimal TemperatureFeel  { get; set; }
        public decimal Temperature  { get; set; }
        public decimal TemperatureMax  { get; set; }
        public decimal TemperatureMin  { get; set; }
        public decimal Humidity  { get; set; }
        public decimal WindDirectionDegrees  { get; set; }
        public decimal WindSpeed  { get; set; }
        public decimal WindGusts  { get; set; }
        public string Icon  { get; set; }
        public string Description  { get; set; }
        public string WindDirection  { get; set; }

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
