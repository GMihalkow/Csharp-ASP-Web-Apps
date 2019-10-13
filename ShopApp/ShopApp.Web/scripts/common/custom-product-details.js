function openProductDetailsDialog(productId, buttons) {
    if (!buttons) {
        buttons = [];
    }

    var modals = $(".modal-wrapper");
    $.ajax({
        method: "GET",
        url: "/Product/GetProduct/" + productId
    }).then(function (product) {
        product.ModalHeading = "Product Details";

        var template = $("#product-details-template").html();
        var rendered = Mustache.render(template, product);

        $(".custom-modal").html(rendered);

        modals.fadeIn(200);
        modals.find(".custom-modal").fadeIn(500);
        var modalFooter = $(".custom-modal").find(".modal-footer");
        Array.from(buttons).forEach(function (btn) {
            var renderedButton = $("<button class='modal-btn'>" + btn.Name + "</button>");
            $(renderedButton).on("click", btn.onClick);
            modalFooter.append(renderedButton);
        });

        var closeModalBtns = $(".custom-modal").find("[close-modal-btn]");
        closeModalBtns.on("click", function (e) {
            var currentTarget = $(e.currentTarget);
            var currentModal = currentTarget.closest(".modal-wrapper");

            currentModal.fadeOut(200);
            currentModal.find(".custom-modal").fadeOut(500);
        });
    });
}
