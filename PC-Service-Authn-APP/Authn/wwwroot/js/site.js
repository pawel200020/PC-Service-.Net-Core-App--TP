// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $("#selectRepair").click(function () {
        event.preventDefault();
        console.log("clikced");
        $.ajax({
            type: "POST",
            url: "ProcessSearch",
            data: $("form").serialize(),
            success: function (data) {
                $('#repairInformationArea').html(data);
            }
        });
    });
});

