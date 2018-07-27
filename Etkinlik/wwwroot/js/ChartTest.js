var ctx = document.getElementById("myChart").getContext('2d');

function bgColor(num) {
    var rbgColor = [];

    while (num > 0) {
        var stri = 'rgba(' + randomNumber(0, 255) + ', ' + randomNumber(0, 255) + ', ' + randomNumber(0, 255) + ', 1)';
        rbgColor.push(stri);
    }
    return rbgColor;
}

function randomNumber(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min)
}

function renderChart(alabel, adata) {
    var mychart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: alabel,
            datasets: [{
                label: 'Oy Sayısı',
                data: adata,
                backgroundColor: bgColor(alabel.length),
                borderWidth: 1
            }]
        }, options: {
            legend: {
                display: false
            }
        }
    });
}

