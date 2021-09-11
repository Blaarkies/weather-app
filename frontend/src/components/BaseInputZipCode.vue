<template>
  <v-text-field
      placeholder="German city zip code"
      label="Zip Code"
      v-model="zipCode"
      :rules="[germanZipCodeRule]"
      @input="tryToEmit($event)"
      autocomplete="false"
  />
</template>

<script>
/**
 * Input component for zip codes.
 */
export default {
  name: "BaseInputZipCode",
  emits: {inputZipCode: Number},

  data: () => ({
    zipCode: null,
  }),

  methods: {
    /**
     * Attempts to emit the user input value, if it passes validation.
     */
    tryToEmit(value) {
      if (value && this.germanZipCodeRule(value) === true) {
        this.$emit('inputZipCode', value);
      }
    },

    /**
     * Validation rules according to german city zipcode rules.
     */
    germanZipCodeRule(zipCode) {
      // Some numbers allowed by this are not real zip codes, but the alternative is to load a list of 2000 zip codes.
      if (zipCode?.length === 0) {
        return true;
      }
      // 5 digit numbers starting from 01001 up to 99998.
      let numberCode = Number(zipCode);
      if (isNaN(numberCode)) {
        return 'Must be a number';
      }
      if (zipCode?.length !== 5) {
        return 'Zip codes must be 5 digits long';
      }
      if (numberCode < 1001) {
        return 'Zip codes range from 01001 to 99998';
      }
      if (numberCode > 99998) {
        return 'Zip codes range from 01001 to 99998';
      }
      return true;
    },
  },
}
</script>

<style scoped>

</style>
