<template>
  <div>
    <v-card-subtitle>{{ title }}</v-card-subtitle>

    <div class="overlap-container">
      <v-sparkline
          class="pl-10"
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
import {formatDateToWeekDay} from '@/helpers';

/**
 * Generic graphing display for data points, adding y-axis labels to denote min, max, and median, and x-axis labels
 * to denote the closest day of the week.
 */
export default {
  name: "SparklineWeather",
  props: {
    /**
     * List of weather detail objects to be used as data points.
     */
    weather: {
      type: Array,
      default: () => ([]),
    },

    /**
     * Title of graph.
     */
    title: String,

    /**
     * Unit of measure used for the y-axis labels.
     */
    unitOfMeasure: String,

    /**
     * Callback that selects the graph y-axis label names.
     */
    valueSelector: {
      type: Function,
      default: () => ((i) => i),
    },
  },

  computed: {
    /**
     * Values used on the vertical axis, like temperature, wind speed, or humidity.
     * @returns {number[]}
     */
    yValues() {
      return this.weather.map(this.valueSelector);
    },

    /**
     * Finds the day of the week name for each data point, and attempts to show the midnight points of the week
     * with the name of the day. The final list contains mostly empty strings, with only specific elements named
     * as week day strings.
     * @returns {string[]}
     */
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
                  output = formatDateToWeekDay(dt);
                }

                sum.previous = current;
                sum.output.push(output);
                return sum;
              },
              {previous: 0, counter: 0, output: []})
          .output;
    },

    /**
     * Finds the Minimum, Median, and Maximum of all data points, and uses their values as y-axis labels
     * @returns {string[]}
     */
    yLabels() {
      if (!this.weather.length) {
        return [];
      }
      let maxIndex = this.weather.length - 1;
      let indexesToInspect = [0, .5, 1].map(r => Math.floor(maxIndex * r));
      let weatherSortedByValueSelector = this.weather.slice()
          .sort((a, b) => this.valueSelector(b) - this.valueSelector(a));
      return indexesToInspect.map(inspectIndex =>
          this.valueSelector(weatherSortedByValueSelector[inspectIndex]).toFixed(0));
    },
  },
}
</script>

<style scoped>
.y-axis-container {
  display: grid;
  justify-items: end;
  justify-self: start;
  width: 50px;
  margin-left: -4px;
}
</style>
