import axios from "axios";

const apiUrl = `https://localhost:5001/api/`;

export async function getForecast(city) {
    const response = await axios.get(`${apiUrl}WeatherForecast/${city}`);
    return response.data;
}

export async function getCitiesByName(search) {
    const response = await axios.get(`${apiUrl}GeoData/cities-by-name/${search}`);
    return response.data;
}

export async function getCitiesAll() {
    const response = await axios.get(`${apiUrl}GeoData/cities`);
    return response.data;
}
