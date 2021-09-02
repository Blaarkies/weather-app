using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WeatherApp.Services.Serializer
{
    public class SerializerService : ISerializerService
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data, _settings);
        }
    }
}
