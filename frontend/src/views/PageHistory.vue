<template>
  <div class="container">

    <v-expand-transition>
      <v-card
          class="no-data-card"
          v-if="cities.length === 0"
      >
        <v-card-title>No historic weather found</v-card-title>
        <v-card-subtitle>
          View weather forecasts at
          <router-link to="/forecast">Weather Forecast</router-link>
          to populate the history cache.
        </v-card-subtitle>
        <v-icon class="icon-pictogram">mdi-cloud-search-outline</v-icon>
      </v-card>
    </v-expand-transition>


    <div class="layout" v-if="cities.length > 0">
      <div>
        <v-card-title class="text-break">Select a week to see the cached forecast</v-card-title>

        <v-card class="city-list">
          <SelectorHistoryWeek
              :cities="cities"
              @select="selectHistoryWeather($event)"
          />
        </v-card>
      </div>

      <v-expand-transition>
        <v-card
            v-if="cityWeather"
            height="auto"
            width="auto"
            class="weather-timeline-card"
        >
          <WeatherDataTimeline
              :title="`Forecasts for ${cityWeather.name} from ${cityWeather.date}`"
              :weather="cityWeather.weather"
              :loading-weather="loadingWeather"
              listVertically
          />
        </v-card>
      </v-expand-transition>
    </div>

  </div>
</template>

<script>
import SelectorHistoryWeek from "@/components/SelectorHistoryWeek";
import WeatherDataTimeline from "@/components/WeatherDataTimeline";
import {formatDateToDayMonth} from "@/helpers";

/**
 * A page showing historic weather forecasts for cities.
 */
export default {
  name: "PageHistory",
  components: {WeatherDataTimeline, SelectorHistoryWeek},
  data: () => ({
    cityWeather: null,
    loadingWeather: null,
  }),
  methods: {
    /**
     * Sets up the display data from the selected city and time period.
     * @param name city name
     * @param dt date
     */
    selectHistoryWeather({name, dt}) {
      const msToHour = 36e5;
      this.cityWeather = {
        name,
        date: formatDateToDayMonth(dt),
        weather: this.$store.state.cityWeather[name].weatherList
            .filter(w => {
              // filter between selected date and 5 days forward
              let itemDate = new Date(w.dateTime);
              let selectedDate = new Date(dt);
              return itemDate >= selectedDate
                  && (itemDate - selectedDate) < msToHour * 24 * 5;
            }),
      };
    },
  },
  computed: {
    /**
     * Gets list of cities and their available weeks of weather data from the store.
     * @returns {any}
     */
    cities() {
      return this.$store.getters.citiesAndWeeksOfData;
    },
  },
}
</script>

<style scoped lang="scss">
@import 'src/styles/media';

.layout {
  height: 100%;
  display: grid;
  gap: var(--gap-tiles);
}

.no-data-card {
  display: grid;
  gap: var(--gap-tiles);
  padding: var(--padding-card);
}

.weather-timeline-card {
  padding: var(--padding-card);
}

.city-list {
  max-height: 300px;
  overflow-y: auto;
}

@media screen and ($media-above-tablets) {
  .city-list {
    max-height: 500px;
  }

  .layout {
    grid-template-columns: auto minmax(400px, auto);
  }

  .container {
    max-width: 900px;
  }
}
</style>
