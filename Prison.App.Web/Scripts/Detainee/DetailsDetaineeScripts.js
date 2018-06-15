$(function () {
    $(".btn#btnReleaseDetainee").click(function () {
        $('#dvReleaseModal').empty();
        var URL = $("#urlField").val();
        var ID = $("input[type='hidden'][name='DetaineeID']").val();

        $.ajax({

            url: URL,
            data: { id:ID},
            success: function (data) {

                $('#dvReleaseModal').html(data);
                $('#ReleaseModal').fadeIn();
                $('.datetimepicker').datetimepicker({ pickTime: false, language: 'ru', format: 'DD.MM.Y' });

            }


        });
    });

});
