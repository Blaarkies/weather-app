export default {
    ADD_FORECAST_FOR_CITY_WEEK(state, {city, weatherList}) {
        state.cityWeather = {
            ...state.cityWeather,
            [city.name]: {
                name: city.name,
                weatherList,
            },
        };
    },
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
    RESET_CITY_WEATHER(state) {
        state.cityWeather = {};
    },
    INITIALISE_STORE(state, additionalState) {
            this.replaceState(
                Object.assign(state, JSON.parse(additionalState)));
    },
}
