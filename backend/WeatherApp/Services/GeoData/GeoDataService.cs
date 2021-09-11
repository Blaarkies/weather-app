using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
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

        private void WriteMessage(string message, string stack)
        {
            _logger.LogInformation($"GeoDataService {message} \n {stack}");
        }

        private async Task SetupListOfCityNames()
        {
            IEnumerable<string> wikiList = null;
            try
            {
                wikiList = await GetWikiGermanCityNames();
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

        private async Task<IEnumerable<string>> GetFallbackGermanCityNames()
        {
            var json = await _jsonFileReaderService.Read("Assets/german-cities.json");
            var items = JsonConvert.DeserializeObject<IEnumerable<string>>(json);
            return items;
        }

        private async Task<IEnumerable<string>> GetWikiGermanCityNames()
        {
            // https://en.wikipedia.org/wiki/List_of_cities_and_towns_in_Germany
            var wikiApi = "https://en.wikipedia.org/w/api.php";
            _client.BaseAddress = new Uri($"{wikiApi}/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _client.GetAsync(
                "?action=query&format=json&prop=revisions&titles=List%20of%20cities%20and%20towns%20in%20Germany&rvslots=*&rvprop=content&formatversion=2");

            var jsonPayload = await response.Content.ReadAsStringAsync();

            var content = JObject.Parse(jsonPayload)["query"]["pages"][0]["revisions"][0]["slots"]["main"]["content"]
                .ToString();

            var cities = content.Split("\n")
                .SkipWhile(l => l != "==A==")
                .Where(l => l.Contains("* [["))
                .Select(l => Regex.Matches(l, @"\b[^,\|\]\(\/]+[,\|\]\(]+", RegexOptions.IgnoreCase)[0].Value)
                .Select(text => text
                    .Replace("]", "")
                    .Replace(",", "")
                    .Replace(".", "")
                    .Replace("_", "")
                    .Replace("(", ""));

            return cities;
        }

        private async Task<IEnumerable<string>> GetGermanCityNames()
        {
            if (_germanCityNames == null)
            {
                await SetupListOfCityNames();
            }

            return await Task.FromResult(_germanCityNames);
        }

        public async Task<IEnumerable<string>> QueryCitiesForName(string search)
        {
            var cities = await GetGermanCityNames();
            return cities.Where(name => name.Like(search))
                .Take(_settings.PageSize);
        }

        public async Task<IEnumerable<string>> GetAllCities()
        {
            var cities = await GetGermanCityNames();
            return cities.Take(_settings.PageSize);
        }
    }
}
