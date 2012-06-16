///<reference path="jquery-1.7.1.min.js"/>

$(document).ready(function () {
    $("#tbEmail").blur(function () {
        if (!$(this).val()) $(this).val("Nhập email để nhận thông tin khuyến mãi");
    });

    $("#tbEmail").focus(function () {
        if ($(this).val() == "Nhập email để nhận thông tin khuyến mãi") $(this).val("");
    });

    $("#content_acc").hide();

    $("#info_acc").hover(function () {
        $("#content_acc").show();
    }, function () { $("#content_acc").hide(); });
});



$(function () {
    var div = $selectDroplist_Manager.els;
    div.cityList.droplistTITLE.hover(function () {
        $('.DropListUIContainer').show();
    }, function () {
        $('.DropListUIContainer').hide();
    });
    $('.DropListUIContainer').hover(function () {
        $('.DropListUIContainer').show();
    }, function () {
        $('.DropListUIContainer').hide();
    });
    $('ul.SelectUI li').hover(function () {
        $('.DropListUIContainer').show();
    }, function () {
        $('.DropListUIContainer').hide();
    });
});