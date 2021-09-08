import {formatDateToDayMonth} from "@/helpers";

export default {
    citiesAndWeeksOfData(state) {
        return Object.entries(state.cityWeather)
            .map(([, {name, weatherList}]) => ({
                name,
                weeks: weatherList.map(w => new Date(w.dateTime))
                    .sort((a, b) => a - b) // sort ascending
                    .filter((dt, i, self) => self.indexOf(dt) === i)
                    .reduce((sum, current) => {
                        if (!sum.previous || !sum.previousWeek) {
                            sum.previous = current;
                            sum.previousWeek = current;
                            sum.weeks.push(sum.getWeekEntry(sum.previous));
                        }

                        let differenceToPreviousEntry = Math.abs(current - sum.previous);
                        let differenceToPreviousWeek = Math.abs(current - sum.previousWeek);

                        let msToHour = 36e5;
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
