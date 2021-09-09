import Vue from 'vue';
import App from './TheApp.vue';
import vuetify from './plugins/vuetify';
import router from './router';
import {store} from './store/store';

Vue.config.productionTip = false;

new Vue({
    vuetify,
    router,
    render: h => h(App),
    store,
}).$mount('#app')
