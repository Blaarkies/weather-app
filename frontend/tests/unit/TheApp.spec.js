import {config, shallowMount} from '@vue/test-utils'
import TheApp from '@/TheApp';

describe('TheApp.vue', () => {

    config.mocks.$route = {
        name: 'test-route',
    };

    it('renders', () => {
        const wrapper = shallowMount(TheApp,
            {stubs: ['router-view']});
        expect(wrapper.element).not.toBe(null);
    });

    it('title matches the current route', async () => {
        const wrapper = shallowMount(TheApp,
            {stubs: ['router-view']});
        await wrapper.vm.$nextTick();

        expect(wrapper.vm.title).toBe('test-route');
    });

    it('mounts the snackbar', async () => {
        const wrapper = shallowMount(TheApp,
            {stubs: ['router-view']});
        await wrapper.vm.$nextTick();

        expect(wrapper.vm.$root.snackbar).not.toBe(null);
    });

});
