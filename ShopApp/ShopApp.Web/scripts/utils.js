$(function () {
    /// Appends the closing function to all the modals on the page //
    var closeModalBtns = $(".custom-modal").find("[close-modal-btn]");
    closeModalBtns.on("click", function (e) {
        var currentTarget = $(e.currentTarget);
        var currentModal = currentTarget.closest(".modal-wrapper");

        currentModal.fadeOut(200);
        currentModal.find(".custom-modal").fadeOut(500);
    });
});