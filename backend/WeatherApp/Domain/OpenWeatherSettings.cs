using System;
using Microsoft.Extensions.Configuration;

namespace WeatherApp.Domain
{
    public class OpenWeatherSettings
    {
        public String ServiceApiKey;
            public String Url;

        public OpenWeatherSettings(IConfiguration configuration)
        {
            ServiceApiKey = configuration["OpenWeather:ServiceApiKey"];
            Url = configuration["OpenWeather:Url"];
        }
    }
}
