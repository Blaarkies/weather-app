import {shallowMount} from '@vue/test-utils'
import CurrentWeather from '@/components/CurrentWeather';

describe('CurrentWeather.vue', () => {

    it('renders', () => {
        const wrapper = shallowMount(CurrentWeather);
        expect(wrapper.element).not.toBe(null);
    });

});
