using Microsoft.Extensions.Configuration;

namespace WeatherApp.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        public int PageSize { get; }

        public SettingsService(IConfiguration configuration)
        {
            PageSize = int.Parse(configuration["Settings:PageSize"]);
        }
    }
}
