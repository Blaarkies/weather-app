namespace WeatherApp.Domain
{
    /// <summary>
    /// API settings relating to the Crystal Weather API (this project).
    /// </summary>
    public class Settings
    {
        public int PageSize { get; set; }
        public string JwtKey { get; set; }
    }
}
