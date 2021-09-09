import {shallowMount} from '@vue/test-utils'
import SelectorHistoryWeek from '@/components/SelectorHistoryWeek';

describe('SelectorHistoryWeek.vue', () => {

    it('renders', () => {
        const wrapper = shallowMount(SelectorHistoryWeek);
        expect(wrapper.element).not.toBe(null);
    });

    it('selectCityAtDate() emits the name & dateTime as select', async () => {
        const wrapper = shallowMount(SelectorHistoryWeek);

        wrapper.vm.selectCityAtDate('test-name', 'test-dt');

        expect(wrapper.emitted().select[0][0]).toEqual({
            name: 'test-name',
            dt: 'test-dt',
        });
    });

});
