$(function () {
    /// Appending the closing function to all the modals on the page //
    var closeModalBtns = $(".custom-modal").find("[close-modal-btn]");
    closeModalBtns.on("click", function (e) {
        var currentTarget = $(e.currentTarget);
        var currentModal = currentTarget.closest(".modal-wrapper");

        currentModal.fadeOut(200);
        currentModal.find(".custom-modal").fadeOut(500);
    });

    // Shows a spinner/loader for ever ajax call
    $(document).ajaxStart(function () {
        $("#loader-container").show();
    });

    $(document).ajaxComplete(function () {
        $("#loader-container").hide();
    });
});