/**
 * Format date to a format like "23 September".
 * @param date
 * @returns {string}
 */
export function formatDateToDayMonth(date) {
    return date.toLocaleDateString('en-UK', {day: 'numeric', month: 'long'});
}

/**
 * Format date to a format like "13:37".
 * @param date
 * @returns {string}
 */
export function formatDateToHourMinute(date) {
    return date.toLocaleTimeString('en-UK', {hour: '2-digit', minute: '2-digit'});
}

/**
 * Format date to a format like "Monday".
 * @param date
 * @returns {string}
 */
export function formatDateToWeekDay(date) {
    return date.toLocaleDateString('en-UK', {weekday: 'long'});
}
