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

persistStoreToLocalStorage();

function persistStoreToLocalStorage() {
    store.subscribe((mutation, state) =>
        localStorage.setItem(storeStorageKey, JSON.stringify(state)));

    store.dispatch('loadStoreFromCache', {
        cachedStore: localStorage.getItem(storeStorageKey)
    });
}
