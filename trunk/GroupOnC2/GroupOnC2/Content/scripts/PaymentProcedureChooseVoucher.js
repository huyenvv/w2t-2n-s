
var isShippingFree;
var currentShippingCharge = 0;
var orderAmount = 0;
var usedBonusMoney = 0;
var totalAmountWithoutShippingCost = 0;
var ready = $(document).ready(function () {
    $(".vSelect").bind("change", function () {
        UpdateOrderBasket();

    });
    UpdateOrderBasket();

});
    function UpdateOrderBasket() {
        var totalAmount = 0;
        var totalQuantity = 0;
        var totalWeight = 0;
        $(".vSelect").each(function () {
            var voucherRow = $(this).parent().parent();
            var voucherPrice = voucherRow.attr("price");
            var voucherWeight = parseFloat(voucherRow.attr("weight").replace(",", "."));
            var voucherQuantity = $(this).val();
            var voucherAmount = voucherPrice * voucherQuantity;
            totalQuantity += voucherQuantity;
            totalAmount += voucherAmount;
            totalWeight = totalWeight + (voucherWeight * voucherQuantity);
           
            voucherRow.find("td[holder='voucherAmount']").html("<b>" + FormatCurrency(voucherAmount, 'vi-VN', null, true) + "đ</b>");
        });
       
       
       // $("#totalAmount").html("<b>"+FormatCurrency(totalAmount, 'vi-VN', null, true) + "đ</b>");

        orderAmount = totalAmount;
        if ($("#NoPostBonusMoney").length > 0) {
            var bonusMoneyAmount = parseInt($("#NoPostBonusMoney").val());
             
            orderAmount = orderAmount - bonusMoneyAmount;
            if (orderAmount <= 0) {
                bonusMoneyAmount = totalAmount;
                orderAmount = 0;
            }
            $("#usedbonusMoney").html("<b>" + "- " + FormatCurrency(bonusMoneyAmount, 'vi-VN', null, true) + "đ</b>");
            usedBonusMoney = bonusMoneyAmount;
        }

        if (totalQuantity == 0 || orderAmount == 0) {
            $("#choise_tt_wrapper").hide();
        } else {
            $("#choise_tt_wrapper").show();
        }

        $("#CurrentTotalAmount").val("orderAmount");
       


        // voucher mode
        if ($("#AmountPerPoint").val() != -1) {
            var amount = $("#AmountPerPoint").val();
            var point = Math.round(totalAmount * amount / 10000);
            $("#atmPoint, #creditPoint").html(point);
        }

        //NOTE 13-2-2012 , here , we must recalculate Weight and ShippingCost
        $("#TotalWeight").val(totalWeight);
        //must reset currentShippingCharge
        currentShippingCharge = 0;
        DoCalculateShippingCode();
    }

   
    function DoCalculateShippingCode() {
       // orderAmount -= currentShippingCharge;        
        if (isShippingFree) {         
            $("#CurrentShippingCost").html("0đ");
            $("#OrderAmount").html("<b>" + FormatCurrency(orderAmount, 'vi-VN', null, true) + "đ</b>");
            currentShippingCharge = 0;
            //we must recalculate bonus money here            
            return;
        }
          //  var cityIdFrom = $("#HCityId").val();
            var cityIdTo = $("#HCityId").val();
            var Weight = $("#TotalWeight").val().replace(".",",");
      
            var action = "/PaymentProcedure/CalculateShippingCost";
            $.post(action,
                { cityIdTo: cityIdTo, Weight: Weight },
                function (result) {

                    currentShippingCharge = result;
                    $("#CurrentShippingCost").html("+ " + FormatCurrency(result, 'vi-VN', null, true) + "đ");
                //    totalAmountWithoutShippingCost = orderAmount;
                    orderAmount += currentShippingCharge;

                    //calculate bonusmoney
                    if ($("#NoPostBonusMoney").length > 0) {
                        var bonusMoneyAmount = parseInt($("#NoPostBonusMoney").val());
                        bonusMoneyAmount = bonusMoneyAmount - usedBonusMoney;
                        if (bonusMoneyAmount > 0) {
                            var cacheAmount = orderAmount;
                            orderAmount = orderAmount - bonusMoneyAmount;
                            if (orderAmount < 0) {
                                orderAmount = 0;
                                bonusMoneyAmount = cacheAmount;
                            }
                            $("#usedbonusMoney").html("<b>" + "- " + FormatCurrency(bonusMoneyAmount + usedBonusMoney, 'vi-VN', null, true) + "đ</b>");

                        }
                    }

                   // if (orderAmount)
                        $("#OrderAmount").html("<b>" + FormatCurrency(orderAmount, 'vi-VN', null, true) + "đ</b>");
                });
        }
    
    