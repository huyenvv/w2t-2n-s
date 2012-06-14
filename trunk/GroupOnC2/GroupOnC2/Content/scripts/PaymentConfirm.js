
function CheckPhoneStatus(phone) {

    if (phone != "") {
        $("#RewardPointBlock").show();
        $("#ChangePhoneBlock").hide();    
    }
    else {
        $("#RewardPointBlock").hide();
        $("#ChangePhoneBlock").show();
    }
}

function OnUpdatePhoneSuccess(data) {
    var newMobile = data;
    $("#RewardPhone").html(newMobile);

    $("#RewardPointBlock").show();
    $("#ChangePhoneBlock").hide();

    return false;
}

function SwitchPanel() {
    var mobile = $("#RewardPhone").html();
    $("#txtRewardPhone").val(mobile);

    $("#RewardPointBlock").hide();
    $("#ChangePhoneBlock").show();
}

function checkEmail(inputvalue) {
    var pattern = /^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.([a-zA-Z])+([a-zA-Z])+/;
    if (pattern.test(inputvalue)) {
        return true;
    } else {
        return false;
    }
}


function isMobileValid(phoneNum) {
    var re = /^(090|091|092|093|094|095|096|097|098|099|0120|0121|0122|0123|0124|0125|0126|0127|0128|1029|0160|0161|0162|0163|0164|0165|0166|0167|0168|0169|0190|0191|0192|0193|0194|0195|0196|0197|0198|0199|0188)\d+$/;
    if (phoneNum.match(re)) {
        var mobileValidRegexPattern = '090|091|092|093|094|095|096|097|098|099|0120|0121|0122|0123|0124|0125|0126|0127|0128|1029|0160|0161|0162|0163|0164|0165|0166|0167|0168|0169|0190|0191|0192|0193|0194|0195|0196|0197|0198|0199|0188';
        var mobileValids = mobileValidRegexPattern.split('|');

        for (var i = 0; i < mobileValids.length; i++) {
            var ext = mobileValids[i];
            var phoneExt = '';
            var restNum = '';
            if (phoneNum.length > ext.length) {
                phoneExt = phoneNum.substring(0, ext.length);
                restNum = phoneNum.substring(ext.length);
            }

            if (ext == phoneExt && restNum.length == 7) {
                return true;
            }
        }
        return false;
    }
    else {
        return false;
    }
}
