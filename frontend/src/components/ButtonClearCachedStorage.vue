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

export default {
  name: "ButtonClearCachedStorage",
  data: () => ({
    shouldClickAgain: null,
    waitValue: null,
    unsubscribe$: new Subject(),
    tooltipKey: 0,
    snackBarOpen: null,
    snackBarColor: null,
    snackBarMessage: null,
  }),
  computed: {
    hasDataInCache() {
      return this.$store.getters.citiesAndWeeksOfData.length > 0;
    },
  },
  methods: {
    tryToConfirm() {
      if (!this.waitValue) {
        this.shouldClickAgain = true;
        this.waitValue = 1;
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
          this.unsubscribe$.next();
          this.clearCachedStorage();
        }
      }
    },

    clearCachedStorage() {
      this.$store.dispatch('resetCachedStore');
      this.$root.snackbar.postWarning({message: 'Cleared weather data in cached storage'})
    },
  },
  watch: {
    shouldClickAgain(value) {
      if (!value) {
        this.tooltipKey++
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
