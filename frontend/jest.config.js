module.exports = {
    preset: '@vue/cli-plugin-unit-jest/presets/typescript-and-babel',
    setupFilesAfterEnv: ['<rootDir>tests/setup.js'],
    transformIgnorePatterns: ['node_modules/(?!vue-router|@babel|vuetify)']
}
