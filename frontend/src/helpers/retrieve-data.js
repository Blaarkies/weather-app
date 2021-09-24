import axios from "axios";
import {authHeader} from '@/helpers/auth-helper';

/**
 * API url based on build environment, read from .env files.
 */
const apiUrl = process.env.VUE_APP_BASE_URL;

/**
 * Get the weather forecast for a specific city
 * @param city string
 * @returns {Promise<object>} weather forecast object
 */
export async function getForecastByCity(city) {
    const response = await axios.get(`${apiUrl}Weather/forecast/city`,
        {params: {city}, headers: authHeader()});
    return response.data;
}

/**
 * Get the weather forecast for a specific zip code in Germany
 * @param zipCode string
 * @returns {Promise<object>} weather forecast object
 */
export async function getForecastByZipCode(zipCode) {
    const response = await axios.get(`${apiUrl}Weather/forecast/zip-code`,
        {params: {zipCode}, headers: authHeader()});
    return response.data;
}

/**
 * Get city names similar to the [search] string.
 * @param search
 * @returns {Promise<array<string>>} list of city names
 */
export async function getCitiesByName(search) {
    const response = await axios.get(`${apiUrl}GeoData/cities-by-name/${search}`,
        {headers: authHeader()});
    return response.data;
}

/**
 * Get all city names.
 * @returns {Promise<array<string>>} list of city names
 */
export async function getCitiesAll() {
    const response = await axios.get(`${apiUrl}GeoData/cities`,
        {headers: authHeader()});
    return response.data;
}
