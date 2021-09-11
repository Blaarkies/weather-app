<template>
  <v-list>
    <v-list-group
        v-for="city in cities"
        :key="city.title"
        v-model="city.active"
    >
      <template v-slot:activator>
        <v-list-item-content>
          <v-list-item-title v-text="city.name"/>
        </v-list-item-content>
      </template>

      <v-list-item-group class="pl-4">
        <v-list-item
            v-for="(week, i) in city.weeks"
            :key="week.name + i"
            @click="selectCityAtDate(city.name, week.dt)"
        >
          <v-list-item-content>
            <v-list-item-title v-text="week.name"/>
          </v-list-item-content>
        </v-list-item>
      </v-list-item-group>

    </v-list-group>
  </v-list>
</template>

<script>
/**
 * List of city names with nested accordion style contents, showing the available weeks of data for each city..
 */
export default {
  name: "SelectorHistoryWeek",
  props: {
    /**
     * List of cities and their nested weeks of weather data.
     */
    cities: {
      type: Array,
      default: () => [],
    }
  },
  emits: {select: Object},

  methods: {
    /**
     * Sends emit events of 'select' with current selection.
     * @param name city name
     * @param dt date time of week
     */
    selectCityAtDate(name, dt) {
      this.$emit('select', {name, dt});
    },
  },
}
</script>

<style scoped>
</style>
