

function google_login() {
    var top = (screen.height - 400) / 2;
    var left = (screen.width - 570) / 2;
    var googlewindow = window.open(SITE_ROOT_URL + '/Profile/GoogleLogin?returnUrl=' +  window.location, "google_account", "status=0,toolbar=0,width=570,height=400,top=" + top + ",left=" + left);
}

function yahoo_login() {
    var top = (screen.height - 500) / 2;
    var left = (screen.width - 570) / 2;
    var googlewindow = window.open(SITE_ROOT_URL + '/Profile/YahooLogin?returnUrl=' + window.location, "yahoo_account", "status=0,toolbar=0,width=570,height=500,top=" + top + ",left=" + left);
}