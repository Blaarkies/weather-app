import {shallowMount} from '@vue/test-utils'
import BaseSelectorCity from '@/components/BaseSelectorCity';
import {getCitiesAll, getCitiesByName} from '@/helpers';

jest.mock('@/helpers');

describe('BaseSelectorCity.vue', () => {

    getCitiesAll.mockImplementation(() =>
        Promise.resolve(['cityA', 'cityB']));
    getCitiesByName.mockImplementation((search) =>
        Promise.resolve(['cityA', 'cityB']
            .filter(c => c.includes(search))));

    it('renders', () => {
        const wrapper = shallowMount(BaseSelectorCity);
        expect(wrapper.element).not.toBe(null);
    });

    it('rendered with list of cities', async () => {
        const wrapper = shallowMount(BaseSelectorCity);

        await wrapper.vm.$nextTick();
        let items = wrapper.vm.$data.items;
        expect(items).toEqual(['cityA', 'cityB']);
    });

    it('changes to search will call the correct endpoints', async () => {
        const wrapper = shallowMount(BaseSelectorCity);

        getCitiesAll.mockClear();
        getCitiesByName.mockClear();

        wrapper.vm.$data.search = 'B';
        await wrapper.vm.$nextTick();

        expect(getCitiesAll).not.toHaveBeenCalled();
        expect(getCitiesByName).toHaveBeenCalled();

        getCitiesAll.mockClear();
        getCitiesByName.mockClear();

        wrapper.vm.$data.search = '';
        await wrapper.vm.$nextTick();

        expect(getCitiesAll).toHaveBeenCalled();
        expect(getCitiesByName).not.toHaveBeenCalled();
    });

    it('changes to select will emit to select', async () => {
        const wrapper = shallowMount(BaseSelectorCity);

        wrapper.vm.$data.select = 'test-city';
        await wrapper.vm.$nextTick();

        expect(wrapper.emitted().select[0][0]).toBe('test-city');
    });

});
