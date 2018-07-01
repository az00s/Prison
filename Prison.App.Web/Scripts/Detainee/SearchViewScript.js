$(document).ready(function () {

    $('#datetimepicker').datetimepicker({ pickTime: false, language: 'ru', format: 'DD.MM.Y' });

    $("#btnSearchDetainee").click(function () {

        var URL = $("#urlField").val();

        $.ajax({
            method:'GET',
            url: URL,
            data: {
                FirstName: $("input[name='FirstName']").val(),
                LastName: $("input[name='LastName']").val(),
                Middlename: $("input[name='Middlename']").val(),
                ResidenceAddress: $("input[name='ResidenceAddress']").val(),
                DetentionDate: $("#dateField").val()
            },
            success: function (data) {

                $('#ResultTable').html(data);

            }


        });
    });

    document.onkeydown = function () {
        if (window.event.keyCode == '13') {
            $("#btnSearchDetainee").click();
        }
    }

    $(".clear").click(function () {

        $(this).closest("div").find("input[type='text'].form-control").val("")
    });
});