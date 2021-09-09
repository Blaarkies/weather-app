export function formatDateToDayMonth(date) {
    return date.toLocaleDateString('en-UK', {day: 'numeric', month: 'long'});
}

export function formatDateToHourMinute(date) {
    return date.toLocaleTimeString('en-UK', {hour: '2-digit', minute: '2-digit'});
}

export function formatDateToWeekDay(date) {
    return date.toLocaleDateString('en-UK', {weekday: 'long'});
}
