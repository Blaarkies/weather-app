<template>
  <div>
    <v-skeleton-loader type="card" v-if="loadingWeather"/>

    <div class="layout" v-else-if="weather.icon">
      <div class="title-area">
        <div class="title-layout">
          <v-card-title class="pa-0">Current Weather</v-card-title>
          <v-card-title class="pa-0">{{ city.name }}</v-card-title>
        </div>
        <v-card-subtitle class="ma-0 pa-0 pl-4">{{ formatTime(weather.dateTime) }}</v-card-subtitle>
      </div>

      <div class="left-column">
        <div class="icon-temp-layout">
          <transition name="fade">
            <v-img
                class="icon"
                :src="`http://openweathermap.org/img/wn/${weather.icon}@2x.png`"
                :alt="'A visualization of the current weather, specifically ' + weather.description"
                :key="weather.icon"
            />
          </transition>
          <h2 class="temp-real">{{ formatTemperature(weather.temperature) }} °C</h2>
          <div>
            Feels like
            <h4 class="temp-feel">{{ formatTemperature(weather.temperatureFeel) }} °C</h4>
          </div>
        </div>
        <h4 class="weather-description">{{ weather.description }}</h4>
      </div>

      <div class="right-column">
        <div>Wind Direction</div>
        <div>{{ weather.windDirection }}</div>

        <div>Wind Speed</div>
        <div>{{ formatWindSpeed(weather.windSpeed) }} km/h</div>

        <div>Wind Gusts</div>
        <div>{{ formatWindSpeed(weather.windGusts) }} km/h</div>
      </div>
    </div>

  </div>
</template>

<script>
import {formatDateToHourMinute} from "@/helpers";

/**
 * Weather card showing the current weather details for the specified city.
 */
export default {
  name: 'CurrentWeather',
  props: {
    /**
     * Main weather object containing data to be displayed.
     */
    weather: {
      type: Object,
      default: () => ({}),
    },

    /**
     * Used to show loading skeleton loaders.
     */
    loadingWeather: Boolean,

    /**
     * City information.
     */
    city: {
      type: Object,
      default: () => ({}),
    },
  },

  methods: {
    /**
     * Formats temperature to integer display.
     * @param value decimal number
     * @returns {string|*|string}
     */
    formatTemperature(value) {
      return value?.toFixed(0);
    },

    /**
     * Formats wind speed to integer display.
     * @param value decimal number
     * @returns {string|*|string}
     */
    formatWindSpeed(value) {
      return value?.toFixed(0);
    },

    /**
     * Formats time to an hour:minutes display, or an empty string if invalid.
     * @param value date time string
     * @returns {*|string}
     */
    formatTime(value) {
      return value
          ? formatDateToHourMinute(new Date(value))
          : '';
    },
  },
};
</script>

<style scoped lang="scss">
.layout {
  display: grid;
  grid-template-areas:
      'title title'
      'left right';
  gap: 0 var(--gap-content-section);

  .title-area {
    grid-area: title;
  }

  .title-layout {
    display: flex;
    justify-content: space-between;
  }

  .left-column {
    grid-area: left;
  }

  .right-column {
    grid-area: right;
    display: grid;
    grid-template-columns: auto auto;
    align-content: center;
    gap: 0 var(--gap-table-column);
  }
}

.icon-temp-layout {
  display: grid;
  grid-template-areas:
      'icon temp-real'
      'icon temp-feel';
  gap: var(--gap-tiles) 0;
  grid-auto-rows: 1fr;


  .icon {
    grid-area: icon;
  }

  .temp-real {
    grid-area: temp-real;
    align-self: end;
  }

  .temp-feel {
    grid-area: temp-feel;
  }
}

.weather-description {
  text-transform: capitalize;
}
</style>
