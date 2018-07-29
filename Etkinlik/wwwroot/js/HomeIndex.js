function getLastSurvey() {
    $.ajax({
        type: 'GET',
        url: '/getLastSurvey'
    });
}
