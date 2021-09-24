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

        <v-menu
            absolute
            left
            top
            offset-y
        >
          <template v-slot:activator="{ on, attrs }">
            <v-btn
                v-bind="attrs"
                v-on="on"
                text
                class="text-pascal-case"
                v-if="user"
            >
              {{user.username}}
            </v-btn>
          </template>

          <v-list class="menu-list">
            <v-list-item @click="void 0">Settings</v-list-item>
            <v-list-item @click="logOut()">Log out</v-list-item>
          </v-list>
        </v-menu>

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
import {authService} from '@/services';

export default {
  name: 'TheApp',
  components: {TheSnackbar, ButtonClearCachedStorage},
  computed: {
    /**
     * Toolbar title, matches the name property from current route.
     * @returns {string}
     */
    title() {
      return this.$route.name;
    },
    user() {
      return authService.getLoggedInUser();
    },
  },
  mounted() {
    this.$root.snackbar = this.$refs.snackbar;
  },
  methods: {
    logOut() {
      authService.logout();
      location.reload();
    },
  },
};
</script>

<style lang="scss">
@import 'src/styles/media';

:root {
  --padding-card: 20px;
  --gap-tiles: 10px;
  --gap-content-section: 20px;
  --gap-table-column: 20px;
}

.content-layout {
  background-color: whitesmoke;
  padding: 20px;
  height: 100%;
}

// class to allow layering elements on top of each other,
// while allowing easy positioning for each element
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
  grid-template-columns: max-content auto max-content max-content;

  .app-bar-title-no-truncation {
    .v-app-bar-title__content {
      width: unset;
    }
  }

  .text-pascal-case {
    text-transform: capitalize;
  }
}

.menu-list {
  min-width: 250px;

  > a {
    text-transform: capitalize;
  }
}

// extra large icon used for empty-state pages
.icon-pictogram {
  font-size: 100px !important;
  width: 100%;
}

@media screen and ($media-above-tablets) {
  .content-layout {

    > * {
      margin: auto;
      padding: 20px;
    }
  }
}
</style>
