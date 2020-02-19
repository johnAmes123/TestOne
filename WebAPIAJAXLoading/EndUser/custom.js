var $instance = $("#movies");

$.ajax({
    type:"GET",
    url:"/api/Movies",
    success: function (movies) {
        console.log(movies);
    },
    error: function () {
        console.log('There was an error');
    }
});