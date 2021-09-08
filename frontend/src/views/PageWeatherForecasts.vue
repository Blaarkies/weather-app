<template>
  <div>
    <v-card-title>Select a city to view its forecast</v-card-title>

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
  </div>
</template>

<script>
import BaseSelectorCity from "@/components/BaseSelectorCity";
import WeatherSummary from "@/components/WeatherSummary";
import {getForecast, getMessageFromError} from "@/helpers";
import BaseInputZipCode from "@/components/BaseInputZipCode";

export default {
  name: "PageWeatherForecasts",
  components: {
    BaseInputZipCode,
    WeatherSummary,
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

    async setWeatherByZipCode(zipCode) {
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

<style scoped>
.weather-loading {
  justify-self: center;
}

.selectors {
  display: flex;
  gap: var(--gap-content-section);
}
</style>
