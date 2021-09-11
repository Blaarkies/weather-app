using System.Net.Http;
using System.Text.Json;
using System.Web.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WeatherApp.Domain;
using WeatherApp.Services.GeoData;
using WeatherApp.Services.JsonJsonFileReader;
using WeatherApp.Services.OpenWeather;
using WeatherApp.Services.Serializer;

namespace WeatherApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Settings>(Configuration.GetSection("Settings"));
            services.Configure<OpenWeatherSettings>(Configuration.GetSection("OpenWeather"));

            services.AddSingleton<IJsonFileReaderService, JsonFileReaderService>();
            services.AddSingleton<IOpenWeatherService, OpenWeatherService>();
            services.AddSingleton<IGeoDataService, GeoDataService>();
            services.AddSingleton<ISerializerService, SerializerService>();

            services.AddTransient<HttpClient>();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.AllowTrailingCommas = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CrystalWeatherApp", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                            .WithMethods("GET")
                            .WithOrigins(
                                "http://localhost:8080",
                                "http://192.168.1.179:8080",
                                "https://crystal-weather.blaarkies.com");
                    });
            });

            var config = new HttpConfiguration();
            config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CrystalWeatherApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
