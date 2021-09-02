<template>
  <v-autocomplete
      v-model="select"
      :loading="loading"
      :items="items"
      :search-input.sync="search"
      cache-items
      class=""
      flat
      solo
      label="City"
      placeholder="Select a city"
      no-data-text="Could not find a match"
  ></v-autocomplete>
</template>

<script>
import {getCitiesAll, getCitiesByName} from "@/common/retrieve-data";

export default {
  name: "CitySelector",
  emits: ['select'],

  data() {
    return {
      loading: false,
      items: [],
      search: null,
      select: null,
    }
  },
  created() {
    this.querySelections('');
  },
  watch: {
    search(value) {
      value && value !== this.select && this.querySelections(value)
    },
    select(value) {
      this.$emit('select', value ?? '');
    },
  },
  methods: {
    querySelections(query) {
      this.loading = true

      let request = !query
          ? getCitiesAll()
          : getCitiesByName(query);

      request
          .then(cities => this.items = cities)
          .finally(() => this.loading = false);
    },
  },
}
</script>

<style scoped>

</style>
