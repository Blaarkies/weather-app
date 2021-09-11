# Crystal Weather API

A .NET 5 Web API that is accessed by the Crystal Weather Frontend clients requesting forecast and naming data about cities in germany.

### Requires a valid OpenWeatherMap.org api key

Before starting up the project, go to [OpenWeatherMap](https://openweathermap.org/api) and sign up for an account to get an API key.

Then run these command to initialise the API key as user secrets:

```
dotnet user-secrets init
dotnet user-secrets set "OpenWeather:ServiceApiKey" "<myValidApiKey>"
```

More help can be found at [Safe storage of app secrets in development in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows).

### On Startup

The API will set the following origins to be allowed using CORS:
- `http://localhost:8080`
  - Local development environment for the frontend Vue project
- `http://192.168.1.179:8080`
  - Local development environment for the frontend Vue project, but on the local network.
  - This is useful for visual UI debugging on mobile devices.
- `https://crystal-weather.blaarkies.com`
  - Production environment of hosted frontend

### Application Configuration

In the `WeatherApp/appsettings.json` file, there resides a section names `Settings`.

This section contains `PageSize`, an integer value denoting the amount of items returned to the frontend (e.g. for autocomplete requests filtering through all city names).

