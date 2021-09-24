import axios from 'axios';

const apiUrl = process.env.VUE_APP_BASE_URL;
const userAuthStorageKey = 'user-auth';

export const authService = {
    login,
    logout,
    getLoggedInUser,
};

async function login(username, password) {
    return await axios.post(
        `${apiUrl}Authn/authenticate`,
        JSON.stringify({username, password}),
        {headers: {'Content-Type': 'application/json'}})
        .then(handleResponse)
        .then(user => {
            if (user) {
                localStorage.setItem(userAuthStorageKey, JSON.stringify(user));
            }

            return user;
        });
}

function logout() {
    localStorage.removeItem(userAuthStorageKey);
}

function getLoggedInUser() {
    return JSON.parse(localStorage.getItem(userAuthStorageKey));
}

function handleResponse(response) {
    return response.data;
}
