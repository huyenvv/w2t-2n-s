using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupOnC2.Models;
using System.Web.Security;

namespace GroupOnC2.Controllers
{
	public class AccountController : Controller
	{
		//
		// GET: /Account/

		public ActionResult _Layout()
		{
			return View();
		}

        public ActionResult Register()
        {
            Register register = new Register();
            return View(register);
        }

        public ActionResult Login(LOGINFORM login)
        {
            return View(login);
        }

		//[HttpPost]
		//public ActionResult DangNhap(TAIKHOAN model, string returnUrl)
		//{
		//    //if (ModelState.IsValid)
		//    //{
		//    //    if (Membership.ValidateUser(model.UserName, model.Password))
		//    //    {
		//    //        FormsAuthentication.SetAuthCookie(model.UserName, model.)
		//    //    }
		//    //}
		//}
	}
}
