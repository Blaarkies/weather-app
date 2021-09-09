import {mount, shallowMount} from '@vue/test-utils'
import BaseInputZipCode from "@/components/BaseInputZipCode";

describe('BaseInputZipCode.vue', () => {

    it('renders', () => {
        const wrapper = shallowMount(BaseInputZipCode);
        expect(wrapper.element).not.toBe(null);
    });

    it('when zipCode is invalid, it does not emit', async () => {
        const wrapper = mount(BaseInputZipCode);

        const input = wrapper.find('input');
        await input.setValue(123);

        expect(wrapper.emitted()).toEqual({});
    });

    it('when zipCode is valid, it does emit zipCode', async () => {
        const wrapper = mount(BaseInputZipCode);

        const input = wrapper.find('input');
        await input.setValue(50000);

        expect(wrapper.emitted().inputZipCode[0][0]).toBe('50000');
    });

});
