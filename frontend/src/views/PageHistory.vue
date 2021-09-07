<template>
  <div>

    <v-expand-transition>

      <v-card class="no-data-card" v-if="cities.length === 0">
        <v-card-title>No historic weather found</v-card-title>
        <v-card-subtitle>
          View weather forecasts at
          <router-link to="/forecast">Weather Forecast</router-link>
          to populate the history cache.
        </v-card-subtitle>
        <v-icon class="icon-pictogram">mdi-cloud-search-outline</v-icon>
      </v-card>


      <div class="" v-else>
        <HistoryWeekSelector
            :cities="cities"
            @select="selectHistoryWeather($event)"
        />
      </div>

    </v-expand-transition>
  </div>
</template>

<script>
import HistoryWeekSelector from "@/components/SelectorHistoryWeek";

export default {
  name: "PageHistory",
  components: {HistoryWeekSelector},
  data: () => ({
    cities: [
      {
        title: 'Leipzig',
        weeks: [
          {title: '13 August 2021'},
          {title: '20 August 2021'},
          {title: '27 August 2021'},
        ],
      },
      {
        title: 'Munich',
        weeks: [
          {title: '7 August 2021'},
          {title: '27 August 2021'},
          {title: '3 September 2021'},
        ],
      },
      {
        title: 'Berlin',
        weeks: [],
      },
      {
        title: 'Frankfurt',
        weeks: [],
      },
      {
        title: 'Hamburg',
        weeks: [],
      },
      {
        title: 'Mullheim',
        weeks: [],
      },
    ],
  }),
  methods: {
    selectHistoryWeather(value) {
      console.log(value);
    },
  },
}
</script>

<style scoped>
.no-data-card {
  display: grid;
  gap: var(--gap-tiles);
  padding: var(--padding-card);
}
</style>
