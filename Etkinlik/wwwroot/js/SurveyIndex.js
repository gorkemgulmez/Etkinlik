function getData(id) {
    $.ajax({
        type: 'GET',
        url: '/Survey/surveyVote/'+id,
        data: { id: id },
        dataType: "json",
        success: dataRender
    });
}