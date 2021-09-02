<template>
  <div class="layout">
    <v-expand-transition>
      <v-card
          v-show="weather[0]"
          height="auto"
          width="auto"
          class="current-weather-card"
      >
        <CurrentWeather :weather="weather[0]" :loadingWeather="loadingWeather"/>
      </v-card>
    </v-expand-transition>
  </div>
</template>

<script>
import CurrentWeather from "@/components/CurrentWeather";

export default {
  name: "WeatherSummary",
  components: {CurrentWeather},
  props: ['weather', 'loadingWeather'],
}
</script>

<style scoped>
.layout {

}

.current-weather-card {
  padding: var(--padding-card);
}
</style>
