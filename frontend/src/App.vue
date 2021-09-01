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
      <v-btn
          v-on:click="buttonCall()"
          color="primary"
      >
        api request
      </v-btn>

      <div>{{weathers.map(w => w.summary ).join(' ')}}</div>

      <HelloWorld/>
    </v-main>
  </v-app>
</template>

<script>
import HelloWorld from './components/HelloWorld';
import axios from "axios";

export default {
  name: 'App',

  components: {
    HelloWorld,
  },

  data: () => ({
    weathers: [],
  }),

  methods: {
    buttonCall() {
      axios.get('https://localhost:5001/api/WeatherForecast')
          .then(response => {
            this.weathers = response.data;
          });
    },
  }

};
</script>

<style>
main {
  background-color: gray !important;
}
</style>
