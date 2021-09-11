# Crystal Weather

[Demo](https://crystal-weather.blaarkies.com)

### An app to view weather forecasts in any german city.

Users can view the current weather forecast for a specific city by selecting that city by name or by zip code.

### Forecast Page

Displays the current weather, and also future predictions around temperature, wind, and humidity. Every forecast viewed here will also save this data into local cache where it can later be viewed in the history page.

![Forecast for Munich](./storage/forecast-page.jpg?raw=true "Forecast for Munich")

### History Page

Data from the local cache is used to show details of all cities viewed previously. Each city might show multiple weeks worth of data that can be viewed by clicking on the date of interest. This data will be still be available after refreshing the browser, but can be cleared with the clear cache button in the top right. 

![Historic weather for Dortmund](./storage/history-page.jpg?raw=true "Historic weather for Dortmund")

### Features

The top toolbar allows changing between the Forecast and History pages. 

Local cache can be cleared by clicking the "Clear cached storage" button residing on the far right-hand side of the toolbar. This will permanently remove the stored weather data thus far.

Search for german cities by name or zip code, and their weather data will be retrieved.
