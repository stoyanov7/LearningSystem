/// <reference path="../lib/signalr/signalr.js" />
$(function() {
    let baseUrl = "https://localhost:44319/";
    let connectionBuilder = new signalR.HubConnectionBuilder().withUrl(baseUrl + "questions");
    let connection = connectionBuilder.build();

    connection.start();

    console.log(connection);
});