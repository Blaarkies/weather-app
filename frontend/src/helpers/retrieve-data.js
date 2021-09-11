import axios from "axios";

/**
 * API url based on build environment, read from .env files.
 */
const apiUrl = process.env.VUE_APP_BASE_URL;

/**
 * Get the weather forecast for a specific city or zip code
 * @param city string
 * @param zipCode string
 * @returns {Promise<object>} weather forecast object
 */
export async function getForecast(city, zipCode = null) {
    const response = await axios.get(`${apiUrl}Weather/forecast`,
        {
            params: {city, zipCode}
        });
    return response.data;
}

/**
 * Get city names similar to the [search] string.
 * @param search
 * @returns {Promise<array<string>>} list of city names
 */
export async function getCitiesByName(search) {
    const response = await axios.get(`${apiUrl}GeoData/cities-by-name/${search}`);
    return response.data;
}

/**
 * Get all city names.
 * @returns {Promise<array<string>>} list of city names
 */
export async function getCitiesAll() {
    const response = await axios.get(`${apiUrl}GeoData/cities`);
    return response.data;
}
