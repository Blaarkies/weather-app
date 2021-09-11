<template>
  <div class="layout">

    <v-expand-x-transition mode="out">
      <v-progress-linear
          absolute
          class="weather-loading"
          v-show="loadingWeather"
          reverse
          indeterminate
      />
    </v-expand-x-transition>

    <v-card-title class="forecast-title text-break">Select a city to view its forecast</v-card-title>

    <div class="selectors">
      <BaseSelectorCity
          :key="'city'+citySelectorKey"
          @select="setWeatherByCityName($event)"
      />
      <BaseInputZipCode
          :key="'zip'+zipCodeSelectorKey"
          @inputZipCode="setWeatherByZipCode($event)"
      />
    </div>

    <v-expand-transition>
      <v-card
          v-show="weather.length"
          height="auto"
          width="auto"
          class="current-weather-card"
      >
        <CurrentWeather
            :weather="weather[0]"
            :loadingWeather="loadingWeather"
            :city="city"
        />
      </v-card>
    </v-expand-transition>

    <v-expand-transition>
      <v-card
          v-show="weather.length"
          height="auto"
          width="auto"
          class="timeline-weather-card"
      >
        <WeatherDataTimeline
            title="Future Weather"
            :weather="weather"
            :loadingWeather="loadingWeather"
        />
      </v-card>
    </v-expand-transition>
  </div>
</template>

<script>
import BaseSelectorCity from "@/components/BaseSelectorCity";
import {getForecast, getMessageFromError} from "@/helpers";
import BaseInputZipCode from "@/components/BaseInputZipCode";
import CurrentWeather from '@/components/CurrentWeather';
import WeatherDataTimeline from '@/components/WeatherDataTimeline';

/**
 * A page displaying the weather forecast for a selected city or zip code. Forecast is displayed in a current weather
 * card, and a 5-day future forecast is shown in data graph format.
 */
export default {
  name: "PageWeatherForecasts",
  components: {
    WeatherDataTimeline,
    CurrentWeather,
    BaseInputZipCode,
    BaseSelectorCity,
  },
  data: () => ({
    weather: [],
    city: {},
    loadingWeather: null,
    citySelectorKey: 0,
    zipCodeSelectorKey: 0,
  }),
  methods: {
    /**
     * Provides an error message to the user generated from an API error object. Typical errors include
     * "City not found", "Network error", etc.
     * @param error
     */
    setSnackBarMessage(error) {
      this.$root.snackbar.postError({message: getMessageFromError(error)})
    },

    /**
     * Sets the weather forecast by city name. If errors are encountered, it resets the weather display,
     * and clears the city name input. On success, it clears the zip code input.
     * @param cityName
     * @returns {Promise<void>}
     */
    async setWeatherByCityName(cityName) {
      this.loadingWeather = true;

      getForecast(cityName)
          .then(({city, weatherList}) => this.setWeather(city, weatherList))
          .catch(error => {
            this.setSnackBarMessage(error);
            this.citySelectorKey++;
            this.weather = [];
            this.city = {};
          })
          .finally(() => {
            this.loadingWeather = false;
            this.zipCodeSelectorKey++;
          });
    },

    /**
     * Sets the weather forecast by zip code. If errors are encountered, it resets the weather display,
     * and clears the zip code input. On success, it clears the city name input.
     * @param cityName
     * @returns {Promise<void>}
     */
    async setWeatherByZipCode(zipCode) {
      this.loadingWeather = true;

      getForecast(null, zipCode)
          .then(({city, weatherList}) => this.setWeather(city, weatherList))
          .catch(error => {
            this.setSnackBarMessage(error);
            this.zipCodeSelectorKey++;
            this.weather = [];
            this.city = {};
          })
          .finally(() => {
            this.loadingWeather = false;
            this.citySelectorKey++;
          });
    },

    /**
     * Set data to new weather data, and save the new data into the store.
     * @param city
     * @param weatherList
     */
    setWeather(city, weatherList) {
      this.city = city;
      this.weather = weatherList;

      this.$store.dispatch('saveCityWeather', {city, weatherList});
    },
  },

}
</script>

<style scoped lang="scss">
@import 'src/styles/media';

.selectors {
  display: flex;
  gap: var(--gap-content-section);
}

.layout {
  display: grid;
  grid-template-areas:
    'title'
    'selectors'
    'loading'
    'current'
    'timeline';
  gap: var(--gap-tiles);

  .weather-loading {
    justify-self: center;
    grid-area: loading;
  }

  .forecast-title {
    grid-area: title;
  }

  .selectors {
    grid-area: selectors;
  }

  .current-weather-card {
    grid-area: current;
    padding: var(--padding-card);
  }

  .timeline-weather-card {
    grid-area: timeline;
    padding: var(--padding-card);
  }
}

@media screen and ($media-above-tablets) {
  .layout {
    max-width: 1200px;
    grid-template-areas:
      'loading   loading'
      'title     current'
      'selectors current'
      'timeline  timeline';
    grid-template-columns: minmax(160px, auto) minmax(200px, auto);
    grid-template-rows: max-content max-content 1fr max-content;

    .current-weather-card {
      align-self: start;
    }
  }
}
</style>
