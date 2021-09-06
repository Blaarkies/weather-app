export function getMessageFromError(error) {
    let message = error.response?.data?.toString()?.trim() ?? '';
    return message[0].toUpperCase() + message.slice(1);
}
