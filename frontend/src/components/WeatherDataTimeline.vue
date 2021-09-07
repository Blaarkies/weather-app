<template>
  <div>
    <v-skeleton-loader
        class="ma-4"
        type="heading, image, text, image"
        v-if="loadingWeather"
    />

    <div class="layout" v-else>
      <v-card-title class="pa-0">{{ title }}</v-card-title>

      <WeatherSparkline
          title="Temperature"
          unit-of-measure="°C"
          :value-selector="weather => weather.temperature"
          :weather="weather"
      />

      <WeatherSparkline
          title="Wind"
          unit-of-measure="km/h"
          :value-selector="weather => weather.windSpeed"
          :weather="weather"
      />

      <WeatherSparkline
          title="Humidity"
          unit-of-measure="%"
          :value-selector="weather => weather.humidity"
          :weather="weather"
      />
    </div>

  </div>
</template>

<script>
import WeatherSparkline from "@/components/SparklineWeather";

export default {
  name: "FutureWeather",
  components: {WeatherSparkline},
  props: {
    title: String,
    weather: {
      type: Array,
      default: () => [],
    },
    loadingWeather: Boolean,
  },
}
</script>

<style scoped>
.layout {
  padding: var(--padding-card);
  display: grid;
  gap: var(--gap-tiles);
}
</style>