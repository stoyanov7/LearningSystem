/// <reference path="../lib/signalr/signalr.js" />
$(function() {
    let baseUrl = "https://localhost:44319/";
    let group = {
        name: null,
        slug: null
    };

    let connection = new signalR.HubConnectionBuilder()
        .withUrl(baseUrl + "questions")
        .build();

    connection
        .start()
        .then(() => {
            $("#join-group-form").submit(e => {
                e.preventDefault();
                let groupName = $("#group-name").val();
                connection.invoke("JoinGroup", groupName);
            });

            $("#ask-question-form").submit(e => {
                e.preventDefault();
                let question = $("#question").val();
                connection.invoke("AskQuestion", group.slug, question);
            });
            
            connection.on("group-accept", (courseName, slug) => {
                group.name = courseName;
                group.slug = slug;

                $("#join-group-form").hide();
                $("#ask-question-form").show();

                $("#questions")
                    .append($("<div>").html(`<h2>${courseName}</h2>`))
                    .append($("<div>").attr("id", "questions-container"));
            });

            connection.on("group-rejected", reason => console.log(reason));

            connection.on("receive-question", question =>
                $("#questions-container").append($("<div>").text(question)));
        })
        .catch(err => console.log(err));

    console.log(connection);
});