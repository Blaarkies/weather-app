using System;
using Newtonsoft.Json;

namespace WeatherApp.Domain.OpenWeather
{
    public class OpenWeatherResponse
    {
        [JsonProperty("cod")]
        public string Cod;
        [JsonProperty("message")]
        public int Message;
        [JsonProperty("cnt")]
        public int Cnt;
        [JsonProperty("list")]
        public OpenWeatherForecast[] List;
        [JsonProperty("city")]
        public OpenWeatherCity City;
    }

    public class OpenWeatherForecast
    {
        [JsonProperty("dt")]
        public int Dt;
        [JsonProperty("dt_txt")]
        public string Dt_txt;
        [JsonProperty("main")]
        public OpenWeatherMainDetails Main;
        [JsonProperty("weather")]
        public OpenWeatherWeatherSummary[] Weather;
        [JsonProperty("clouds")]
        public OpenWeatherSpecificCloud Clouds;
        [JsonProperty("wind")]
        public OpenWeatherSpecificWind Wind;
        [JsonProperty("visibility")]
        public decimal Visibility;
        [JsonProperty("pop")]
        public decimal Pop;
    }

    public class OpenWeatherSpecificWind
    {
        [JsonProperty("speed")]
        public decimal Speed;
        [JsonProperty("deg")]
        public decimal Deg;
        [JsonProperty("gust")]
        public decimal Gust;
    }

    public class OpenWeatherSpecificCloud
    {
        [JsonProperty("all")]
        public string All;
    }

    public class OpenWeatherWeatherSummary
    {
        [JsonProperty("id")]
        public int Id;
        [JsonProperty("main")]
        public string Main;
        [JsonProperty("description")]
        public string Description;
        [JsonProperty("icon")]
        public string Icon;
    }

    public class OpenWeatherMainDetails
    {
        [JsonProperty("temp")]
        public decimal Temp;
        [JsonProperty("feels_like")]
        public decimal Feels_like;
        [JsonProperty("temp_min")]
        public decimal Temp_min;
        [JsonProperty("temp_max")]
        public decimal Temp_max;
        [JsonProperty("pressure")]
        public decimal Pressure;
        [JsonProperty("sea_level")]
        public decimal Sea_level;
        [JsonProperty("grnd_level")]
        public decimal Grnd_level;
        [JsonProperty("humidity")]
        public decimal Humidity;
        [JsonProperty("temp_kf")]
        public decimal Temp_kf;
    }

    public class OpenWeatherCity
    {
        [JsonProperty("id")]
        public int Id;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("coord")]
        public OpenWeatherCoordinates Coord;
        [JsonProperty("country")]
        public string Country;
        [JsonProperty("population")]
        public int Population;
        [JsonProperty("timezone")]
        public int Timezone;
        [JsonProperty("sunrise")]
        public int Sunrise;
        [JsonProperty("sunset")]
        public int Sunset;
    }

    public class OpenWeatherCoordinates
    {
        [JsonProperty("lat")]
        public decimal Lat;
        [JsonProperty("lon")]
        public decimal Lon;
    }
}
