import {formatDateToDayMonth} from "@/helpers";

export default {
    /**
     * Returns a list city objects, each containing a list of dates that denote a week of available weather data for
     * that city.
     * @param state
     * @returns {{weeks: *[], name: string}[]} {weeks: Date[], name: string}[]
     */
    citiesAndWeeksOfData(state) {
        return Object.entries(state.cityWeather)
            .map(([, {name, weatherList}]) => ({
                name,
                weeks: weatherList.map(w => new Date(w.dateTime))
                    .sort((a, b) => a - b) // sort ascending
                    .filter((dt, i, self) => self.indexOf(dt) === i) // filter for unique elements
                    // dates might not be consecutive 3 hours periods. change this into a list of discrete weeks
                    // each containing 5 days worth of consecutive 3 hour interval data points
                    .reduce((sum, current) => {
                        if (!sum.previous || !sum.previousWeek) { // first iteration cannot be compared to anything yet
                            sum.previous = current;
                            sum.previousWeek = current;
                            sum.weeks.push(sum.getWeekEntry(sum.previous));
                        }

                        let differenceToPreviousEntry = Math.abs(current - sum.previous);
                        let differenceToPreviousWeek = Math.abs(current - sum.previousWeek);

                        let msToHour = 36e5; // factor to convert milliseconds into hours
                        if (differenceToPreviousEntry > msToHour * 3
                            || differenceToPreviousWeek > msToHour * 24 * 5) {
                            // add entry and reset for a new week
                            sum.weeks.push(sum.getWeekEntry(sum.previous));
                            sum.previousWeek = current;
                        }
                        sum.previous = current;

                        return sum;
                    }, {
                        previous: null,
                        weeks: [],
                        previousWeek: null,
                        getWeekEntry: dt => ({
                            name: formatDateToDayMonth(dt),
                            dt,
                        }),
                    })
                    .weeks,
            }));
    },
}
