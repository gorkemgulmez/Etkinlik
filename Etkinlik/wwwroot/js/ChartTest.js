var ctx = document.getElementById("myChart").getContext('2d');
var mychart = new Chart(ctx, {
    type: 'pie',
    data: {
        labels: ["Anket"],
        datasets: [{
            label: 'Oy Sayısı',
            data: [1],
            
            borderWidth: 1
        }]
    },options:{
        legend: {
            display: false
        }
    }
});
