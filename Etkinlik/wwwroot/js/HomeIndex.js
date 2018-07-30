function getLastSurvey() {
    $.ajax({
        type: 'GET',
        url: '/getLastSurvey'
    });
}

function voteSurvey(id) {
    $.ajax({
        type: "POST",
        url: "/Survey/vote/" + id,
        data: { id: id },
        success: function () {
            alert("oldu");
        }
    });
}