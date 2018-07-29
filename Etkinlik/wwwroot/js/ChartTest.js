var ctx = document.getElementById("myChart").getContext('2d');



function randomColorString() {
    var color = 'rgba(' + randomNumber(0, 255) + ', ' + randomNumber(0, 255) + ', ' + randomNumber(0, 255) + ', 1)';
    return color;
}

function randomNumber(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min)
}

function renderChart(alabel, adata, abgColor) {
    
    var mychart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: alabel,
            datasets: [{
                label: 'Oy Sayısı',
                data: adata,
                backgroundColor: abgColor,
                borderColor: abgColor,
                borderWidth: 0.6
            }]
        }, options: {
            legend: {
                display: false
            }
        }
    });
}