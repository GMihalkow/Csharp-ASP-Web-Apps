function openProductDetailsDialog(productId, buttons) {
    $.ajax({
        method: "GET",
        url: "/api/ProductApi/Get/" + productId
    }).then(function (product) {
        var template = $("#product-details-template").html();
        var rendered = Mustache.render(template, product);

        openModal("Product Details", rendered, buttons);
    });
}
