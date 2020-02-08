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
    
    window.onclick = function(e) {
      var $target = $(e.target);
      
      var targetClasses = $target.attr('class') || '';
         
      if(!targetClasses.includes('dropdown')) {
          $('.dropdown').fadeOut(200);
      }
    };
});