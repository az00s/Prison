﻿$(function () {
    var num = $("input[name^='PhoneNumbers[']").length - 1;

    $("#btnAddNumber").click(function () {
        if (num < 2) {
            num = num + 1;

            var NumberInput = $("[name='PhoneNumbers[0]']");

            var NewNumberInput = NumberInput.clone().attr("name", "PhoneNumbers[" + num + "]").val(() => '');

            NewNumberInput.appendTo("#dvPhoneNumbers");
        }

    });

    $("#btnRemoveNumber").click(function () {
        if (num > 0) {
            $("[name='PhoneNumbers[" + num + "]']").remove();

            num = num - 1;
        }
    });
});