using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WeatherApp.Services.Serializer
{
    /// <inheritdoc/>
    public class SerializerService : ISerializerService
    {
        private readonly JsonSerializerSettings _settings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        /// <inheritdoc/>
        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data, _settings);
        }
    }
}
