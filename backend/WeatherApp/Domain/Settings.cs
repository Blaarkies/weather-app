using Microsoft.Extensions.Configuration;

namespace WeatherApp.Domain
{
    public class Settings
    {
        public int PageSize = 20;

        public Settings(IConfiguration configuration)
        {
            PageSize = int.Parse(configuration["Settings:PageSize"]);
        }
    }
}
