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
    var color = [];

    for(var i in jinfo) {
        label.push(jinfo[i].choiceName);
        data.push(jinfo[i].vote);
        color.push(randomColorString());
    }
    console.log(color)
    renderChart(label, data, color);
}

function randomColorString() {
    var color = 'rgba(' + randomNumber(0, 255) + ', ' + randomNumber(0, 255) + ', ' + randomNumber(0, 255) + ', 1)';
    return color;
}

function randomNumber(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min)
}