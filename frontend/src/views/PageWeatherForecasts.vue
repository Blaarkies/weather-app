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
          @select="setWeatherByCity($event)"
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
    setSnackBarMessage(error) {
      this.$root.snackbar.postError({message: getMessageFromError(error)})
    },

    async setWeatherByCity(cityName) {
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

    setWeatherByZipCode(zipCode) {
      this.loadingWeather = true;

      getForecast(null, zipCode)
          .then(({city, weatherList}) => this.setWeather(city, weatherList))
          .catch(error => {
            this.setSnackBarMessage(error);
            this.weather = [];
            this.city = {};
          })
          .finally(() => {
            this.loadingWeather = false;
            this.citySelectorKey++;
          });
    },

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
