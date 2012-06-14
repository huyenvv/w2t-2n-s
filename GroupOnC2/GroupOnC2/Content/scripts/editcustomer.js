$(document).ready(function () {

    $("#Phone, #Name").keyup(function (event) {
        if (event.keyCode == 13) {
            EditInfoCustomer();
        }
    });

    $('#iconedit').click(function () {
        EditInfoCustomer();
    });
    $('.imgCheck').click(function () {
        var cityId = $(this).attr("cityId");
        var cityName = $(this).attr("cityName");
        var temp = $(this).attr("cityId");
        var title = $('#title' + temp).text();
        var email = $(this).attr("email");
        var subscribeId = $("#subscriber" + cityId).val();
        if (title == "Nhận mail") {
            var r = confirm("Bạn muốn nhận thông tin khuyến mãi từ thành phố " + cityName + "?");
            if (r == true) {
                $.post(SITE_ROOT_URL + "/Profile/Subscribe", { email: email, cityId: cityId }, function (data) {
                    $("#subscriber" + cityId).val(data.subscriberId);
                    $("#img" + cityId).attr("src", "../images/general/check.png");
                    $("#title" + cityId).text("Ngưng nhận");
                    //$(".info_user2").unblock();
                }, "json");
            }
            else {
                //$(".info_user2").unblock();
                return;
            }
        }
        else {
            var r = confirm("Bạn có chắc là không muốn nhận thông tin khuyến mãi từ thành phố " + cityName + "?");
            if (r == true) {
                $.post(SITE_ROOT_URL + "/Profile/UnSubscribe", { id: subscribeId }, function (data) {
                    $("#img" + cityId).attr("src", "../images/general/delete.png");
                    $("#title" + cityId).text("Nhận mail");
                    //$(".info_user2").unblock();
                }, "json");
            }
            else {
                //$(".info_user2").unblock();
                return;
            }
        }
    });

    $("#btEdit").click(function () {
        EditInfoCustomer();
    });
    $('#Phone').ForceNumericOnly();
    $(".ReceiveEmailButton").click(function () {
        /*var divBlock = $(".info_user2");
        if (divBlock != null)
        divBlock.block({ message: '<h1><img src="/Resources/images/ajax-loader.gif" /></h1>' });*/
        var cityId = $(this).attr("cityId");
        var cityName = $(this).attr("cityName");
        var title = $(this).text();
        var email = $(this).attr("email");
        var subscribeId = $("#subscriber" + cityId).val();
        if (title == "Nhận mail") {
            var r = confirm("Bạn muốn nhận thông tin khuyến mãi từ thành phố " + cityName + "?");
            if (r == true) {
                $.post(SITE_ROOT_URL + "/Profile/Subscribe", { email: email, cityId: cityId }, function (data) {
                    $("#subscriber" + cityId).val(data.subscriberId);
                    $("#img" + cityId).attr("src", "../images/general/check.png");
                    $("#title" + cityId).text("Ngưng nhận");
                    //$(".info_user2").unblock();
                }, "json");
            }
            else {
                //$(".info_user2").unblock();
                return;
            }
        }
        else {
            var r = confirm("Bạn có chắc là không muốn nhận thông tin khuyến mãi từ thành phố " + cityName + "?");
            if (r == true) {
                $.post(SITE_ROOT_URL + "/Profile/UnSubscribe", { id: subscribeId }, function (data) {
                    $("#img" + cityId).attr("src", "../images/general/delete.png");
                    $("#title" + cityId).text("Nhận mail");
                    //$(".info_user2").unblock();
                }, "json");
            }
            else {
                //$(".info_user2").unblock();
                return;
            }
        }
    });
});

function UpdateSubscriber(parameters) {

}

function EditInfoCustomer() {
    //$.BigLoading();
    if ($("#btEdit").text() == 'Chỉnh sửa') {
        $("#nameLabel").hide();
        $("#contentName").show();
        $("#BirthDayLabel").hide();
        $("#mobileLabel").hide();
        $("#GenderLabel").hide();
        $("#ContentGender").show();
        $("#ContentBirthDay").show();
        $("#contentMobile").show();
        $("#btEdit").text("Lưu lại");
        $('#iconedit').attr("src", "../images/general/save.png");
        $.RemoveBigLoading();
        $('#Name').focus();
        $('#Name').select();
    }
    else {
        /*var name = $('#firstName').val();
        name = $.trim(name);
        if (name == "") {
        $('#firstName').addClass('input-validation-error');
        return false;
        }
        if (isMobileValid($('#mobile').val())) 
        {
        // Make an ajax update
        $.post(SITE_ROOT_URL + "/Profile/UpdateInfoCustomer", { id: 486781, mobile: $('#mobile').val(), firstName:name}, function (data) {
        $("#nameLabel").html(data.firstName);
        $("#mobileLabel").html(data.mobile);
        $("#mobile").html(data.mobile);
        $("#firstName").html(data.firstName);
        $("#nameLabel").show();
        $("#contentName").hide();
        $("#mobileLabel").show();
        $("#contentMobile").hide();
        $("#btEdit").text("Chỉnh sửa");
        $('#iconedit').attr("src", "../images/general/edit.png");
        alert("Cập nhật thành công !");
        //$.RemoveBigLoading();
        }, "json"); 
        }
        else
        {
        alert('Số điện thoại không hợp lệ, vui lòng nhập lại.');
        }*/
        $('.form_acc_err').hide();
        $('.input_edit').removeClass('input-validation-error');
        //var divBlock = $(".info_user");
        //if (divBlock != null)
        //    divBlock.block({ message: '<h1><img src="../images/ajax-loader.gif" /></h1>' });
        $.post(SITE_ROOT_URL + "/Profile/UpdateInfoCustomer",
                { id: 486781,
                    mobile: $('#Phone').val(),
                    firstName: $('#Name').val(),
                    gender: $("input[name=group1]:checked").val(),
                    day: $("#CurrentDate").val(),
                    month: $("#CurrentMonth").val(),
                    year: $("#CurrentYear").val()
                }, function (data) {
                    if (data.success) {
                        $("#nameLabel").html(data.firstName);
                        $("#mobileLabel").html(data.mobile);
                        $("#Phone").val(data.mobile);
                        $("#BirthDayLabel").html(data.birthday);
                        $("#Name").val(data.firstName);
                        $("#GenderLabel").html(data.gender == 0 ? "Nam" : "Nữ");
                        $("#nameLabel").show();
                        $("#BirthDayLabel").show();
                        $("#GenderLabel").show();
                        $("#ContentGender").hide();
                        $(".newsletter").hide();
                        $('#info_acc').css("margin-top", "13px");
                        $("#contentName").hide();
                        $("#ContentBirthDay").hide();
                        $("#mobileLabel").show();
                        $("#contentMobile").hide();
                        $("#btEdit").text("Chỉnh sửa");
                        $('#iconedit').attr("src", "../images/general/edit.png");
                        //$(".info_user").unblock();
                    }
                    else {
                        $.ValidateUI(data.ajaxResult.Errors);
                        //$.ShowErrorPopup($.GetNiceErrors(data.ajaxResult.Errors));
                        $(".info_user").unblock();
                    }
                }, "json");
    }
}