var ready = $(document).ready(function () {

    $(".choosePaymentType").bind("click", function () {
        DoChoosePaymentType($(this));
        return false;
    });

    $("#buttonDoBuy").bind("click", function () {
        DoBuyStep2();
        return false;
    });
    $("#PaymentForm").bind('keypress', function (e) {
        if (e.keyCode == 13) {
            DoBuyStep2();
            return false;
        }
    });
    $("#buttonDoBuy").focus();
    //  alert($("input[name = PaymentType]").val());
    //we will choose default value here
    ReMapCurrentPaymentType();
});

function ReMapCurrentPaymentType() {
    $(".choosePaymentType").removeClass("active");
    var paymentType = $("input[name = PaymentType]").val();
    
    if (paymentType == PayByCash) {
        $("#PayByCash").addClass("active");

    }
    else if (paymentType == PayByLocalCard) {
        $("#PayByATM").addClass("active");

    }
    else if (paymentType == PayByCreditCard) {
        $("#PayByLocalCard").addClass("active");


    }
    else if (paymentType == PayByCashAtCompany) {
        $("#PayByCashAtCompany").addClass("active");


    }
   
    //check if there are 1 object that have the active class
    if ($(".choosePaymentType").hasClass("active") == false) {
   
        //we must set default
        var div = $(".choosePaymentType").first();
        div.addClass("active");
        DoChoosePaymentType(div);
    }
}

function DoChoosePaymentType(divClick) {
    $(".choosePaymentType").removeClass("active");
    var currentId = divClick.attr('id').toString();
    divClick.addClass("active");
    switch (currentId) {
        case "PayByCash": //we don't have any Payment Type yet, 
            $("input[name=PaymentType]").val(PayByCash);
            break;
        case "PayByATM":
            $("input[name=PaymentType]").val(PayByLocalCard);
            break;
        case "PayByLocalCard":
            $("input[name=PaymentType]").val(PayByCreditCard);
            break;
        case "PayByCashAtCompany":
            $("input[name=PaymentType]").val(PayByCashAtCompany);
            break;
    }
 //   alert($("input[name = PaymentType]").val());

}

function DoBuyStep2() {

    var f = $("#PaymentForm");
    var action = "/PaymentProcedure/PaymentStep2ChoosePaymentType";
    var form = f.serialize();
    $("#buttonDoBuy").attr('disabled', 'disabled');
    $.post(action,
                form,
                function (result) {
                    if (!result.IsValid) {
                        if (result.RedirectUrl != null && result.RedirectUrl != "") {
                            window.location = result.RedirectUrl;
                        }
                        else {
                            $("#buttonDoBuy").attr('disabled', '');
                            $.ValidateUI(result.Errors);
                            if ($("#VoucherMode-Error").is(":visible")) {
                                $('html').animate({ scrollTop: 0 }, 'fast');
                            }
                        }

                    } else {
                        window.location = result.RedirectUrl;
                    }
                });
    return false;
}