<template>
  <div>
    <v-card-subtitle>{{ title }}</v-card-subtitle>

    <div class="overlap-container">
      <v-sparkline
          class="pl-8"
          :value="yValues"
          :labels="xLabels"
          :show-labels="false"
          :smooth="10"
          :padding="8"
          :line-width="2"
          :stroke-linecap="'round'"
          :type="'trend'"
          auto-draw
          auto-draw-easing="ease-in-out"
      />

      <div class="y-axis-container">
        <h5
            class="text--disabled"
            v-for="(label, i) in yLabels"
            :key="label + i"
        >
          {{ label }}&nbsp;{{ unitOfMeasure }}
        </h5>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "WeatherSparkline",
  props: {
    weather: {
      type: Array,
      default: () => ([]),
    },

    title: String,

    unitOfMeasure: String,

    // callback that selects the graph y-values
    valueSelector: {
      type: Function,
      default: () => ((i) => i),
    },
  },

  data: () => ({
    daysOfWeek: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
  }),

  computed: {
    yValues() {
      return this.weather.map(this.valueSelector);
    },

    xLabels() {
      if (!this.weather.length) {
        return [];
      }
      let firstDate = new Date(this.weather[0].dateTime);
      return this.weather
          .map(w => [
            new Date(w.dateTime),
            (new Date(w.dateTime) - firstDate) / 3.6e6 // the amount hours since firstDate
          ])
          // create an array where the day's name populates the first index for every 24 hours,
          // and whitespace strings to the rest
          .reduce((sum, [dt, current]) => {
                let output = ' ';
                sum.counter += current - sum.previous;
                if (sum.counter >= 24) {
                  sum.counter = 0;
                  output = this.daysOfWeek[dt.getDay()]
                }

                sum.previous = current;
                sum.output.push(output);
                return sum;
              },
              {previous: 0, counter: 0, output: []})
          .output;
    },

    yLabels() {
      if (!this.weather.length) {
        return [];
      }
      let maxIndex = this.weather.length - 1;
      let indexesToInspect = [0, .5, 1].map(r => Math.floor(maxIndex * r));
      let weatherSortedByValueSelector = this.weather
          .slice().sort((a, b) => this.valueSelector(b) - this.valueSelector(a));
      return indexesToInspect.map(inspectIndex =>
          this.valueSelector(weatherSortedByValueSelector[inspectIndex]).toFixed(0));
    },
  },
}
</script>

<style scoped>
.y-axis-container {
  display: grid;
}
</style>
