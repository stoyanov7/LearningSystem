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

                $(function () {
                    $('#group-name').val('');
                });
            });

            $("#ask-question-form").submit(e => {
                e.preventDefault();
                let question = $("#question").val();
                connection.invoke("AskQuestion", group.slug, question);

                $(function () {
                    $('#question').val('');
                });
            });

            connection.on("group-accept", (courseName, slug, page) => {
                group.name = courseName;
                group.slug = slug;

                $("#join-group-form").hide();
                $("#ask-question-form").show();

                $("#questions")
                    .append($("<div>").html(`<h2>${courseName}</h2>`))
                    .append($("<div>").attr("id", "questions-container"));

                for (let question of page.questions) {
                    $("<div>").html(`<strong>${question.username}</strong>: ${question.questionText}`)
                        .appendTo($("#questions-container"));
                }
            });

            connection.on("group-rejected", reason => {
                $("#questions").html(`<h2>${reason}</h2>`);
            });

            connection.on("receive-question", question =>
                $("<div>").html(`<strong>${question.username}</strong>: ${question.questionText}`)
                    .appendTo($("#questions-container")));
        })
        .catch(err => console.log(err));

    console.log(connection);
});