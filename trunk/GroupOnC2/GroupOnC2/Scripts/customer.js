function Set_Cookie(value) {
    $.post("/Default/ChangeCity", { cityId: value }, function (data) {

        window.location.href = data.ReturnUrl;
    }, "json");
}
var i = 0;
$(document).ready(function () {
    if ($("#gender").is(":hidden") == false && $("#birthday").is(":hidden") == false) {
        $('#info_acc').css("margin-top", "13px");
    }

    if ($("#birthday1").is(":visible") == true) {
        
        $('#info_acc').css("margin-top", "0px");
    }

    $("#cityList").addSelectUI({
        scrollbarWidth: 15, //default is 10
        onchange: function () {
            $.post("/Default/ChangeCity",
                         { cityId: $("#cityList").val() },
                         function () {
                             window.location.href = '/';
                         }
                );

        }
    });



    $('.close-message').click(function () {
        $('.error').slideUp();
        $('.inform').slideUp();
    });

    $(".form_sendmail, .btn_sendmail").mouseup(function () {
        return false;
    });

    $(document).mouseup(function () {
        if (i == 1) {
            $(".form_sendmail").slideUp();
            $('#iconmail').attr("src", "http://www.cungmua.com/Resources/images/general/button_mail_left.png");
            i = 0;
        }
    });

    $("#scroll").hover(
        function () {
            $(this).attr("src", "http://www.cungmua.com/Resources/images/btn_top_hover.png");
        },
        function () {
            $(this).attr("src", "http://www.cungmua.com/Resources/images/btn_top.png");
        }
    );

    $(".btn_sendmail").click(function () {

        $('input.button_input').removeClass('button');
        if (i == 0) {
            $(".form_sendmail").slideDown();
            $('#iconmail').attr("src", "http://www.cungmua.com/Resources/images/general/button_mail_down.png");
            i = 1;
            $('.form_sendmail div.email2').removeClass("email2-erro");
            $("#tbEmail").val("Email của bạn");
            $('#erroemail').hide();
            $("#tbEmail").focus();
        }
        else {
            $(".form_sendmail").slideUp();
            $('#iconmail').attr("src", "http://www.cungmua.com/Resources/images/general/button_mail_left.png");
            i = 0;
        }
    });


    $('#tbEmail').focus(function () {
        if (document.getElementById("tbEmail").value == "Email của bạn") {
            document.getElementById("tbEmail").value = "";
            $('#erroemail').hide();
        }
    });

    $('#tbEmail').blur(function () {
        if (document.getElementById("tbEmail").value == "") {
            document.getElementById("tbEmail").value = "Email của bạn";
            $('#erroemail').hide();
        }
    });

    $('#formSubscriberMenuTop').submit(function () {
        return SubmitForm();
    });
});

function SubmitForm(obtion) {

    if (obtion == 0) {
        var email = $('#tbEmail').val();
        if ($.trim(email) == '' || !validateEmail(email)) {
            $('.form_sendmail div.email2').addClass("email2-erro");
            $('#msgemail').html("Email không hợp lệ!");
            $('.error').slideDown();
            var hideMessage = 0;
            hideMessage = setInterval(function () {
                $('.error').slideUp();
                clearInterval(hideMessage);
            }, 3000);
        }
        else {
            if (!clicked) {
                clicked = true;
                $.post("/SubcriberSubmenu",
                { email: email },
                function (data) {
                    if (!data.IsValid) {
                        $('#msgemail').html("Email không hợp lệ!");
                        $('.error').slideDown();
                        var hideMessage = 0;
                        hideMessage = setInterval(function () {
                            $('.error').slideUp();
                            clearInterval(hideMessage);
                        }, 3000);
                    }
                    else {
                        $('.form_sendmail div.email2').removeClass("email2-erro");

                        //$("#tbEmail").removeClass("input-validation-error");
                        $("#tbEmail").val("Email của bạn");
                        $('#iconmail').attr("src", "http://www.cungmua.com/Resources/images/general/button_mail_left.png");
                        i = 0;
                        $('#erroemail').hide();
                        $(".form_sendmail").slideUp();
                        //$.ShowErrorPopup("Bạn đã đăng ký nhận email thành công!");
                        $('#Span2').html("Bạn đã đăng ký nhận email thành công!");
                        $('.inform').slideDown();
                        var hideMessage = 0;

                        hideMessage = setInterval(function () {
                            $('.inform').slideUp();
                            clearInterval(hideMessage);
                        }, 10000);
                    }
                });
            }
        }
    }
    else if (obtion == 1 || obtion == 2) {
        if (!clicked) {
            clicked = true;
            $.post("Default/SubmitGenderCustomerFromMenuTop",
                        { gender: obtion },
                        function (data) {
                            if (data.IsValid) {
                                $("#gender").hide();
                                $("#birthday").show();
                                $('#info_acc').css("margin-top: 13px;");
                            }
                        });
        }
    }
    else {
        var day = $("#CurrentDate1").val();
        var month = $("#CurrentMonth1").val();
        var year = $("#CurrentYear1").val();
        if (!clicked) {
            clicked = true;
            $.post("Default/SubmitBirthdayCustomerFromMenuTop",
                        { day: day, month: month, year: year },
                        function (data) {
                            if (!data.IsValid) {
                                $('#msgemail').html("Ngày tháng năm sinh không hợp lệ!");
                                $('.error').slideDown();
                                var hideMessage = 0;
                                hideMessage = setInterval(function () {
                                    $('.error').slideUp();
                                    clearInterval(hideMessage);
                                }, 3000);

                            }
                            else {
                                $(".newsletter").hide();
                                $('#info_acc').css("margin-top", "13px");
                            }
                        });
        }
    }

    return false;
};