var ready = $(document).ready(function () {

    $(".HeaderExpander").bind("click", function () {
        if ($(this).hasClass("active")) {
            //hide the div
            DoHide($(this));
        }
        else {
            DoShow($(this));
        }
        return false;
    });
    //remove last bottom width for the LI Fag
    $("ul.surport_block li:last").attr("style","border-bottom:none !important;");  
});

function DoShow(currentAtag) {

    $(".HeaderExpander").each(function () {
        DoHide($(this));
    });
    
    currentAtag.addClass("active");
    var parent = currentAtag.parent(); //get the parent
    parent = parent.parent(); 
    var div = parent.find("div");
    div.show();
    
}

function DoHide(currentAtag) {
   
    currentAtag.removeClass("active");   
    var parent = currentAtag.parent(); //get the parent
    parent = parent.parent();
    var div = parent.find("div");
    div.hide();
    
}