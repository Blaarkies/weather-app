<template>
  <div class="container">

    <v-expand-transition>
      <div class="login-layout">
        <v-progress-linear v-show="loading"></v-progress-linear>
        
        <v-card class="form-layout">
          <v-card-title class="justify-center">Login</v-card-title>

          <v-text-field
              placeholder="Provide your login user name"
              label="User Name"
              v-model="username"
              :rules="[notEmptyRule]"
              autocomplete="false"
          />

          <v-text-field
              placeholder="Provide your login password"
              label="Password"
              v-model="password"
              :rules="[notEmptyRule]"
              autocomplete="false"
              type="password"
          />
        </v-card>

        <v-btn color="primary" @click="login(username, password)">Login</v-btn>

        <v-divider class="mt-4 mb-4"></v-divider>

        <v-btn color="" @click="login('guest', 'guest')">Login as Guest</v-btn>
      </div>

    </v-expand-transition>

    <v-snackbar
        v-model="open"
        :timeout="7e3"
        color="error"
    >
      {{ message }}
    </v-snackbar>
  </div>
</template>

<script>
import {authService} from '@/services/auth';
import router from '@/router';

export default {
  name: "PageLogin",
  data() {
    return {
      username: '',
      password: '',
      loading: false,
      returnUrl: '',
      message: '',
      open: false,
    }
  },
  created() {
    authService.logout();
    this.returnUrl = this.$route.query.returnUrl || '/';
  },
  methods: {
    login(username, password) {
      if (!(username && password)) {
        return;
      }

      this.loading = true;
      authService.login(username, password)
          .then(
              (user) => {
                this.$root.snackbar.postInfo(`Logged in as ${user.firstName}`);
                return router.push(this.returnUrl);
              },
              error => {
                this.message = error.response?.data?.message;
                this.open = true;
                this.loading = false;
              }
          );
    },

    notEmptyRule(value) {
      if (value === '') {
        return 'Required';
      }

      return true;
    },
  }
}
</script>

<style scoped>
.container {
  justify-items: center;
  align-items: center;
  display: grid;
  height: 100%;
}

.login-layout {
  display: grid;
  width: clamp(300px, 500px, 600px);
}

.form-layout {
  padding: var(--padding-card);
  display: grid;
  gap: var(--gap-tiles);
}
</style>
