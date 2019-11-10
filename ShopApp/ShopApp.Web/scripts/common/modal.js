$(function () {
    var modals = $(".modal-wrapper");

    modals.on("click", function (e) {
        if ($(e.target).attr("class") === "modal-wrapper") {
            modals.fadeOut(200);
            modals.find(".custom-modal").fadeOut(500);
        }
    });

});

var _this = this;

var modalFunctions = {
    openModal: function openModal(heading, body, footerBtns) {
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
    },

    openConfirmationModal: function openConfirmationModal(confirmCallback) {
        var body = "<p>Are you sure that u want to continue?</p>";

        var buttons = [
            {
                Name: "Confirm",
                onClick: confirmCallback
            }
        ];

        _this.modalFunctions.openModal("Confirm", body, buttons);
    },

    openProductDetailsDialog: function openProductDetailsDialog(productId, buttons) {
        $.ajax({
            method: "GET",
            url: "/api/ProductApi/Get/" + productId
        }).then(function (product) {
            var template = $("#product-details-template").html();
            var rendered = Mustache.render(template, product);

            _this.modalFunctions.openModal("Product Details", rendered, buttons);
        });
    }
};