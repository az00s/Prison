$(document).ready(function () {

    $('#datetimepicker').datetimepicker({ pickTime: false, language: 'ru', format: 'DD.MM.Y' });

    $("#GetDetaineeBtn").click(function () {

        var WrongDate = $("#dateField").val();
        var day = WrongDate.substring(3, 5);
        var month = WrongDate.substring(0, 2);
        var Year = WrongDate.substring(6, 10);
        var RightDate = day + "." + month + "." + Year
        var URL = $("#urlField").val();

        $.ajax({

            url: URL,
            data: {
                date: RightDate
            },
            success: function (data) {

                $('#ResultTable').html(data);

            }


        });
    });

});