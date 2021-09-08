<template>
  <div class="layout">
    <v-expand-transition>
      <v-card
          v-show="weather.length"
          height="auto"
          width="auto"
          class="current-weather-card"
      >
        <WeatherSummaryCurrentWeather
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
import WeatherSummaryCurrentWeather from "@/components/WeatherSummaryCurrentWeather";
import WeatherDataTimeline from "@/components/WeatherDataTimeline.vue";

export default {
  name: "WeatherSummary",
  components: {WeatherDataTimeline, WeatherSummaryCurrentWeather},
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
