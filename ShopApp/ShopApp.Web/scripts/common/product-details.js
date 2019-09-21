product = {};
product.dialog = {};

product.dialog.configuration = {
    autoOpen: false,
    height: 620,
    width: 500,
    modal: true,
    show: {
        effect: "blind",
        duration: 500
    },
    hide: {
        effect: "explode",
        duration: 500
    },
    // default buttons 
    buttons: {
        Cancel: function () {
            productDetailsDialog.dialog("close");
        }
    }
};
// product details popup dialog
var productDetailsDialog = $("#product-details-dialog");
productDetailsDialog.dialog(product.dialog.configuration);

var productDetailsButtons = $("[shop-action='product-details']");
productDetailsButtons.on("click", function (e) {
    e.preventDefault();

    var productId = $(this).closest("[shop-product-id]").attr("shop-product-id");

    $.ajax({
        method: "GET",
        url: "/Product/GetProduct/" + productId
    }).then(function (product) {
        var template = $("#product-details-template").html();
        var rendered = Mustache.render(template, product);

        $("#product-details-dialog").html(rendered);

        productDetailsDialog.dialog("open");
    });
});