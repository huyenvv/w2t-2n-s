var clicked  = false;
$(document).ready(function () {
    $("input[type='submit']").ajaxStart(function () {
        var f = $(this).parents('form:first');
        if (f.is(":visible")) {
            $(this).removeClass('button');
            $(this).addClass('button_dis');
            //$('<p class="loadingString" style="text-align:center; display:block; padding:5px; margin:5px; font-size:12px;">Đang xử lý vui lòng chờ...</p>').insertAfter($(this));
            clicked = true;
        }
    });


    $("input[type='submit']").ajaxComplete(function () {
        var f = $(this).parents('form:first');
        if (f.is(":visible")) {
            $(this).removeClass('button_dis');
            $(this).addClass('button');
            // $(".loadingString").remove();
            clicked = false;
        }
    });

    $("input[type='submit']").ajaxError(function () {
        var f = $(this).parents('form:first');
        if (f.is(":visible")) {
            $(this).removeClass('button_dis');
            $(this).addClass('button');
            // $(".loadingString").remove();
            clicked = false;
        }
    });

});

$(function () {
    jQuery.fn.extend({

        outerHtml: function (replacement) {
            // We just want to replace the entire node and contents with
            // some new html value
            if (replacement) {
                return this.each(function () { $(this).replaceWith(replacement); });
            }

            /*
            * Now, clone the node, we want a duplicate so we don't remove
            * the contents from the DOM. Then append the cloned node to
            * an anonymous div.
            * Once you have the anonymous div, you can get the innerHtml,
            * which includes the original tag.
            */
            var tmpNode = $("<div></div>").append($(this).clone());
            var markup = tmpNode.html();

            // Don't forget to clean up or we will leak memory.
            tmpNode.remove();
            return markup;
        }
    });

    $.fn.vCenter = function (options) {
        var pos = {
            sTop: function () {
                return window.pageYOffset
        || document.documentElement && document.documentElement.scrollTop
        || document.body.scrollTop;
            },
            wHeight: function () {
                return window.innerHeight
        || document.documentElement && document.documentElement.clientHeight
        || document.body.clientHeight;
            }
        };
        return this.each(function (index) {
            if (index == 0) {
                var $this = $(this);
                var elHeight = $this.height();
                var elTop = pos.sTop() + (pos.wHeight() / 2) - (elHeight / 2);
                $this.css({
                    position: 'absolute',
                    marginTop: '0',
                    top: elTop
                });
            }
        });
    };

    $.BigLoading = function (width, divpopup) {
        var overlayElement = $('#overlay');
        if (overlayElement.length == 0) {
            overlayElement = $("<div id='overlay' class='ui-widget-overlay' ></div>");
            overlayElement.css({
                "position": "absolute",
                "top": 0,
                "left": 0,
                "width": $(document).width(),
                "height": $(document).height(),
                "z-index": 11111111
            });
            overlayElement.appendTo("body");
        }

        var popupElement = $("#" + divpopup);
        //alert(loadingElement.html());
        var windowWidth = $(window).width();
        var windowHeight = $(window).height();
        var popupWidth = width;
        popupElement.css({
            "position": "absolute",
            "left": (windowWidth - popupWidth) / 2,
            "z-index": 1000000000
        });

        popupElement.vCenter();

        popupElement.appendTo("body");

        overlayElement.show();

        popupElement.show();

    };

    $.RemoveBigLoading = function () {
        $("#popupdiv").remove();
        $("#overlay").remove();
    };

    $.HtmlEncode = function (html) {
        if (html == null)
            return "";
        return $('<div/>').text(html).html();
    };

    $.HtmlDecode = function (text) {
        if (html == null)
            return "";
        return $('<div/>').html(text).text();
    };

    $.ShowErrorPopup = function (errors) {

        var overlayElement = $("#overlay");

        if (overlayElement.length == 0) {
            overlayElement = $("<div id='overlay' class='ui-widget-overlay'></div>");
            overlayElement.appendTo("body");
        }
        var popupElement = $("#PopupWrapper");
        //alert(errors);
        popupElement.find("ul").html(errors);

        //alert(loadingElement.html());
        var windowWidth = $(window).width();
        var windowHeight = $(window).height();
        var popupWidth = popupElement.width();
        popupElement.css({
            "position": "absolute",
            "left": windowWidth / 2 - popupWidth / 2,
            "z-index": 1000
        });
        overlayElement.css({
            "position": "absolute",
            "top": 0,
            "width": $(document).width(),
            "height": $(document).height(),
            "z-index": 999//10
        });
        popupElement.vCenter();
        overlayElement.fadeIn("slow");
        popupElement.fadeIn("slow");

        $("#popupCloseBtn").bind("click", function () {
            $.ClosePopup();
            return false;
        });

    };

    $.ShowPopupInstruction = function (errors) {

        var overlayElement = $("#overlay");

        if (overlayElement.length == 0) {
            overlayElement = $("<div id='overlay' class='ui-widget-overlay' style='opacity: 0.4;'></div>");
            overlayElement.appendTo("body");
        }
        var popupElement = $("#Instruction");
        //alert(errors);
        popupElement.find("#content_instruction").html(errors);

        //alert(loadingElement.html());
        var windowWidth = $(window).width();
        var windowHeight = $(window).height();
        var popupWidth = popupElement.width();
        popupElement.css({
            "position": "absolute",
            "left": windowWidth / 2 - popupWidth / 2,
            "z-index": 1000
        });
        overlayElement.css({
            "position": "absolute",
            "top": 0,
            "width": $(document).width(),
            "height": $(document).height(),
            "z-index": 999//10
        });
        popupElement.vCenter();
        overlayElement.fadeIn("slow");
        popupElement.fadeIn("slow");

        $("#popupCloseBtn_Instruction").bind("click", function () {
            $("#Instruction").fadeOut("slow");
            $("#overlay").fadeOut("slow");
            $("#overlay").unbind('click');
            return false;
        });
        $("#overlay").bind("click", function () {
            /*$("#Instruction").fadeOut("slow");
            $("#overlay").fadeOut("slow");
            $("#overlay").unbind('click');*/
        });
    };

    $.ClosePopup = function () {
        $("#PopupWrapper").fadeOut("slow");
        //$("#overlay").fadeOut("slow");
        $(".ui-widget-overlay").fadeOut("slow");
    };

    $.CheckEmail = function (id, error) {
        //$('#' + id + '').blur(function () {
        //var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        var emailaddressVal = $('#' + id + '').val();
        if (emailaddressVal == '' || !validateEmail(emailaddressVal)) {
            $('#' + id + '').addClass("input-validation-error");
            if ($('.' + error + '').is(":visible") == false)
                $('.' + error + '').slideToggle("slow");
            //timeOut = setTimeout(function () { $('#' + id + '').focus(); }, 100);
            $.Status = false;
        }
        else {
            $('.' + error + '').hide();
            $('#' + id + '').removeClass("input-validation-error");
            //clearTimeout(timeOut);
            $.Status = true;
        }
        //});
    };

    $.Status = false;

    /*when has account*/
    $.LocationHome = function (url) {
        $.cookie('Landing', "1", { expires: 30000, path: '/' });
        if (url == "back") {
            location.href = "";
        }
        if (url != "") {
            location.href = url;
        }
        else {
            $("#popupdiv").hide();
            $('.popupdiv').hide();
            $("#overlay").hide();
            //location.reload();
        }
        $('#popupdiv.popup').remove();
    };

});

function validateEmail(email) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    var address = email;
    if (reg.test(address) == false) {
        return false;
    }
    else
        return true;
};
jQuery.fn.ForceNumericOnly =
    function () {
        return this.each(function () {
            $(this).keydown(function (e) {
                var key = e.charCode || e.keyCode || 0;
                // allow backspace, tab, delete, arrows, numbers and keypad numbers ONLY
                return (
                    key == 8 ||
                    key == 9 ||
                    key == 46 ||
                    (key >= 37 && key <= 40) ||
                    (key >= 48 && key <= 57) ||
                    (key >= 96 && key <= 105));
            });
        });

    };
/*function isEmailValid(email) {
var reg = /^([A-Za-z0-9]{1,}([-_\.&][A-Za-z0-9]{1,}){0,}){1,}@(([A-Za-z0-9]{1,}[-]{0,1})\.){1,}[A-Za-z]{2,6}$/;
var address = email;
if (reg.test(address) == false) {
return false;
}
else
return true;
}
    function simple_tooltip(targetItems, name) {
        $(targetItems).each(function (i) {
            
            
                    $("body").append("<div class='" + name + "' id='" + name + i + "'><p>" + $(this).attr('tooltip') + "</p></div>");
                var my_tooltip = $("#" + name + i);
            $(this).removeAttr("title").mouseover(function () {
                my_tooltip.css({ opacity: 0.8, display: "none" }).show();
            }).mousemove(function (kmouse) {
                my_tooltip.css({ left: kmouse.pageX + 15, top: kmouse.pageY + 15 });
            }).mouseout(function () {
                my_tooltip.hide();// fadeOut(400);
            });
        });
    }*/