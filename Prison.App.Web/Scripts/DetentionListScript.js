$(function () {
    $(document).on('click', '.table-hover tbody tr', function () {
        $('#dvDetentionDetailsModal').empty();

        var td = $(this).children()[0];
        var ID = td.innerText;
        $.ajax(
            {
                url: "/Detainee/DetentionDetails",
                method: "GET",
                data: { id: ID },
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
