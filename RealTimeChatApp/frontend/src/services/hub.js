import * as signalR from "@microsoft/signalr";
export function createHubConnection() {
    const url = import.meta.env.VITE_SIGNALR_URL;
    const token = localStorage.getItem("token");
    return new signalR.HubConnectionBuilder()
        .withUrl(url, { accessTokenFactory: () => token })
        .withAutomaticReconnect()
        .build();
}