<template>
  <div class="confirmer-container">
    <v-tooltip
        left
        :nudge-left="shouldClickAgain ? 10 : -50"
        nudge-bottom="35"
        :color="shouldClickAgain ? 'error' : ''"
        :key="tooltipKey"
    >
      <template v-slot:activator="{ on }">
        <div
            class="overlap-container button-container"
            v-on="shouldClickAgain ? {} : on"
        >

          <v-fade-transition>
            <v-progress-circular
                v-show="waitValue"
                :value="waitValue"
                color="error"
                size="40"
            />
          </v-fade-transition>

          <v-btn
              @click="tryToConfirm()"
              icon
              :disabled="!hasDataInCache"
          >
            <v-icon>mdi-database-remove-outline</v-icon>
          </v-btn>
        </div>
      </template>
      <span v-text="shouldClickAgain
      ? 'Tap icon again to confirm delete'
      : 'Clear cached storage'"/>
    </v-tooltip>
  </div>
</template>

<script>
import {finalize, interval, map, Subject, takeUntil, takeWhile} from "rxjs";

/**
 * Confirmation button requiring 2 clicks in 5 seconds to confirm action.
 */
export default {
  name: "ButtonClearCachedStorage",
  data: () => ({
    /**
     * True when the button is in a waiting state. During this time, a second click will call the action function.
     */
    shouldClickAgain: null,

    /**
     * Number used to slowly tick up the progress bar animation to show time passing by.
     */
    waitValue: null,

    unsubscribe$: new Subject(),
    tooltipKey: 0,
    snackBarOpen: null,
    snackBarColor: null,
    snackBarMessage: null,
  }),
  computed: {
    /**
     * Checks if the local storage cache has any weather data inside.
     * @returns {boolean}
     */
    hasDataInCache() {
      return this.$store.getters.citiesAndWeeksOfData.length > 0;
    },
  },
  methods: {
    /**
     * Called by button on click. From an idle state, the first click will cause this to go into a waiting state for
     * 5 seconds. If clicked again during the waiting state, the clearCachedStorage() function is actioned.
     * If left to run out of time, it will return to an idle state without actioning the clearCachedStorage() function.
     */
    tryToConfirm() {
      if (!this.waitValue) {
        this.shouldClickAgain = true;
        this.waitValue = 1;
        // start a timer that periodically sends a new larger integer down the "pipe"
        interval(200).pipe(
            map(i => i * 5),
            finalize(() => {
              this.waitValue = null;
              this.shouldClickAgain = null;
              this.$root.snackbar.postInfo({message: 'Nothing was deleted'});
            }),
            takeWhile(i => i <= 115),
            takeUntil(this.unsubscribe$))
            .subscribe(i => this.waitValue = i);

      } else {
        if (this.waitValue >= 0) {
          this.unsubscribe$.next(); // stops any running timers
          this.clearCachedStorage();
        }
      }
    },

    /**
     * Clear the local cache storage and informs the user thereof.
     */
    clearCachedStorage() {
      this.$store.dispatch('resetCachedStore');
      this.$root.snackbar.postWarning({message: 'Cleared weather data in cached storage'})
    },
  },
  watch: {
    shouldClickAgain(value) {
      if (!value) {
        this.tooltipKey++; // reset the tooltip element to close it
      }
    },
  },
  destroyed() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
</script>

<style scoped>
.confirmer-container {
  border-radius: 100px;
}

.button-container {
  width: max-content;
  align-items: center;
  justify-items: center;
}
</style>
