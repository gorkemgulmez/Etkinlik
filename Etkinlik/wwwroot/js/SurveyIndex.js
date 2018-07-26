
function getData(id) {
    $.ajax({
        type: 'GET',
        url: '/testi',
        data: { id: id },
        dataType: "json"
        success: dataRender
    }).done(function () {
        alert("Oldu");
    });
}

function dataRender(data) {
    alert(data);
}