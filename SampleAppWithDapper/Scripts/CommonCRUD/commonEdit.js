﻿// Item edit modal
$(document).ready(function () {
    $(".editDialog").click(function (event) {
        event.preventDefault();
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id-value');
        var options = { /*'backdrop': 'static',*/ keyboard: true, focus: true };

        $.ajax({
            type: "GET",
            url: editUrl + '?id=' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (response) {

                $('#mdModal').modal(options);
                $('#mdModal').find('.modal-body').html("");
                $('#mdModal').modal('show');
                $('#mdModal').find('.modal-body').html(response);
            },
            failure: function (response) {

                $('#mdModal').modal(options);
                $('#mdModal').find('.modal-body').html("");
                $('#mdModal').modal('show');
                $('#mdModal').find('.modal-body').html("Problem loading your data...");
            },
            error: function (response) {

                $('#mdModal').modal(options);
                $('#mdModal').find('.modal-body').html("");
                $('#mdModal').modal('show');
                $('#mdModal').find('.modal-body').html("Problem loading your data...");
            }
        });
    });
});