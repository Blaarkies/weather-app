import {storeStorageKey} from "@/store/store";

export default {
    /**
     * Given a city name and list of weather points, adds this into the cityWeather. If the city already has an
     * entry in the store, it updates that entry with the new info.
     * @param store
     * @param payload
     * @param payload.city city name
     * @param payload.weatherList list of weather points
     * @returns {Promise<void>}
     */
    async saveCityWeather(store, {city, weatherList}) {
        let cityEntryExists = store.state.cityWeather[city.name];
        if (cityEntryExists) {
            store.commit('UPDATE_FORECAST_FOR_CITY_WEEK', {city, weatherList});
        } else {
            store.commit('ADD_FORECAST_FOR_CITY_WEEK', {city, weatherList});
        }
    },

    /**
     * Resets the store of weather data.
     * @param store
     * @returns {Promise<void>}
     */
    async resetCityWeather(store) {
        store.commit('RESET_CITY_WEATHER');
    },

    /**
     * Load data from local cache into store.
     * @param store
     * @param cachedStore
     * @returns {Promise<void>}
     */
    async loadStoreFromCache(store, {cachedStore}) {
        if (cachedStore) {
            store.commit('INITIALISE_STORE', cachedStore);
        }
    },

    /**
     * Resets the store & browser storage cache of weather data.
     * @param store
     * @returns {Promise<void>}
     */
    async resetCachedStore(store) {
        localStorage.removeItem(storeStorageKey);
        store.dispatch('resetCityWeather');
    },
}
