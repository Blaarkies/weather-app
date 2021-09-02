using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Domain;
using WeatherApp.Domain.OpenWeather;

namespace WeatherApp.Services.GeoData
{
    public interface IGeoDataService
    {
        IEnumerable<String> QueryCitiesForName(String search);
    }
}
