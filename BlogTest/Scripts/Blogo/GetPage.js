$(document).ready(function () {
    $('#entries').load('/Home/GetPage', { page: 1 } //, function (response, status, xhr) { alert(xhr.statusText); }
        );
    $('.page').click(function () {
        $('#entries').load('/Home/GetPage', { page: $(this).text() } //, function (response, status, xhr) { alert(xhr.statusText); }
        );
    });
});