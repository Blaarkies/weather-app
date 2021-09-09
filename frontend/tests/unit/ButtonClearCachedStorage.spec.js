import {shallowMount} from '@vue/test-utils'
import ButtonClearCachedStorage from '@/components/ButtonClearCachedStorage';
import {firstValueFrom, timer} from 'rxjs';

describe('ButtonClearCachedStorage.vue', () => {

    it('renders', () => {
        const wrapper = shallowMount(ButtonClearCachedStorage);
        expect(wrapper.element).not.toBe(null);
    });

    it('clearCachedStorage() will dispatch action and post snackbar warning', async () => {
        const wrapper = shallowMount(ButtonClearCachedStorage);

        wrapper.vm.$store = {dispatch: jest.fn()};
        wrapper.vm.$root = {snackbar: {postWarning: jest.fn()}};
        jest.spyOn(wrapper.vm.$store, 'dispatch');
        jest.spyOn(wrapper.vm.$root.snackbar, 'postWarning');

        wrapper.vm.clearCachedStorage();
        await wrapper.vm.$nextTick();

        expect(wrapper.vm.$store.dispatch).toHaveBeenCalled();
        expect(wrapper.vm.$root.snackbar.postWarning).toHaveBeenCalled();
    });

    describe('tryToConfirm()', () => {

        function setupMocksForTryToConfirm(wrapper) {
            wrapper.vm.$root = {snackbar: {postInfo: jest.fn()}};
            jest.spyOn(wrapper.vm.$root.snackbar, 'postInfo');
            jest.spyOn(wrapper.vm, 'clearCachedStorage').mockReturnValue(void 0);
        }

        it('with null waitValue, will start the timer, but not clearCachedStorage', async () => {
            const wrapper = shallowMount(ButtonClearCachedStorage);
            setupMocksForTryToConfirm(wrapper);

            wrapper.vm.tryToConfirm();
            await wrapper.vm.$nextTick();

            let data = wrapper.vm.$data;
            expect(data.shouldClickAgain).toBe(true);
            expect(data.waitValue).toBeGreaterThan(0);

            let oldWaitValue = data.waitValue;
            await firstValueFrom(timer(500));

            expect(oldWaitValue).not.toEqual(data.waitValue);
            expect(oldWaitValue).toBeLessThan(data.waitValue);
        });

        it('timer running out does not clearCachedStorage, but posts to snackbar', async () => {
            const wrapper = shallowMount(ButtonClearCachedStorage);
            setupMocksForTryToConfirm(wrapper);

            jest.useFakeTimers();

            wrapper.vm.tryToConfirm();
            await wrapper.vm.$nextTick();

            let data = wrapper.vm.$data;
            expect(data.waitValue).not.toBe(null);
            expect(data.shouldClickAgain).not.toBe(null);

            jest.runAllTimers();

            expect(data.waitValue).toBe(null);
            expect(data.shouldClickAgain).toBe(null);
            expect(wrapper.vm.$root.snackbar.postInfo).toHaveBeenCalled();
        });

        it('when tapped twice in short time period, it calls clearCachedStorage', async () => {
            const wrapper = shallowMount(ButtonClearCachedStorage);
            setupMocksForTryToConfirm(wrapper);

            wrapper.vm.tryToConfirm();
            await wrapper.vm.$nextTick();

            expect(wrapper.vm.clearCachedStorage).not.toHaveBeenCalled();

            wrapper.vm.tryToConfirm();
            await wrapper.vm.$nextTick();

            expect(wrapper.vm.clearCachedStorage).toHaveBeenCalled();
        });
    });
});
