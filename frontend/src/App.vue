<template>
  <v-app>
    <v-app-bar
        app
        color="primary"
        dark
    >
      <div class="toolbar-layout">
        <v-menu
            absolute
            left
            top
            offset-y
        >
          <template v-slot:activator="{ on, attrs }">
            <v-btn
                icon
                v-bind="attrs"
                v-on="on"
            >
              <v-icon>mdi-menu</v-icon>
            </v-btn>
          </template>

          <v-list class="menu-list">
            <v-list-item to="/forecast">forecast</v-list-item>
            <v-list-item to="/history">history</v-list-item>
          </v-list>
        </v-menu>

        <v-app-bar-title class="app-bar-title-no-truncation">{{ title }}</v-app-bar-title>

        <ButtonClearCachedStorage/>
      </div>
    </v-app-bar>

    <v-main>
      <div class="content-layout">
        <router-view></router-view>
      </div>
    </v-main>

    <TheSnackbar ref="snackbar"/>
  </v-app>
</template>

<script>
import ButtonClearCachedStorage from "@/components/ButtonClearCachedStorage";
import TheSnackbar from "@/components/TheSnackbar";

export default {
  name: 'App',
  components: {TheSnackbar, ButtonClearCachedStorage},
  computed: {
    title() {
      return this.$route.name;
    },
  },
  mounted() {
    this.$root.snackbar = this.$refs.snackbar;
  },
};
</script>

<style lang="scss">
:root {
  --padding-card: 20px;
  --gap-tiles: 10px;
  --gap-content-section: 20px;
  --gap-table-column: 20px;
  --color-warn: #dc3545;
}

.content-layout {
  background-color: whitesmoke;
  padding: 20px;
  height: 100%;
  //display: grid;
  //grid-auto-rows: max-content;
  //gap: 10px;
  //max-width: 600px !important;

}

.overlap-container {
  display: grid;

  > * {
    grid-column: 1/1;
    grid-row: 1/1;
  }
}

.toolbar-layout {
  width: 100%;
  display: grid;
  gap: var(--gap-content-section);
  align-items: center;
  grid-template-columns: max-content auto max-content;

  .app-bar-title-no-truncation {
    .v-app-bar-title__content {
      width: unset;
    }
  }
}

.menu-list {
  min-width: 250px;

  > a {
    text-transform: capitalize;
  }
}

.icon-pictogram {
  font-size: 100px !important;
  width: 100%;
}
</style>
