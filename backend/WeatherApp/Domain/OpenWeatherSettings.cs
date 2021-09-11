using Microsoft.Extensions.Configuration;

namespace WeatherApp.Domain
{
    public class OpenWeatherSettings
    {
        public readonly string ServiceApiKey;
        public readonly string Url;

        public OpenWeatherSettings(IConfiguration configuration)
        {
            ServiceApiKey = configuration["OpenWeather:ServiceApiKey"];
            Url = configuration["OpenWeather:Url"];
        }
    }
}
