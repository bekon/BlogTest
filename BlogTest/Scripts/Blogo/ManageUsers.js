$(document).ready(function () {
    $('#users').load('GetUsers', { page: 1 }, function () {
        $('#search').click(function () {
            $('#users').load('GetUsers', { user: $('#UserName').text() } //, function (response, status, xhr) { alert(xhr.statusText); }
            );
            $('.page').click(function () {
                $('#users').load('GetUsers', { page: $(this).text() } //, function (response, status, xhr) { alert(xhr.statusText); }
                );
            });
        });
        $('.page').click(function () {
            $('#users').load('GetUsers', { page: $(this).text() } //, function (response, status, xhr) { alert(xhr.statusText); }
            );
        });
    }
        );
});
function changeRole(user) {
    $.ajax({
        url: "ChangeRole",
        type: "POST",
        data: {
            userName: user, newRole: $('#Role'+user).val() 
        }
    }).done(function () {
       alert("Role changed");
    }).fail(function () {
        alert("Update error. Try later");
    })
};
