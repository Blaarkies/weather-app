<template>
  <v-list class="city-data-list">
    <v-list-group
        v-for="city in cities"
        :key="city.title"
        v-model="city.active"
    >
      <template v-slot:activator>
        <v-list-item-content>
          <v-list-item-title v-text="city.title"/>
        </v-list-item-content>
      </template>

      <v-list-item-group class="pl-4">
        <v-list-item
            v-for="week in city.weeks"
            :key="week.title"
            @click="selectCityAtDate(city, week)"
        >
          <v-list-item-content>
            <v-list-item-title v-text="week.title"/>
          </v-list-item-content>
        </v-list-item>
      </v-list-item-group>

    </v-list-group>
  </v-list>
</template>

<script>
export default {
  name: "HistoryWeekSelector",
  props: {
    cities: {
      default: () => [],
    }
  },
  emits: ['select'],
  methods: {
    selectCityAtDate(city, week) {
      this.$emit('select', 'look here');
      console.log('plain usage', city, week);
    },
  },
}
</script>

<style scoped>
.city-data-list {

}
</style>
