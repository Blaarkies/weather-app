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
/**
 * Global snackbar to provide user feedback on actions, warnings, and errors.
 */
export default {
  name: "TheSnackbar",
  data: () => ({
    open: null,
    duration: null,
    color: null,
    message: null,
  }),
  methods: {
    /**
     * Posts a new snackbar message to the user, with given properties.
     * @param object arg
     * @param arg.duration number of milliseconds to show the message
     * @param arg.color color of message {@link https://vuetifyjs.com/en/styles/colors/ material colors}
     * @param arg.message string message to display
     */
    post({duration = 10e3, color = 'primary', message}) {
      if (!message) {
        return;
      }
      this.open = true;
      this.duration = duration;
      this.color = color;
      this.message = message;
    },

    /**
     * Posts a new snackbar warning message to the user, with given properties.
     * @param object arg
     * @param arg.duration number of milliseconds to show the message
     * @param arg.message string message to display
     */
    postWarning({duration, message}) {
      this.post({duration, color: 'orange darken-2', message});
    },

    /**
     * Posts a new snackbar error message to the user, with given properties.
     * @param object arg
     * @param arg.duration number of milliseconds to show the message
     * @param arg.message string message to display
     */
    postError({duration, message}) {
      this.post({duration, color: 'error', message});
    },

    /**
     * Posts a new snackbar informational message to the user, with given properties.
     * @param object arg
     * @param arg.duration number of milliseconds to show the message
     * @param arg.message string message to display
     */
    postInfo({duration, message}) {
      this.post({duration, color: 'primary', message});
    },
  },
}
</script>

<style scoped>

</style>
