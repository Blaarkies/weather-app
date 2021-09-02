using System;

namespace ExtensionMethods
{
    public static class Extensions
    {
        public static bool Like(this String baseString, String query)
        {
            var safeBaseString = baseString.Trim().ToLower();
            var safeQuery = query.Trim().ToLower();
            return safeBaseString.Contains(safeQuery);
        }
    }
}
