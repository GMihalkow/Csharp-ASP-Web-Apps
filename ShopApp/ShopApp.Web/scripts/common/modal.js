$(function () {
    var modals = $(".modal-wrapper");

    //var openModalBtns = $("[modal-btn]");
    //openModalBtns.on("click", function () {
    //    modals.fadeIn(200);
    //    modals.find(".custom-modal").fadeIn(500);
    //});

    modals.on("click", function (e) {
        if ($(e.target).attr("class") === "modal-wrapper") {
            modals.fadeOut(200);
            modals.find(".custom-modal").fadeOut(500);
        }
    });
});

function openModal(heading, body, footerBtns) {
    if (!footerBtns) {
        footerBtns = [];
    }

    var closeModalBtnExists = footerBtns.some(function (btn) {
        return btn.Name === "Cancel";
    });

    if (!closeModalBtnExists) {
        var closeModalBtn = {
            Name: "Cancel",
            onClick: function () {
                $(".custom-modal").find("[close-modal-btn]").click();
            }
        };

        footerBtns.push(closeModalBtn);
    }

    var modals = $(".modal-wrapper");

    modals.fadeIn(200);
    modals.find(".custom-modal").fadeIn(500);

    modals.find(".modal-header").find("h3").text(heading);

    var modalBody = modals.find(".modal-body");
    modalBody.html("");
    modalBody.html(body);

    var $form = modalBody.find(".modal-form");
    if ($form.length > 0) {
        $.validator.unobtrusive.parse($form);
    }

    var modalFooter = $(".custom-modal").find(".modal-footer");
    modalFooter.html("");
    Array.from(footerBtns).forEach(function (btn) {
        var renderedButton = $("<button class='modal-btn'>" + btn.Name + "</button>");
        $(renderedButton).on("click", btn.onClick);
        modalFooter.append(renderedButton);
    });

    // TODO [GM]: Don't attach event every time?
    var closeModalBtns = $(".custom-modal").find("[close-modal-btn]");
    closeModalBtns.on("click", function (e) {
        var currentTarget = $(e.currentTarget);
        var currentModal = currentTarget.closest(".modal-wrapper");

        currentModal.fadeOut(200);
        currentModal.find(".custom-modal").fadeOut(500);
    });
}