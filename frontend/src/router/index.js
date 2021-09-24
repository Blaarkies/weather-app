import Vue from 'vue'
import VueRouter from 'vue-router'
import {authService} from '@/services';

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
        path: '/login',
        name: 'Login',
        component: () => import('@/views/PageLogin'),
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

router.beforeEach((to, from, next) => {
    // redirect to login page if not logged in and trying to access a restricted page
    const publicPages = ['/login'];
    const authRequired = !publicPages.includes(to.path);
    const loggedIn = authService.getLoggedInUser();

    if (authRequired && !loggedIn) {
        return next({
            path: '/login',
            name: 'Login',
            query: { returnUrl: to.path }
        });
    }

    next();
});

export default router;
