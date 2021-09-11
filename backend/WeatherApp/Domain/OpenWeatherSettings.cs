using Microsoft.Extensions.Configuration;

namespace WeatherApp.Domain
{
    public class OpenWeatherSettings : IOpenWeatherSettings
    {
        public string ServiceApiKey { get; }
        public string Url { get; }

        public OpenWeatherSettings(IConfiguration configuration)
        {
            ServiceApiKey = configuration["OpenWeather:ServiceApiKey"];
            Url = configuration["OpenWeather:Url"];
        }
    }
}
