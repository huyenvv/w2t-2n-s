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

		private GROUPONEntities1 db = new GROUPONEntities1();
		
		//
		// GET: /Account/
		public ActionResult _Layout()
		{
			return View();
		}

		public ActionResult Register()
		{
			THONGTINMEMBER register = new THONGTINMEMBER();
			return View(register);
		}

		[HttpPost]
		public ActionResult Register(FormCollection col)
		{
			THONGTINMEMBER member = new THONGTINMEMBER();
			member.Avatar = null;
			member.UserName = col["TenDangNhap"];
			member.Email = col["RegisterModel.EmailRegister"];
			var gioiTinh = ValueProvider.GetValue("RegisterModel.Gender");
			if (gioiTinh != null)
			{
				string gt = gioiTinh.AttemptedValue;
				if (gt == "Nam")
					member.GioiTinh = true;
				else member.GioiTinh = false;
			}

			member.MaTK = "MB" + (db.LaySoLuongMember() + 1).ToString();
			member.MaTT = "TT01";
			string ngaySinh = col["RegisterModel.CurrentDate"];
			string thangSinh = col["RegisterModel.CurrentMonth"];
			string namSinh = col["RegisterModel.CurrentYear"];
			member.NgaySinh = DateTime.Parse(thangSinh + "/" + ngaySinh + "/" + namSinh);
			member.Password = col["RegisterModel.PasswordRegister"];

			member.SDT = col["RegisterModel.Phone"];
			member.Ten = col["RegisterModel.Name"];


			bool res = db.ThemMember(member);

			return RedirectToAction("Index", "Home");
		}

		public ActionResult Login(LOGINFORM login)
		{
			return View(login);
		}

		[HttpPost]
		public ActionResult Login(FormCollection collect, string returnUrl)
		{
			string userName = collect["LoginModel.Username"];
			string password = collect["LoginModel.Password"];

			bool result = db.KiemTraThongTinDangNhap(userName, password);
			if (result == true)
			{
				TAIKHOAN TK = db.TAIKHOANs.SingleOrDefault(p => p.UserName == userName && p.Password == password);
				string MaTK = TK.MaTK;
				
				FormsAuthentication.SetAuthCookie(MaTK, true);
				Response.Cookies["MaTK"]["MaTK"] = MaTK;
				Response.Cookies["MaTK"].Expires = DateTime.Now.AddMinutes(10);
				if (Url.IsLocalUrl(returnUrl) &&
					returnUrl.Length > 1 &&
					returnUrl.StartsWith("/") &&
					!returnUrl.StartsWith("//") &&
					!returnUrl.StartsWith("/\\"))
				{
					return Redirect(returnUrl);
				}
				else
					return RedirectToAction("Index", "Home");

			}
			else
			{
				ModelState.AddModelError("", "Tên người dùng và mật khẩu không hợp lệ.");
				return View();
			}

			

		}

		private void MigrateShoppingCart(string userName)
		{
			var cart = ShoppingCart.GetCart(this.HttpContext);

			cart.MigrateCart(userName);
			Session[ShoppingCart.CartSessionKey] = userName;
		}



		#region Status Codes
		private static string ErrorCodeToString(MembershipCreateStatus createStatus)
		{
			// See http://go.microsoft.com/fwlink/?LinkID=177550 for
			// a full list of status codes.
			switch (createStatus)
			{
				case MembershipCreateStatus.DuplicateUserName:
					return "User name already exists. Please enter a different user name.";

				case MembershipCreateStatus.DuplicateEmail:
					return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

				case MembershipCreateStatus.InvalidPassword:
					return "The password provided is invalid. Please enter a valid password value.";

				case MembershipCreateStatus.InvalidEmail:
					return "The e-mail address provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidAnswer:
					return "The password retrieval answer provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidQuestion:
					return "The password retrieval question provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidUserName:
					return "The user name provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.ProviderError:
					return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				case MembershipCreateStatus.UserRejected:
					return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				default:
					return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
			}
		}
		#endregion
	}
}
