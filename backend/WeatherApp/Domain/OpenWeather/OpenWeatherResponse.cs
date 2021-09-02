﻿using System;
using Newtonsoft.Json;

namespace WeatherApp.Domain.OpenWeather
{
    public class OpenWeatherResponse
    {
        [JsonProperty("cod")]
        public String Cod;
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
        [JsonProperty("dt_txt")]
        public String Dt_txt;
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
        public String All;
    }

    public class OpenWeatherWeatherSummary
    {
        [JsonProperty("id")]
        public int Id;
        [JsonProperty("main")]
        public String Main;
        [JsonProperty("description")]
        public String Description;
        [JsonProperty("icon")]
        public String Icon;
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
        public String Name;
        [JsonProperty("coord")]
        public OpenWeatherCoordinates Coord;
        [JsonProperty("country")]
        public String Country;
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
