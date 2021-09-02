using System;

namespace WeatherApp.Services.Serializer
{
    public interface ISerializerService
    {
        String Serialize<T>(T data);
    }
}
