import {shallowMount} from '@vue/test-utils'
import SparklineWeather from '@/components/SparklineWeather';

describe('SparklineWeather.vue', () => {

    let defaultProps = {
        weather: [
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
        ],
        valueSelector: w => w.value,
    };

    it('renders', () => {
        const wrapper = shallowMount(SparklineWeather, {propsData: defaultProps});
        expect(wrapper.element).not.toBe(null);
    });

    it('yValues should be correct', async () => {
        const wrapper = shallowMount(SparklineWeather, {propsData: defaultProps});

        expect(wrapper.vm.yValues).toEqual([1,2,3]);
    });

    it('xLabels should be correct', async () => {
        const wrapper = shallowMount(SparklineWeather, {propsData: defaultProps});

        expect(wrapper.vm.xLabels).toEqual([' ', 'Saturday', 'Sunday']);
    });

});
