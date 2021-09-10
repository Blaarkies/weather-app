using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherApp.Domain;
using WeatherApp.Extensions;

namespace WeatherApp.Services.GeoData
{
    public class GeoDataService : IGeoDataService
    {
        private readonly ILogger<GeoDataService> _logger;
        private readonly IEnumerable<String> _germanCityNames;
        private readonly Settings _settings;

        public GeoDataService(ILogger<GeoDataService> logger, Settings settings)
        {
            _logger = logger;
            _settings = settings;

            using StreamReader r = new StreamReader("Assets/german-cities.json");
            String json = r.ReadToEnd();
            var items = JsonConvert.DeserializeObject<IEnumerable<String>>(json);
            _germanCityNames = items;
        }

        public void WriteMessage(string message)
        {
            _logger.LogInformation($"MyDependency2.WriteMessage Message: {message}");
        }

        public IEnumerable<String> QueryCitiesForName(String search)
        {
            return _germanCityNames.Where(name => name.Like(search))
                .Take(_settings.PageSize);
        }

        public IEnumerable<String> AllCities()
        {
            return _germanCityNames
                .Take(_settings.PageSize);
        }
    }
}
