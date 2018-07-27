

function getData(id) {
    $.ajax({
        type: 'GET',
        url: '/Survey/surveyVote/'+id,
        data: { id: id },
        dataType: "json",
        success: dataRender
    });
}

function dataRender(jinfo) {
    var label = [];
    var data = [];
    for(var i in jinfo) {
        label.push(jinfo[i].choiceName);
        data.push(jinfo[i].vote);
    }
    //label = [jinfo[0].choiceName, jinfo[1].choiceName];
    //data = [jinfo[0].vote, jinfo[1].vote];
    renderChart(label, data);
}