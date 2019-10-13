$(function () {
    var modals = $(".modal-wrapper");

    var openModalBtns = $("[modal-btn]");
    openModalBtns.on("click", function () {
        modals.fadeIn(200);
        modals.find(".custom-modal").fadeIn(500);
    });

    modals.on("click", function (e) {
        if ($(e.target).attr("class") === "modal-wrapper") {
            modals.fadeOut(200);
            modals.find(".custom-modal").fadeOut(500);
        }
    });
});