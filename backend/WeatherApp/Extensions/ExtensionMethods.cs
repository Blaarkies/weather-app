using System;

namespace WeatherApp.Extensions
{
    public static class ExtensionMethods
    {
        public static bool Like(this string baseString, string query)
        {
            if (string.IsNullOrEmpty(baseString) || string.IsNullOrEmpty(query))
            {
                return false;
            }

            var safeBaseString = baseString.Trim().ToLower();
            var safeQuery = query.Trim().ToLower();
            return safeBaseString.Contains(safeQuery);
        }

        public static decimal KelvinToCelsius(this decimal baseValue)
        {
            return baseValue - 273.15m;
        }

        public static string DegreesToCardinal(this decimal baseValue)
        {
            var sectionIndex = Math.Floor(baseValue / 22.5m + 0.5m);
            var directions = new[]
                { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };
            return directions[Convert.ToInt32(sectionIndex % 16m)];
        }

        public static decimal MpsToKmph(this decimal baseValue)
        {
            return baseValue * 3.6m;
        }
    }
}
