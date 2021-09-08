<template>
  <v-text-field
      placeholder="German city zip code"
      label="Zip Code"
      v-model="zipCode"
      :rules="[germanZipCodeRule]"
  />
</template>

<script>
export default {
  name: "BaseInputZipCode",
  emits: {inputZipCode: Number},

  data: () => ({
    zipCode: null,
  }),

  watch: {
    zipCode(value) {
      if (value && this.germanZipCodeRule(value) === true) {
        this.$emit('inputZipCode', value);
      }
    },
  },

  methods: {
    germanZipCodeRule(zipCode) {
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
