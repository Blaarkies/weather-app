namespace WeatherApp.Domain
{
    /// <summary>
    /// API settings relating only to the OpenWeatherApi
    /// </summary>
    public class OpenWeatherSettings
    {
        public string ServiceApiKey { get; set; }
        public string Url { get; set; }
    }
}
