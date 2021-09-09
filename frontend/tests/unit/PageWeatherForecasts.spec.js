import {shallowMount} from '@vue/test-utils'
import PageWeatherForecasts from '@/views/PageWeatherForecasts';

jest.mock('@/helpers', () => ({
    getForecast: () => Promise.resolve({
        city: 'test-city',
        weatherList: [],
    }),
    getMessageFromError: (message) => message,
}));

describe('PageWeatherForecasts.vue', () => {

    it('renders', () => {
        const wrapper = shallowMount(PageWeatherForecasts);
        expect(wrapper.element).not.toBe(null);
    });

    function setupMocksForSnackbar(wrapper) {
        wrapper.vm.$root = {snackbar: {postError: jest.fn()}};
        jest.spyOn(wrapper.vm.$root.snackbar, 'postError');
    }

    it('setSnackBarMessage() will post error to snackbar', async () => {
        const wrapper = shallowMount(PageWeatherForecasts);
        setupMocksForSnackbar(wrapper);

        wrapper.vm.setSnackBarMessage('test-error');
        await wrapper.vm.$nextTick();

        expect(wrapper.vm.$root.snackbar.postError).toHaveBeenCalled();
    });

    it('setWeatherByZipCode() will call setWeather()', async () => {
        const wrapper = shallowMount(PageWeatherForecasts);

        jest.spyOn(wrapper.vm, 'setWeather').mockReturnValue(void 0);

        wrapper.vm.setWeatherByZipCode('test-error');
        await wrapper.vm.$nextTick();

        expect(wrapper.vm.setWeather).toHaveBeenCalled();
    });

    it('setWeather() will set weather & dispatch store action', async () => {
        const wrapper = shallowMount(PageWeatherForecasts);

        wrapper.vm.$store = {dispatch: () => void 0};
        jest.spyOn(wrapper.vm.$store, 'dispatch');

        wrapper.vm.setWeather({name: 'test-city'}, [{name: 'test-weather'}]);
        await wrapper.vm.$nextTick();

        expect(wrapper.vm.$data.city.name).toBe('test-city');
        expect(wrapper.vm.$data.weather[0].name).toBe('test-weather');
        expect(wrapper.vm.$store.dispatch).toHaveBeenCalled();
    });

});
