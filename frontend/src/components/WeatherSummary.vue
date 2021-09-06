<template>
  <div class="layout">
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
          class="graph-weather-card"
      >
        <FutureWeather :weather="weather" :loadingWeather="loadingWeather"/>
      </v-card>
    </v-expand-transition>
  </div>
</template>

<script>
import CurrentWeather from "@/components/CurrentWeather";
import FutureWeather from "@/components/FutureWeather";

export default {
  name: "WeatherSummary",
  components: {FutureWeather, CurrentWeather},
  props: ['weather', 'loadingWeather', 'city'],
}
</script>

<style scoped>
.layout {
  display: grid;
  gap: var(--gap-tiles);
}

.current-weather-card {
  padding: var(--padding-card);
}
</style>
