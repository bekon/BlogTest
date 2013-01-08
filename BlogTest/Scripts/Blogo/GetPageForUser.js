function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
};
$(document).ready(function () {
    $('#entries').load('/Home/GetPage', { page: 1 , user: getUrlVars()["user"] } //, function (response, status, xhr) { alert(xhr.statusText); }
        );
    $('.page').click(function () {
        $('#entries').load('GetPage', { page: $(this).text(), user: getUrlVars()["user"] }//, function (response, status, xhr) { alert(xhr.statusText); }
            );
    });
});