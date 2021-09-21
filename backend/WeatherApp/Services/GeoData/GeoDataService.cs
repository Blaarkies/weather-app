using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WeatherApp.Domain;
using WeatherApp.Extensions;
using WeatherApp.Services.JsonFileReader;

namespace WeatherApp.Services.GeoData
{
    /// <summary>
    /// Provides geographical information about cities in germany.
    /// </summary>
    public class GeoDataService : IGeoDataService
    {
        private readonly ILogger<GeoDataService> _logger;
        private readonly Settings _settings;
        private readonly IJsonFileReaderService _jsonFileReaderService;
        private IEnumerable<string> _germanCityNames;

        public GeoDataService(
            ILogger<GeoDataService> logger,
            IOptions<Settings> settingsService,
            IJsonFileReaderService jsonFileReaderService)
        {
            _logger = logger;
            _settings = settingsService.Value;
            _jsonFileReaderService = jsonFileReaderService;

            var json = _jsonFileReaderService.Read("Assets/german-cities.json").Result;
            _germanCityNames = JsonConvert.DeserializeObject<IEnumerable<string>>(json);
        }

        /// <summary>
        /// Logs information relevant to the service's behaviour.
        /// </summary>
        private void WriteMessage(string message, string stack)
        {
            _logger.LogInformation($"GeoDataService {message} \n {stack}");
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<string>> QueryCitiesForName(string search, CancellationToken cancellationToken)
        {
            var cities = await Task.FromResult(_germanCityNames);
            return cities.Where(name => name.Like(search))
                .Take(_settings.PageSize);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> GetAllCities(CancellationToken cancellationToken)
        {
            var cities = await Task.FromResult(_germanCityNames);
            return cities.Take(_settings.PageSize);
        }
    }
}
