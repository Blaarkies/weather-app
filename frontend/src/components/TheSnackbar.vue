<template>
  <v-snackbar
      v-model="open"
      :timeout="duration"
      :color="color"
  >
    {{ message }}
  </v-snackbar>
</template>

<script>
export default {
  name: "TheSnackbar",
  data: () => ({
    open: null,
    duration: null,
    color: null,
    message: null,
  }),
  methods: {
    post({duration, color, message}) {
      if (!message) {
        return;
      }
      this.open = true;
      this.duration = duration ?? 10e3;
      this.color = color ?? 'primary';
      this.message = message;
    },

    postWarning({duration, message}) {
      this.post({duration, color: 'orange darken-2', message});
    },

    postError({duration, message}) {
      this.post({duration, color: 'error', message});
    },

    postInfo({duration, message}) {
      this.post({duration, color: 'primary', message});
    },
  },
}
</script>

<style scoped>

</style>
