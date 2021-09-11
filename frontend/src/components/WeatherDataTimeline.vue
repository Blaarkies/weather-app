<template>
  <div>
    <v-skeleton-loader
        class="ma-4"
        type="heading, image, text, image"
        v-if="loadingWeather"
    />

    <div v-else>
      <v-card-title class="pa-0 text-break">{{ title }}</v-card-title>

      <div :class="[{ 'list-vertically': listVertically }, 'layout']">
        <SparklineWeather
            title="Temperature"
            unit-of-measure="°C"
            :value-selector="dataPoint => dataPoint.temperature"
            :weather="weather"
        />

        <SparklineWeather
            title="Wind"
            unit-of-measure="km/h"
            :value-selector="dataPoint => dataPoint.windSpeed"
            :weather="weather"
        />

        <SparklineWeather
            title="Humidity"
            unit-of-measure="%"
            :value-selector="dataPoint => dataPoint.humidity"
            :weather="weather"
        />
      </div>
    </div>

  </div>
</template>

<script>
import SparklineWeather from "@/components/SparklineWeather";

/**
 * A card displaying the temperature, wind speed, and humidity data in graphs for a period of weather data points.
 */
export default {
  name: "WeatherDataTimeline",
  components: {SparklineWeather},
  props: {
    /**
     * Title of weather card.
     */
    title: String,

    /**
     * List of weather data to be displayed in graphs.
     */
    weather: {
      type: Array,
      default: () => [],
    },

    /**
     * Used by skeleton loader to show loading status.
     */
    loadingWeather: Boolean,

    /**
     * Sets the graphs to be stacked vertically, regardless of screen size/orientation.
     */
    listVertically: Boolean,
  },
}
</script>

<style scoped lang="scss">
@import 'src/styles/media';

.layout {
  display: grid;
  gap: var(--gap-tiles);
}

@media screen and ($media-above-tablets) {
  // PageHistory needs to load this component in vertical format, regardless of screen size
  .layout:not(.list-vertically) {
    grid-template-columns: 1fr 1fr;
  }
}
</style>
