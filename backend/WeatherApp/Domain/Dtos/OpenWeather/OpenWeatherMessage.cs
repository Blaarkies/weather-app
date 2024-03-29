﻿using Newtonsoft.Json;

namespace WeatherApp.Domain.Dtos.OpenWeather
{
    /// <summary>
    /// Partial definition of the responses from the OpenWeatherApi, denoting the [message] used to relay requests
    /// errors.
    /// </summary>
    public class OpenWeatherMessage
    {
        [JsonProperty("cod")]
        public string Cod;
        [JsonProperty("message")]
        public string Message;
    }
}
