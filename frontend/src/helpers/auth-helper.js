import {authService} from '@/services';

export function authHeader() {
    let user = authService.getLoggedInUser();

    return user && user.token
        ? {'Authorization': 'Bearer ' + user.token}
        : {};
}
