function getLastSurvey() {
    $.ajax({
        type: 'GET',
        url: '/getLastSurvey',
        dataType: 'json',
        success: dataRender
    });
}

function dataRender(jinfo) {
    var label = [];
    var data = [];
    var color = [];

    for (var i in jinfo) {
        label.push(jinfo[i].choiceName);
        data.push(jinfo[i].vote);
        color.push(randomColorString());
    }
    console.log(color)
    renderChart(label, data, color);
}