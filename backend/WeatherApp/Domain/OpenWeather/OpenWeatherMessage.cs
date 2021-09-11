using Newtonsoft.Json;

namespace WeatherApp.Domain.OpenWeather
{
    public class OpenWeatherMessage
    {
        [JsonProperty("cod")]
        public string Cod;
        [JsonProperty("message")]
        public int Message;
    }
}
