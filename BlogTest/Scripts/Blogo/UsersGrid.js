﻿$(document).ready(function () {
    $("#usersgrid").kendoGrid({
        dataBound: function (e) {
            $(".dropDownRole").kendoDropDownList();
            
        },
        dataSource: {
            serverPaging: true,
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
                read: "GetUsers"
            }
        },
        height: 280,
        scrollable: { virtual: true },
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
