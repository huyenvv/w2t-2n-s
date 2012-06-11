var heightLoad = -150;
var homepagePageSize;
var urlMoreDeal;
var preventMultiloadingFlag = false;
$(document).ready(function () {
    if (document.onscroll) {
        document.onscroll = function () {
            if ($(document).scrollTop() > heightLoad && preventMultiloadingFlag == false) {
                loading_funtion();
            }
        };
    }
    else {
        window.onscroll = function () {
            if ($(window).scrollTop() > heightLoad && preventMultiloadingFlag == false) {
                loading_funtion();
            }
        }
    }
});
function loading_funtion() {
            preventMultiloadingFlag = true;
            $('.see_more').fadeIn('slow');
            var list = $("ul.product li").length + homepagePageSize;
            
            $.post(urlMoreDeal,
                { entryCount: list},
			    function (data) {
			        //var child = $("ul.product", data).html();
			        $("ul.product li:last ").after(data);
			        $('.see_more').fadeOut('slow');
			        preventMultiloadingFlag = false;
			    }
            );
        };      