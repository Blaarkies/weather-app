using System;

namespace WeatherApp.Extensions
{
    /// <summary>
    /// Extension methods to add functionality to existing values.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Checks if the query is contained inside the baseString, and returns true if it is.
        /// Both strings are checked for null or empty (in which case it returns false), are trimmed of whitespace,
        /// and transformed to lower case to make the check case-insensitive.
        /// </summary>
        /// <returns>bool</returns>
        /// <example>myLongText.Like("needle")</example>
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

        /// <summary>
        /// Converts a decimal value of degrees Kelvin into the equivalent decimal value in degrees Celsius.
        /// </summary>
        /// <returns>decimal</returns>
        /// <example>myVar.KelvinToCelsius()</example>
        public static decimal KelvinToCelsius(this decimal baseValue)
        {
            return baseValue - 273.15m;
        }

        /// <summary>
        /// Converts a decimal value in rotational degrees into a string compass reading(lang:en-UK), denoting the
        /// heading.
        /// </summary>
        /// <returns>string</returns>
        /// <example>myVar.DegreesToCardinal()</example>
        public static string DegreesToCardinal(this decimal baseValue)
        {
            var sectionIndex = Math.Floor(baseValue / 22.5m + 0.5m);
            var directions = new[]
                { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };
            return directions[Convert.ToInt32(sectionIndex % 16m)];
        }

        /// <summary>
        /// Converts a decimal value in meters per second into the equivalent decimal value in kilometers per hour.
        /// </summary>
        /// <returns>decimal</returns>
        /// <example>myVar.MpsToKmph()</example>
        public static decimal MpsToKmph(this decimal baseValue)
        {
            return baseValue * 3.6m;
        }
    }
}
