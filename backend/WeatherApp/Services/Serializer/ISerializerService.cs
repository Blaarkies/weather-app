namespace WeatherApp.Services.Serializer
{
    public interface ISerializerService
    {
        string Serialize<T>(T data);
    }
}
