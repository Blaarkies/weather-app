import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter);

const routes = [
    {
        path: '/forecast',
        name: 'Weather Forecast',
        component: () => import('@/views/PageWeatherForecasts'),
    },
    {
        path: '/history',
        name: 'Historic Weather',
        component: () => import('@/views/PageHistory'),
    },
    {
        path: '/',
        redirect: {path: '/forecast'},
    },
    {
        path: '*',
        component: () => '<h1>404 Not Found</h1>',
    },
];

const router = new VueRouter({
    routes,
});

export default router;
