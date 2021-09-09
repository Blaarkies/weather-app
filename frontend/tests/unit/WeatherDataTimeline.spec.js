import {shallowMount} from '@vue/test-utils'
import WeatherDataTimeline from '@/components/WeatherDataTimeline';

describe('WeatherDataTimeline.vue', () => {

    it('renders', () => {
        const wrapper = shallowMount(WeatherDataTimeline);
        expect(wrapper.element).not.toBe(null);
    });

});
