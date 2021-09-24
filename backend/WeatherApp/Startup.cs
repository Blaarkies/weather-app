using System.Text.Json;
using System.Web.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WeatherApp.Domain;
using WeatherApp.Middleware;
using WeatherApp.Services.GeoData;
using WeatherApp.Services.JsonFileReader;
using WeatherApp.Services.OpenWeather;
using WeatherApp.Services.User;

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

            services.AddScoped<IJsonFileReaderService, JsonFileReaderService>();
            services.AddScoped<IOpenWeatherService, OpenWeatherService>();
            services.AddScoped<IGeoDataService, GeoDataService>();

            services.AddApplicationInsightsTelemetry();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.AllowTrailingCommas = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });


            services.AddScoped<IUserService, UserService>();

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
                            .WithMethods("GET", "POST")
                            .AllowAnyHeader()
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

            app.UseMiddleware<JwtMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
