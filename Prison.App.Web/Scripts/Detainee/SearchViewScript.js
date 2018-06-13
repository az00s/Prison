$(document).ready(function () {

    $('#datetimepicker').datetimepicker({ pickTime: false, language: 'ru', format: 'DD.MM.Y' });

    $("#btnSearchDetainee").click(function () {

        //var WrongDate = $("#dateField").val();
        //var day = WrongDate.substring(3, 5);
        //var month = WrongDate.substring(0, 2);
        //var Year = WrongDate.substring(6, 10);
        //var RightDate = day + "." + month + "." + Year
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

});