import {shallowMount} from '@vue/test-utils'
import TheSnackbar from '@/components/TheSnackbar';

describe('TheSnackbar.vue', () => {

    it('renders', () => {
        const wrapper = shallowMount(TheSnackbar);
        expect(wrapper.element).not.toBe(null);
    });

    it('post() sets the config properties to match the arguments', async () => {
        const wrapper = shallowMount(TheSnackbar);

        wrapper.vm.post({
            duration: 42,
            color: 'test-color',
            message:'test-message',
        });

        expect(wrapper.vm.$data).toMatchObject({
            duration: 42,
            color: 'test-color',
            message:'test-message',
        });
    });

});
