$(document).ready(function () {
    $("#grid").kendoGrid({
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
                        Email: { type: "string" },
                        LastActivityDate: { type: "date" }
                    }
                }
            },
            transport: {
                read: "GetBloggers"
            }
        },
        height: 280,
        scrollable: { virtual: true },
        filterable: true,
        columns: [
            { field: "UserName", title: "Blogger name",  template: '<a href="UserPosts?User=#=UserName#\">#=UserName#</a>' },
            { field: "Email", title: "Email" },
            { field: "LastActivityDate", title: "Last activity date", format: "{0:dd/MMMM/yyyy}", filterable: false },
        ]
    });
    $(".k-grid-header-wrap th").css("text-align", "center");
});