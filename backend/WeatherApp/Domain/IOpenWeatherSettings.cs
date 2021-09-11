namespace WeatherApp.Domain
{
    public interface IOpenWeatherSettings
    {
        string ServiceApiKey { get; }
        string Url { get; }
    }
}
