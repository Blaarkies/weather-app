import {storeStorageKey} from "@/store/store";

export default {
    async saveCityWeather(store, {city, weatherList}) {
        let cityEntryExists = store.state.cityWeather[city.name];
        if (cityEntryExists) {
            store.commit('UPDATE_FORECAST_FOR_CITY_WEEK', {city, weatherList});
        } else {
            store.commit('ADD_FORECAST_FOR_CITY_WEEK', {city, weatherList});
        }
    },

    async resetCityWeather(store) {
        store.commit('RESET_CITY_WEATHER');
    },

    async loadStoreFromCache(store, {cachedStore}) {
        if (cachedStore) {
            store.commit('INITIALISE_STORE', cachedStore);
        }
    },

    async resetCachedStore(store) {
        localStorage.removeItem(storeStorageKey);
        store.dispatch('resetCityWeather');
    },
}
