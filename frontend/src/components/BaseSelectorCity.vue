<template>
  <v-autocomplete
      v-model="select"
      :loading="loading"
      :items="items"
      :search-input.sync="search"
      cache-items
      flat
      label="City"
      placeholder="Select a city"
      no-data-text="Could not match a city"
  />
</template>

<script>
import {getCitiesAll, getCitiesByName} from "@/helpers";

export default {
  name: "BaseSelectorCity",
  emits: {
    select: Object,
  },

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

      let safeQuery = query?.trim();
      let request = !safeQuery
          ? getCitiesAll()
          : getCitiesByName(safeQuery);

      request
          .then(cities => this.items = cities)
          .finally(() => this.loading = false);
    },
  },
}
</script>

<style scoped>

</style>
