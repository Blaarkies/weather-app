namespace WeatherApp.Services.Serializer
{
    /// <summary>
    /// Provides JSON serialization from POCO objects.
    /// </summary>
    public interface ISerializerService
    {
        /// <summary>
        /// Serializes an object into a JSON string.
        /// </summary>
        /// <param name="data">Object to be serialized</param>
        /// <typeparam name="T">Object's type</typeparam>
        string Serialize<T>(T data);
    }
}
