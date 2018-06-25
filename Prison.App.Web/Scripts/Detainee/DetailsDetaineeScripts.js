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
                $('#dtmReleaseDate').datetimepicker({ pickTime: true, language: 'ru' });

                

            }


        });
    });

    $("#btnCreateDetention").click(function () {
        $('#dvCreateDetentionModal').empty();
        var URL = $("#CreateDetentionUrl").val();
        var ID = $("input[type='hidden'][name='DetaineeID']").val();


        $.ajax({

            url: URL,
            data: { id: ID },

            success: function (data) {

                $('#dvCreateDetentionModal').html(data);
                $('.datetimepicker').datetimepicker({ pickTime: false, language: 'ru', format: 'DD.MM.Y' });
                $('#dtmReleaseDate').datetimepicker({ pickTime: true, language: 'ru' });
                $('#CreateDetentionModal').fadeIn();

                $(".modal button[data-dismiss='modal']").click(function () {

                    $('#CreateDetentionModal').remove();

                });
            }


        });

    });


});
