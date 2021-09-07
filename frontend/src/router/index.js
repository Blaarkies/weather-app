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
        component: () => '<div>404 Not Found</div>',
    },
];

const router = new VueRouter({
    routes
});

export default router;
