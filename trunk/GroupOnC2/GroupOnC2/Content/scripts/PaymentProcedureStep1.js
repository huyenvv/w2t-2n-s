var haveShippingCostDefine ="";
var lastDistrictChoose = "";
var lastDistrictIndex = "";
var isSecondClick = false;
var ready = $(document).ready(function () {
    lastDistrictChoose = $('#HDistrictId option:selected').text();
    lastDistrictIndex = $('#HDistrictId').selectedIndex;
    $("#buttonDoBuy").bind("click", function () {
        DoBuyStep1();
        return false;
    });
    $("#PaymentForm").bind('keypress', function (e) {
        if (e.keyCode == 13) {
            DoBuyStep1();
            return false;
        }
    });
    //when cityChange then we push user back to deal details with new deal of new city
    $("#HCityId").bind('change', function () {
        PushUserBackToDealDetail(parseInt(this.value, 10));
    });
    $("#HDistrictId").bind('change', function () {
        //    isSecondClick = true;
        // DoCalculateShippingCost();
       // alert("change");
        GetWardFromDistrictId();
    });
    $("#HDistrictId").bind('focus', function () {
        /*// if (isSecondClick == false) {
        $('#ShippingCostAnnounceText').html("");
        $.each($('#HDistrictId option'), function (i, item) {
        if (i.toString() == lastDistrictIndex) {                   
        $(item).text(lastDistrictChoose);
        }

        });
        //  }
        // isSecondClick = false;*/
    });
    //$("#HFullName").focus();
    $("#HFullName").select();
    DoCalculateShippingCost();

    //NOte 14-3-2012 , we bind deliveryMethod Radiobutton here
    $("#DeliveryAtCompanyRadioButton").bind("click", function () {
        ShowOrHideAdditionalInfo(false);
    });
    $("#DeliveryAtHomeRadioButton").bind("click", function () {
        ShowOrHideAdditionalInfo(true);
    });
    RebindDeliveryMethod();
});
function RebindDeliveryMethod() {
     $(".chooseDeliveryType").removeClass("active");
     var deliveryType = $("input[name = DeliveryMethod]").val();

    if (deliveryType == deliveryAtHome) {
        $("#AtHome").addClass("active");
        ShowOrHideAdditionalInfo(true);

    }
    else if (deliveryType == deliveryAtCompany) {
        $("#AtCompany").addClass("active");
        ShowOrHideAdditionalInfo(false);

    }

}

function DoChooseDeliveryMethod(divClick) {
    $(".choosePaymentType").removeClass("active");
    var currentId = divClick.attr('id').toString();
    divClick.addClass("active");
    switch (currentId) {
        case "AtHome": //we don't have any Payment Type yet, 
            $("input[name=DeliveryMethod]").val(deliveryAtHome);
            break;
        case "AtCompany":
            $("input[name=DeliveryMethod]").val(deliveryAtCompany);
            break;
    }
    RebindDeliveryMethod();
    return false;

}


function ShowOrHideAdditionalInfo(isShow) {
    if (isShow) {
        $("#BuyAtHomeHolder").append($(".form_buy"));
        $("#BuyAtHomeHolder").fadeIn();
        $("#BuyAtCompanyHolder").fadeOut();
        $("#AdditionInfoDiv").show();
        $("#CompanyAddressInfo").hide();
        $(".form_buy").show();

    }
    else {
        $("#BuyAtHomeHolder").fadeOut();
        $("#BuyAtCompanyHolder").fadeIn();
        $("#BuyAtCompanyHolder").append($(".form_buy"));
        $("#AdditionInfoDiv").hide();
        $("#CompanyAddressInfo").show();
    }
}

function DoCalculateShippingCost() {
    $.each($('#HDistrictId option'), function (i, item) {
        if (i.toString() == lastDistrictIndex) {
            $(item).text(lastDistrictChoose);
        }
    });
    //cheat , check if the name is contain (=> must calculate shipping cost
        if ($('#HDistrictId option:selected').text().toLowerCase().indexOf("(") >= 0) {
            isShippingFree = false;
            var value = haveShippingCostDefine.replace("(", "").replace(")","");
            $('#ShippingCostAnnounceText').html(value);
        }
        else {
            isShippingFree = true;
            $('#ShippingCostAnnounceText').html("");
        }
        //we must cache the current name of district
        lastDistrictChoose = $('#HDistrictId option:selected').text();
        lastDistrictIndex = $('#HDistrictId  option:selected').index();

   
    $('#HDistrictId option:selected').text($('#HDistrictId option:selected').text().replace(haveShippingCostDefine, "")); 
    UpdateOrderBasket();
    }
function DoBuyStep1() {
  /*  //we check case user change cityId here (nationalDeal)--not use anymore deal to shipping deal
    if ($("#CacheCurrentCityId").val() != $("#HCityId").val()) {
        PushUserBackToDealDetail(parseInt($("#HCityId").val(), 10));
        return false;
    }*/
    //NOTE 5-2-2012 Add Ward
    $("#CurrentWardName").val($('#HWardId option:selected').text());
    $('#CurrentCityName').val($('#HCityId option:selected').text());
  //  alert($('#CurrentCityName').val());
    $('#CurrentDistrictName').val($('#HDistrictId option:selected').text());
    var f = $("#PaymentForm");
    var action = "/PaymentProcedure/PaymentStep1GetReceiverInformation";
    var form = f.serialize();
    $.post(action,
                form,
                function (result) {
                    if (!result.IsValid) {
                        if (result.RedirectUrl != null && result.RedirectUrl != "") {
                            // alert(result.RedirectUrl);
                            window.location = result.RedirectUrl;
                        }
                        else {
                            $.ValidateUI(result.Errors);
                            if ($("#VoucherMode-Error").is(":visible")) {
                                $('html').animate({ scrollTop:0 }, 'fast');
                            }
                        }
                    } else {
                        window.location = result.RedirectUrl;
                    }
                });
    return false;
}

function PushUserBackToDealDetail() {
 /*   if (!confirm("Bạn phải thực hiện lại quá trình đặt hàng nếu chuyển thành phố, bạn thật sự muốn chuyển?")) {
        /*  alert($("#CacheCurrentCityId").val().toString());#1#
        $("#HCityId").val($("#CacheCurrentCityId").val().toString());
        return false;
    }
    else {*/
        //must call ajax to get the new url to push user back
        var f = $("#PaymentForm");
        var action = "/PaymentProcedure/PushUserBackToDealDetails";
        var form = f.serialize();
   //  $("#contentHolder").block({ message: '<h1><img src="/Resources/images/ajax-loader.gif" /></h1>' });
        $.post(action,
                form,
                function (result) {

                    if (result.Error == "") {
                        alert("Có Deal tương tự ở thành phố bạn chọn , bạn sẽ được chuyển đến trong giây lát !!!");
                        window.location = result.RedirectUrl;

                    } else {
                        //we reload District here and recalculate shipping code
                        GetDistrictOfCity($("#HCityId").val());
                        // alert("có lỗi xảy ra :" + result.Error);
                //        $("#contentHolder").unblock();
                    }
                });
        return false;
   // }
    }
    function GetDistrictOfCity(cityId) {
        var action = '/District/GetAllDistrictByCity?CityId=' + cityId;
        $.getJSON(action, function (data) {
            MakeDistrictList(data);
         //   ShowDefaultDistrict();
        });
    }

    function MakeDistrictList(data) {
        $("#HDistrictId option").remove();
        $.each(data.DistrictList, function (i, item) {

            $('#HDistrictId').append($('<option></option>').val(item.Id).html(item.Name));
        });
        lastDistrictChoose = $('#HDistrictId option:selected').text();
        lastDistrictIndex = $('#HDistrictId').selectedIndex;
        DoCalculateShippingCost();

    }
    function GetWardFromDistrictId() {
        var currentDistrictId = $("#HDistrictId").val(); ;
        var action = "/PaymentProcedure/GetWardByDistrictId";
        $.post(action,
                { DistrictId: currentDistrictId },
                function (result) {
                    if (result.length > 1) {
                        //  alert(result.length);
                        $('#HWardId').show();
                        $("#HWardId").parent().parent().show();
                        $('#HWardId option').remove();
                        $.each(result, function (i, item) {
                            $('#HWardId').append($('<option></option>').val(item.Value).html(item.Text));
                        });
                    }
                    else {
                        //  alert("bb");
                        $('#HWardId option').remove();
                        $('#HWardId').append($('<option></option>').val("-1").html("--chọn phường--"));
                        $("#HWardId-Error").hide();
                        $("#HWardId").hide();
                        $("#HWardId").parent().parent().hide();
                    }

                });
        return false;
    }