<template>
  <div class="layout">
    <v-card-title>Select a city to view its forecast</v-card-title>

    <div class="selectors">
      <CitySelector :key="'city'+citySelectorKey" @select="setWeatherForCity($event)"/>
      <ZipCodeInput :key="'zip'+zipCodeSelectorKey" @inputZipCode="setWeatherForZipCode($event)"/>
    </div>

    <div class="overlap-container">
      <WeatherSummary
          :weather="weather"
          :loadingWeather="loadingWeather"
          :city="city"
      />

      <v-expand-x-transition mode="out">
        <v-progress-linear
            class="weather-loading"
            v-show="loadingWeather"
            reverse
            indeterminate
        />
      </v-expand-x-transition>
    </div>

    <v-snackbar
        v-model="snackBarOpen"
        :timeout="10e3"
        color="deep-orange"
    >
      {{ snackBarMessage }}
    </v-snackbar>
  </div>
</template>

<script>
import CitySelector from "@/components/CitySelector";
import WeatherSummary from "@/components/WeatherSummary";
import {getForecast} from "@/common/retrieve-data";
import ZipCodeInput from "@/components/ZipCodeInput";
import {getMessageFromError} from "@/common/error-handling";

export default {
  name: "PageWeatherForecasts",
  components: {
    ZipCodeInput,
    WeatherSummary,
    CitySelector,
  },
  data: () => ({
    weather: [],
    city: {},
    loadingWeather: null,
    snackBarOpen: null,
    snackBarMessage: null,
    citySelectorKey: 0,
    zipCodeSelectorKey: 0,
  }),
  methods: {
    setSnackBarMessage(error) {
      this.snackBarMessage = getMessageFromError(error);
      this.snackBarOpen = true;
    },
    async setWeatherForCity(cityName) {
      this.loadingWeather = true;

      getForecast(cityName)
          .then(response => {
            this.weather = response.weatherList;
            this.city = response.city;
          })
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
    async setWeatherForZipCode(zipCode) {
      this.loadingWeather = true;

      getForecast(null, zipCode)
          .then(response => {
            this.weather = response.weatherList;
            this.city = response.city;
          })
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
  },

}
</script>

<style scoped>
.layout {
  /*display: grid;*/
  /*gap: var(--gap-content-section);*/
}

.weather-loading {
  justify-self: center;
}

.selectors {
  display: flex;
  gap: var(--gap-content-section);
}
</style>
