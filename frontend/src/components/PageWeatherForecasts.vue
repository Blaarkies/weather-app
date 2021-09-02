<template>
  <div>
    <h2>Weather Forecasts</h2>

    <CitySelector :key="citySelectorKey" @select="setWeatherForCity($event)"/>

    <div class="overlap-container">
      <WeatherSummary :weather="weather" :loadingWeather="loadingWeather"/>

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

export default {
  name: "PageWeatherForecasts",
  components: {
    WeatherSummary,
    CitySelector,
  },
  data: () => ({
    weather: [],
    loadingWeather: null,
    snackBarOpen: null,
    snackBarMessage: null,
    citySelectorKey: 0,
  }),
  methods: {
    setSnackBarMessage(error) {
      this.snackBarMessage = error;
      this.snackBarOpen = true;
    },
    async setWeatherForCity(cityName) {
      this.loadingWeather = true;

      let response = await getForecast(cityName)
          .catch(error => {
            this.setSnackBarMessage(error);
            this.citySelectorKey++;
          })
          .finally(() => this.loadingWeather = false);
      this.weather = response.weatherList;
    },
  },

}
</script>

<style scoped>
.weather-loading {
  justify-self: center;
}
</style>
