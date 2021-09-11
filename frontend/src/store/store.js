import Vue from 'vue';
import Vuex from 'vuex';
import getters from "@/store/getters";
import actions from "@/store/actions";
import mutations from "@/store/mutations";

Vue.use(Vuex);

export const store = new Vuex.Store({
    state: {
        cityWeather: {},
    },
    getters,
    actions,
    mutations,
});

export const storeStorageKey = 'crystal-weather-app-store';

syncLocalStorageToStore();

/**
 * Loads local storage store into app store, and sets up local storage updates on each store mutations.
 */
function syncLocalStorageToStore() {
    store.dispatch('loadStoreFromCache', {
        cachedStore: localStorage.getItem(storeStorageKey)
    });

    store.subscribe((mutation, state) =>
        localStorage.setItem(storeStorageKey, JSON.stringify(state)));
}
