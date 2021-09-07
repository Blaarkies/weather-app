<template>
  <v-app>
    <v-app-bar
        app
        color="primary"
        dark
    >
      <div class="toolbar-layout">
        <v-menu offset-y>
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
      </div>
    </v-app-bar>

    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
export default {
  name: 'App',
  computed: {
    title() {
      return this.$route.name;
    },
  },
};
</script>

<style lang="scss">
:root {
  --padding-card: 20px;
  --gap-tiles: 10px;
  --gap-content-section: 20px;
  --gap-table-column: 20px;
}

main > div {
  background-color: whitesmoke;
  padding: 20px;
  display: grid;
  grid-auto-rows: max-content;
  gap: 10px;
  max-width: 600px !important;

}

.overlap-container {
  display: grid;

  > * {
    grid-column: 1/1;
    grid-row: 1/1;
  }
}

.toolbar-layout {
  display: flex;
  gap: var(--gap-content-section);
  align-items: center;

  .app-bar-title-no-truncation {
    .v-app-bar-title__content {
      width: unset;
    }
  }
}

.menu-list > a {
  text-transform: capitalize;
}

.icon-pictogram {
  font-size: 100px !important;
  width: 100%;
}
</style>
