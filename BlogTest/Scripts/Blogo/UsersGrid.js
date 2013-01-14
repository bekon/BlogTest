$(document).ready(function () {
    $("#usersgrid").kendoGrid({
        dataBound: function (e) {
            $(".dropDownRole").kendoDropDownList();
        },
        dataSource: {
            serverPaging: true,
            serverSorting: true,
            pageSize: 25,
            schema: {
                type: "json",
                data: "Data",
                total: "Total",
                model: {
                    fields: {
                        UserName: { type: "string" },
                        Role: { type: "string" },
                    }
                }
            },
            transport: {
                read: {
                    url: "GetUsers",
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json"
                },
                parameterMap: function (options) {
                    return kendo.stringify(options);
                }
            }
        },
        height: 280,
        scrollable: { virtual: true },
        sortable: true,
        filterable: true,
        columns: [
            { field: "UserName", title: "User", width: 170, },
            { field: "Role", title: "Role", width: 150 },
            { field: "NewRole", title: "New Role", template: '#=showDropDown(UserName)#', width: 200, filterable: false },
            { field: "Change", title: "Change", template: '<input type=\'button\' value=\'Change\' onClick="changeRole(\'#=UserName#\')" />', width: 150, filterable: false },
        ]
    });

});
function changeRole(user) {
    $.ajax({
        url: "ChangeRole",
        type: "POST",
        data: {
            userName: user, newRole: $('#dropDown_' + user).val()
        }
    }).done(function () {
        alert("Role changed");
        refreshGrid();
    }).fail(function () {
        alert("Update error. Try later");
    });
   
}

function refreshGrid()
{
    var grid = $("#usersgrid").data("kendoGrid");
    grid.dataSource.read();
    grid.refresh();
}

function showDropDown(userName) {
    var str = "<select id=\"dropDown_" + userName + "\" class='dropDownRole'><option>reader</option><option>writer</option><option>chief</option></select>";
    return str;
}
