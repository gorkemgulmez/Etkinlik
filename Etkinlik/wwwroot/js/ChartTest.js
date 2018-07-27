var ctx = document.getElementById("myChart").getContext('2d');

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