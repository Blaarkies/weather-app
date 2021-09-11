using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherApp.Domain;
using WeatherApp.Extensions;
using WeatherApp.Services.JsonJsonFileReader;

namespace WeatherApp.Services.GeoData
{
    /// <summary>
    /// Provides geographical information about cities in germany.
    /// </summary>
    public class GeoDataService : IGeoDataService
    {
        private readonly ILogger<GeoDataService> _logger;
        private readonly Settings _settings;
        private readonly HttpClient _client;
        private readonly IJsonFileReaderService _jsonFileReaderService;
        private IEnumerable<string> _germanCityNames;
        private readonly IEnumerable<string> _validationNames = new[]
        {
            "Berlin",
            "Hamburg",
            "Munich",
            "Cologne",
            "Frankfurt am Main",
        };

        public GeoDataService(
            ILogger<GeoDataService> logger,
            IOptions<Settings> settingsService,
            HttpClient client,
            IJsonFileReaderService jsonFileReaderService)
        {
            _logger = logger;
            _settings = settingsService.Value;
            _client = client;
            _jsonFileReaderService = jsonFileReaderService;
        }

        /// <summary>
        /// Logs information relevant to the service's behaviour.
        /// </summary>
        private void WriteMessage(string message, string stack)
        {
            _logger.LogInformation($"GeoDataService {message} \n {stack}");
        }

        /// <summary>
        /// Attempts to populate the API with a list of city names, for quick responses to frontend request.
        /// </summary>
        private async Task SetupListOfCityNames(CancellationToken cancellationToken)
        {
            IEnumerable<string> wikiList = null;
            try
            {
                wikiList = await GetWikiGermanCityNames(cancellationToken);
            }
            catch (Exception e)
            {
                WriteMessage("Could not find Wikipedia list of german cities, falling back to local json list. "
                             + e.Message, e.StackTrace);
            }

            // test if the wiki list was parsed correctly
            if (wikiList != null
                && _validationNames.All(v => wikiList.Contains(v)))
            {
                _germanCityNames = wikiList;
                return;
            }

            _germanCityNames = await GetFallbackGermanCityNames();
        }

        /// <summary>
        /// Returns a list of city names from a JSON file. This is used a fallback if other methods of city name
        /// retrievals failed.
        /// </summary>
        private async Task<IEnumerable<string>> GetFallbackGermanCityNames()
        {
            var json = await _jsonFileReaderService.Read("Assets/german-cities.json");
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json);
            return items;
        }

        /// <summary>
        /// Queries the Wikimedia API and attempts to retrieve the list of Towns/Cities in Germany.
        /// </summary>
        private async Task<IEnumerable<string>> GetWikiGermanCityNames(CancellationToken cancellationToken)
        {
            var wikiApi = "https://en.wikipedia.org/w/api.php";
            _client.BaseAddress = new Uri($"{wikiApi}/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // The request will try to find this page
            // https://en.wikipedia.org/wiki/List_of_cities_and_towns_in_Germany
            var response = await _client.GetAsync(
                "?action=query&format=json&prop=revisions&titles=List%20of%20cities%20and%20towns%20in%20Germany&rvslots=*&rvprop=content&formatversion=2",
                cancellationToken);

            var jsonPayload = await response.Content.ReadAsStringAsync(cancellationToken);

            var content = JObject.Parse(jsonPayload)["query"]["pages"][0]["revisions"][0]["slots"]["main"]["content"]
                .ToString();

            // The content is not in a simple format to parse. This pulls city names from the list, and cleans up names
            // from any special characters.
            var cities = content.Split("\n")
                .SkipWhile(l => l != "==A==") // skip the lines above the first "A" heading (those are aggregations)
                .Where(l => l.Contains("* [[")) // filter anything that is not possible a city name line
                // regex that selects a name up to and stopping at any special characters " ,|]( "
                .Select(l => Regex.Matches(l, @"\b[^,\|\]\(\/]+[,\|\]\(]+", RegexOptions.IgnoreCase)[0].Value)
                .Select(text => text
                    .Replace("]", "") // remove leftover special characters
                    .Replace(",", "")
                    .Replace(".", "")
                    .Replace("_", "")
                    .Replace("(", ""));

            return cities;
        }

        /// <summary>
        /// Gets german city names, and handles first call setup of the names list.
        /// </summary>
        private async Task<IEnumerable<string>> GetGermanCityNames(CancellationToken cancellationToken)
        {
            if (_germanCityNames == null)
            {
                await SetupListOfCityNames(cancellationToken);
            }

            return await Task.FromResult(_germanCityNames);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> QueryCitiesForName(string search, CancellationToken cancellationToken)
        {
            var cities = await GetGermanCityNames(cancellationToken);
            return cities.Where(name => name.Like(search))
                .Take(_settings.PageSize);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> GetAllCities(CancellationToken cancellationToken)
        {
            var cities = await GetGermanCityNames(cancellationToken);
            return cities.Take(_settings.PageSize);
        }
    }
}
