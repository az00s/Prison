var num = $("select[name^='Roles[']").length-1;

$("#btnAddRoles").click(function () {
    if (num < 2) {
        num = num + 1;

        var DropDownList = $("[name='Roles[0].RoleID']");

        var NewDropDownList = DropDownList.clone().attr("name", "Roles[" + num + "].RoleID");

        NewDropDownList.appendTo("#dvRolesDrdwn");
    }

});

$("#btnRemoveRoles").click(function () {
    if (num > 0) {
        $("[name='Roles[" + num + "].RoleID']").remove();

        num = num - 1;
    }
});