

var LoginAndRegisterFunction = {
    Email: "",
    PasswordLogin: "",
    Name: "",
    Phone: "",
    LoginDone: function (result) {
        location.href = location.pathname;
    },
    RegisterDone: function () {
        location.href = location.pathname;
    },
    ForgotPasswordDone: function () {
        location.href = location.pathname;
    },
    ShowWindow: function (typeOfWindow) {
        clicked = false;
        //auto fill data here
        if (typeOfWindow == 1) {
            $('.register').hide();
            $('.foget').hide();
            $('.login').show();
            $('.checkboxLogin').attr("checked", "checked");
            setTimeout("$('#Email').focus();$('#Email').select();", 10);
        }
        else if (typeOfWindow == 3) {
            $('.login').hide();
            $('.register').hide();
            $('.foget').show();
            $('#ForgotEmail').val($('#Email').val());
            setTimeout(" $('#ForgotEmail').focus();$('#ForgotEmail').select();", 10);
        }
        else {
            $('.login').hide();
            $('.foget').hide();
            $('.register').show();

            $('#RegisterModel_Agree').attr("checked", "checked");
            $('#Name').val(this.Name);
            $('#EmailRegister').val(this.Email);
            $('#Phone').val(this.Phone);
            setTimeout(" $('#EmailRegister').focus();$('#EmailRegister').select();", 10);
        }
        $.BigLoading(610, 'divpopup');
        
    },
    CloseWindow: function () {
        $.ClosePopup();
        $('#overlay').remove();
        $('.login').hide();
        $('.register').hide();
        $('.foget').hide();
    }
};

function DoLogin() {
    if (!clicked) {
        //$('#loginBtn').addClass("button_dis");
        //$('#loginBtn').removeClass("button");
        var f = $("#LoginForm");
        var action = f.attr("action");
        var form = f.serialize();
        $.post(action,
            form,
            function (result) {
                if (!result.IsValid) {
                    $.ValidateUI(result.Errors);
                } else {
                    LoginAndRegisterFunction.LoginDone(1);
                    //LoginAndRegisterFunction.CloseWindow();
                }
            });
        }

    return false;
};

function DoRegister() {
    if (!clicked) {
        var f = $('#RegisterForm');
        var action = f.attr("action");
        var form = f.serialize();
        $.post(action,
            form,
            function (result) {
                if (!result.IsValid) {
                    $.ValidateUI(result.Errors);
                } else {
                    LoginAndRegisterFunction.RegisterDone(1);
                    //LoginAndRegisterFunction.CloseWindow();
                }
            });
    }

    return false;
};

function DoForgotPassword() {
    if (!clicked) {
        var f = $('#ForgotPasswordForm');
        var action = f.attr("action");
        var form = f.serialize();
        $.post(action,
            form,
            function (result) {
                if (result.IsValid) {
                    //LoginAndRegisterFunction.CloseWindow();
                    alert(result.JsonData);
                    LoginAndRegisterFunction.ForgotPasswordDone(1);
                }
                else {
                    $.ValidateUI(result.Errors);
                }
            });
    }

    return false;
};

function HideLoginShowRegister() {
    LoginAndRegisterFunction.ShowWindow(2);
}
