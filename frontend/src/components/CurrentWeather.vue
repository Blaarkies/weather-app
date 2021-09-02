<template>
  <div>
    <v-skeleton-loader type="card" v-if="loadingWeather"/>

    <div class="layout" v-else>
      <div class="title-area">
        <v-card-title class="pa-0">Current Weather</v-card-title>
        <v-card-subtitle class="ma-0 pa-0 pl-4">{{ formatTime(weather.time) }}</v-card-subtitle>
      </div>

      <div class="left-column">
        <div class="icon-temp-layout">
          <transition name="fade">
            <v-img
                class="icon"
                :src="`http://openweathermap.org/img/wn/${weather.icon}@2x.png`"
                :key="weather.Icon"
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
        <div>Direction</div>
        <div>{{ weather.windDirection }}</div>

        <div>Speed</div>
        <div>{{ formatWindSpeed(weather.windSpeed) }} km/h</div>

        <div>Gusts</div>
        <div>{{ formatWindSpeed(weather.windGusts) }} km/h</div>
      </div>
    </div>

  </div>
</template>

<script>
export default {
  name: "CurrentWeather",
  props: {
    weather: {
      type: Object,
      default: () => ({}),
    },
    loadingWeather: {},
  },
  data: () => ({}),
  methods: {
    formatTemperature(value) {
      return value?.toFixed(0);
    },
    formatWindSpeed(value) {
      return value?.toFixed(0);
    },
    formatTime(value) {
      if (!value) {
        return '';
      }
      let date = new Date(this.weather.time);
      return date.toLocaleTimeString();
    },
  },
}
</script>

<style scoped>
.layout {
  display: grid;
  grid-template-areas:
      'title title'
      'left right';
  gap: 0 var(--gap-content-section);
}

.layout .title-area {
  grid-area: title;
}

.layout .left-column {
  grid-area: left;
}

.layout .right-column {
  grid-area: right;
  display: grid;
  grid-template-columns: auto auto;
  align-content: center;
  gap: 0 var(--gap-table-column);
}

.icon-temp-layout {
  display: grid;
  grid-template-areas:
      'icon temp-real'
      'icon temp-feel';
  gap: var(--gap-tiles) 0;
  grid-auto-rows: 1fr;
}

.icon-temp-layout .icon {
  grid-area: icon;
}

.icon-temp-layout .temp-real {
  grid-area: temp-real;
  align-self: end;
}

.icon-temp-layout .temp-feel {
  grid-area: temp-feel;
}

.weather-description {
  text-transform: capitalize;
}
</style>
