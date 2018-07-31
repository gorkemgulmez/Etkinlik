function getData(id) {
    $.ajax({
        type: 'GET',
        url: '/getVote/' + id,
        data: { id: id },
        dataType: "json",
        success: dataRender
    });
}

