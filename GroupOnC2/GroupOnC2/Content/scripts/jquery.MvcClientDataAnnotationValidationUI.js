$.ValidateUI = function (errors) {
    var inputFocus;
    $(".input-validation-error").removeClass("input-validation-error");
    $(".Enternet-Field-Validation").hide();
    $(".Enternet-Field-Validation").removeClass("form_buy_err");
    $.each(errors, function (index, error) {
        if (index == 0)
            inputFocus = $("#" + error.Key);
      
        $("#" + error.Key).addClass("input-validation-error");
        $("#" + error.Key + "-Error").addClass("form_buy_err");
        $("#" + error.Key + "-Error").show();
        $("#" + error.Key + "-Error").html(error.ErrorMessage);
    });

    inputFocus.focus();
}
$.ResetValidateUI = function () {
    $(".input-validation-error").removeClass("input-validation-error");
    $(".Enternet-Field-Validation").hide();
    $(".Enternet-Field-Validation").removeClass("form_buy_err");
}
$.GetErrors = function (errors) {
    var errorStr = "";
    $.each(errors, function (index, error) {
        errorStr += "- " + error.ErrorMessage + "\n";
    });
    return errorStr;
}

$.GetNiceErrors = function (errors) {
    var errorStr = "";
    $.each(errors, function (index, error) {
        errorStr += "<li>" + error.ErrorMessage + "</li>";
    });
    return errorStr;
}