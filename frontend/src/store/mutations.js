export default {
    /**
     * Add entry for city into store.
     * @param state
     * @param payload
     * @param payload.city city name
     * @param payload.weatherList list of weather objects
     */
    ADD_FORECAST_FOR_CITY_WEEK(state, {city, weatherList}) {
        state.cityWeather = {
            ...state.cityWeather,
            [city.name]: {
                name: city.name,
                weatherList,
            },
        };
    },

    /**
     * Updates an existing entry for [city] by overwriting its data. Previous weather data is preserved, unless a
     * new data point matches the date time.
     * @param state
     * @param payload
     * @param payload.city city name
     * @param payload.weatherList list of weather objects
     */
    UPDATE_FORECAST_FOR_CITY_WEEK(state, {city, weatherList}) {
        let listData = state.cityWeather[city.name].weatherList;
        let weatherListMap = new Map(listData.map(w => [w.dateTime, w]));
        weatherList.forEach(w => weatherListMap.set(w.dateTime, w));

        state.cityWeather = {
            ...state.cityWeather,
            [city.name]: {
                ...state.cityWeather[city.name],
                weatherList: Array.from(weatherListMap.values()),
            },
        };
    },

    /**
     * Sets weather data to empty object.
     * @param state
     */
    RESET_CITY_WEATHER(state) {
        state.cityWeather = {};
    },

    /**
     * Adds existing state from a different source into the store. Used to load up local storage cache into the store
     * upon starting the app.
     * @param state
     * @param additionalState state to be added in addition to the current state
     */
    INITIALISE_STORE(state, additionalState) {
            this.replaceState(
                Object.assign(state, JSON.parse(additionalState)));
    },
}
