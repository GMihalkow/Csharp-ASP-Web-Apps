$(function () {
    var dropdownBtns = $('.dropdown-btn');

    dropdownBtns.on('click', function (e) {
        var $currentBtn = $(e.currentTarget);

        var dropdown = $currentBtn.next();
        
        var attrValue = dropdown.attr('style') || ''; 

        if (attrValue.includes('block')) {
            dropdown.fadeOut(200);
        } else {
            dropdown.fadeIn(500);
        }
    });
});