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
        <FutureWeather
            title="Future Weather"
            :weather="weather"
            :loadingWeather="loadingWeather"
        />
      </v-card>
    </v-expand-transition>
  </div>
</template>

<script>
import CurrentWeather from "@/components/WeatherSummaryCurrentWeather";
import FutureWeather from "@/components/WeatherDataTimeline.vue";

export default {
  name: "WeatherSummary",
  components: {FutureWeather, CurrentWeather},
  props: {
    weather: {
      type: Array,
      default: () => [],
    },
    loadingWeather: Boolean,
    city: {
      type: Object,
      default: () => ({}),
    },
  },
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
