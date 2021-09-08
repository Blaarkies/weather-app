<template>
  <div>

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
      <v-card class="city-list">
        <SelectorHistoryWeek
            :cities="cities"
            @select="selectHistoryWeather($event)"
        />
      </v-card>

      <v-expand-transition>
        <v-card
            v-if="cityWeather"
            height="auto"
            width="auto"
        >
          <WeatherDataTimeline
              :title="`Forecasts for ${cityWeather.name} from ${cityWeather.date}`"
              :weather="cityWeather.weather"
              :loading-weather="loadingWeather"
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

export default {
  name: "PageHistory",
  components: {WeatherDataTimeline, SelectorHistoryWeek},
  data: () => ({
    cityWeather: null,
    loadingWeather: null,
  }),
  methods: {
    selectHistoryWeather({name, dt}) {
      let msToHour = 36e5;
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
    cities() {
      return this.$store.getters.citiesAndWeeksOfData;
    },
  },
}
</script>

<style scoped>
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

.city-list {
  max-height: 300px;
  overflow-y: auto;
}
</style>
