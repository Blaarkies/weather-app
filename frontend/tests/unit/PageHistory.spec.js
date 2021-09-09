import {config, RouterLinkStub, shallowMount} from '@vue/test-utils'
import PageHistory from '@/views/PageHistory';

describe('PageHistory.vue', () => {

    config.mocks.$store = {
        getters: {
            citiesAndWeeksOfData: ['test-city-a'],
        },
        state: {
            cityWeather: {
                'test-city': {
                    weatherList: [
                        {
                            value: 1,
                            dateTime: new Date(2021, 9, 1)
                        },
                        {
                            value: 2,
                            dateTime: new Date(2021, 9, 2)
                        },
                        {
                            value: 3,
                            dateTime: new Date(2021, 9, 3)
                        },
                        {
                            value: 3,
                            dateTime: new Date(2021, 9, 20)
                        },
                    ],
                },
            },
        },
    };

    it('renders', () => {
        const wrapper = shallowMount(PageHistory,
            {stubs: {RouterLink: RouterLinkStub}});
        expect(wrapper.element).not.toBe(null);
    });

    it('selectHistoryWeather() sets cityWeather to the correct weatherList', async () => {
        const wrapper = shallowMount(PageHistory,
            {stubs: {RouterLink: RouterLinkStub}});

        wrapper.vm.selectHistoryWeather({
            name: 'test-city',
            dt: new Date(2021, 9, 2),
        });
        // expect return dates to be between (2021, 9, 2) and (2021, 9, 7)
        await wrapper.vm.$nextTick();

        let cityWeather = wrapper.vm.$data.cityWeather;
        expect(cityWeather).toMatchObject({
            name: 'test-city',
            date: 'October 2',
        });
        expect(cityWeather.weather.map(w => w.value)).toEqual([2, 3]);
        expect(cityWeather.weather.map(w => w.dateTime)).toEqual([new Date(2021, 9, 2), new Date(2021, 9, 3)]);
    });

    it('cities returns the citiesAndWeeksOfData from store', () => {
        const wrapper = shallowMount(PageHistory,
            {stubs: {RouterLink: RouterLinkStub}});
        expect(wrapper.vm.cities[0]).toBe('test-city-a');
    });

});
