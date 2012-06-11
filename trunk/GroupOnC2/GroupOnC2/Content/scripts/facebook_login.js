function facebook_login() {
    
    FB.login(function (response) {
        if (response.authResponse) {
               FacebookConnectMySite(response.authResponse);
        }  else {
                FB.logout(function (response) {
                    alert("Bạn cần cung các thông tin cơ bản để có thể đăng nhập cungmua.com");
                });
        }

    }, { scope: 'email' });
}

function FacebookConnectMySite(facebookSession) {
    document.location = SITE_ROOT_URL + '/Profile/FacebookLogin?' + "&uid=" + facebookSession.userID + "&token=" + facebookSession.accessToken + "&returnUrl=" + window.location;
}

function Logout() {
    FB.getLoginStatus(handleSessionResponse);
}

function handleSessionResponse(response) {
    if (!response.session) {
        return;
    }

    FB.logout();
}

var FB_SESSION = "";

function AutoDetectLogin(IsAuthenticated, IsFacebookAuthenticated) {
    if(IsAuthenticated == "True") // da dang nhap
    {
        if(IsFacebookAuthenticated == "False") // dang nhap bang groupon
        {
            return;
        }
        else
        {
            FB.getLoginStatus(function (response) { // neu mat facebook session thi tu dong logout
                if (!response.session) {
                    FB.logout();
                    document.location = SITE_ROOT_URL + "/Profile/Logout";
                }
            });
        } 
    }
    else //chua dang nhap
    {
        FB.getLoginStatus(function (response) { // neu ton tai facebook session thi auto login bang facebook
            if (response.session) {
                FacebookConnectMySite(response.session);
            }
        });
    }
}