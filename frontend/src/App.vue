<template>
  <v-app>
    <v-app-bar
        app
        color="primary"
    >
    </v-app-bar>

    <v-main>
      <PageWeatherForecasts></PageWeatherForecasts>
    </v-main>
  </v-app>
</template>

<script>
import PageWeatherForecasts from "@/components/PageWeatherForecasts";

export default {
  name: 'App',
  components: {
    PageWeatherForecasts,
  },
};
</script>

<style>
:root {
  --padding-card: 20px;
  --gap-tiles: 10px;
  --gap-content-section: 20px;
  --gap-table-column: 20px;
}

main > div {
  background-color: whitesmoke;
  /*color: #fff;*/
  padding: 20px;
  display: grid;
  grid-auto-rows: max-content;
  gap: 10px;
  max-width: 600px !important;

}

.overlap-container {
  display: grid;
}

.overlap-container > * {
  grid-column: 1/1;
  grid-row: 1/1;
}
</style>
