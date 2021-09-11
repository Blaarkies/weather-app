/**
 * Gets and formats error messages from the API into user readable messages.
 * @param error object
 * @returns {string}
 */
export function getMessageFromError(error) {
    let message = error.response?.data?.toString()?.trim() ?? '';
    return message[0].toUpperCase() + message.slice(1);
}
