function getData(id) {
    $.ajax({
        type: 'GET',
        url: '/Survey/surveyVote/'+id,
        data: { id: id },
        dataType: "json",
        success: dataRender
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