/// <reference path="../lib/signalr/signalr.js" />
$(function() {
    let baseUrl = "https://localhost:44319/";
    let connectionBuilder = new signalR.HubConnectionBuilder().withUrl(baseUrl + "questions");
    let connection = connectionBuilder.build();

    connection.start()
        .then(() => {
            $("#submit-button").click(e => {
                e.preventDefault();
                let username = $("#username").val();
                let question = $("#question").val();

                connection.invoke("PostQuestion", username || "unknown", question);
            });
            
            connection.on("showQuestion", (user, question) => {
                $("#questions").append($("<div>").html(`<strong>${user}</strong>: ${question}`));
            });
        }, err => console.log(err));

    console.log(connection);
});