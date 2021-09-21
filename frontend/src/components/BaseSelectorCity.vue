<template>
  <v-autocomplete
      v-model="select"
      :loading="loading"
      :items="items"
      :search-input.sync="search"
      @keydown.enter="select = search"
      cache-items
      flat
      label="City"
      placeholder="Select a city"
      no-data-text="Could not match a city"
      autocomplete="false"
  />
</template>

<script>
import {getCitiesAll, getCitiesByName} from "@/helpers";

/**
 * Input component for city names. Supports free text by pressing enter after typing.
 */
export default {
  name: "BaseSelectorCity",
  emits: {select: Object},

  data() {
    return {
      loading: false,
      items: [],

      /**
       * Current user input value.
       */
      search: null,

      /**
       * Selected option, either null or a full city name.
       */
      select: null,
    }
  },

  created() {
    // initialise the options list
    this.querySelections('');
  },

  watch: {
    search(value) {
      (value || value === '')
      && value !== this.select
      && this.querySelections(value)
    },
    select(value) {
      if (value) {
        this.$emit('select', value);
      }
    },
  },

  methods: {
    /**
     * Retrieve a new list of cities filtered with [query].
     * @param query string
     */
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
