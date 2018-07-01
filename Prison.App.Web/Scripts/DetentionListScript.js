$(function () {
    $(document).on('click', '.table-hover tbody tr', function () {
        $('#dvDetentionDetailsModal').empty();

        var td = $(this).children()[0];
        var detentionId = td.innerText;
        var detaineeId = $("input[type='hidden'][name='DetaineeID']").val();
        $.ajax(
            {
                url: "/Detainee/DetentionDetails",
                method: "GET",
                data: {
                    detentionID: detentionId,
                    detaineeID:detaineeId
                },
                success: function (data) {

                    $('#dvDetentionDetailsModal').html(data);
                    $('#DetentionModal').fadeIn();

                    $(".modal button[data-dismiss='modal']").click(function () {

                        $('#DetentionModal').remove();

                    });

                }
            }
        )
    });

});
