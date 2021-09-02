<template>
  <v-app>
    <!--    <v-app-bar
            app
            color="primary"
            dark
        >
          <div class="d-flex align-center">
            <v-img
                alt="Vuetify Logo"
                class="shrink mr-2"
                contain
                src="https://cdn.vuetifyjs.com/images/logos/vuetify-logo-dark.png"
                transition="scale-transition"
                width="40"
            />

            <v-img
                alt="Vuetify Name"
                class="shrink mt-1 hidden-sm-and-down"
                contain
                min-width="100"
                src="https://cdn.vuetifyjs.com/images/logos/vuetify-name-dark.png"
                width="100"
            />
          </div>

          <v-spacer></v-spacer>

          <v-btn
              href="https://github.com/vuetifyjs/vuetify/releases/latest"
              target="_blank"
              text
          >
            <span class="mr-2">Latest Release</span>
            <v-icon>mdi-open-in-new</v-icon>
          </v-btn>
        </v-app-bar>-->

    <v-main>
      <v-btn @click="weathersCall()">
        weathers
      </v-btn>

      <v-btn @click="getCitiesCall()">
        cities
      </v-btn>

      <v-btn @click="openWeatherCall()">
        openweather
      </v-btn>

      <div>{{weathers.join(', ')}}</div>

      <div>{{cities.join(', ')}}</div>

      <HelloWorld/>
    </v-main>
  </v-app>
</template>

<script>
import HelloWorld from './components/HelloWorld';
import axios from "axios";
import {getCitiesByName, getForecast} from "@/common/retrieve-data";

export default {
  name: 'App',

  components: {
    HelloWorld,
  },

  data: () => ({
    weathers: [],
    cities: [],
  }),

  methods: {
    async weathersCall() {
      let response = await getForecast('leipzig');
      this.weathers = response.list
          .flatMap(l => l.weather.map(w => w.description));
    },

    async getCitiesCall() {
      let response = await getCitiesByName('lei');
      this.cities = response;
    },

    async openWeatherCall() {
      let apiKey = "secret";
      let response = await axios.get("https://api.openweathermap.org/data/2.5/forecast?q=leipzig&appid=" + apiKey);
      console.log(response.data);
    }
  }

};
</script>

<style>
main div {
  background-color: #333;
  color: #fff;
  padding: 20px;
  display: grid;
  gap: 10px;

}
</style>
