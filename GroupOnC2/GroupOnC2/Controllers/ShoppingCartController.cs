using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupOnC2.Models;
using GroupOnC2.ViewModels;

namespace GroupOnC2.Controllers
{
    public class ShoppingCartController : Controller
    {
		GROUPONEntities1 db = new GROUPONEntities1();
		
		//
        // GET: /ShoppingCart/

        public ActionResult Index()
        {
			var cart = ShoppingCart.GetCart(this.HttpContext);

			var viewModel = new ShoppingCartViewModel
			{
				CartItems = cart.GetCartItems(),
				CartTotal = cart.GetTotal()
			};
            return View(viewModel);
        }

		//
		// GET: /ShoppingCart/AddToCart/5
		public ActionResult AddToCart(string MaSP)
		{
			var addedSanPham = db.SANPHAMs.Single(sp => sp.MaSP == MaSP);

			var cart = ShoppingCart.GetCart(this.HttpContext);
			cart.AddToCart(addedSanPham);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult RemoveFromCart(int id)
		{
			var cart = ShoppingCart.GetCart(this.HttpContext);

			string tenSP = db.CARTs1.Single(item => item.RecordId == id).SanPham.TenSP;

			int itemCount = cart.RemoveFromCart(id);

			var result = new ShoppingCartRemoveViewModel
			{
				Message = Server.HtmlEncode(tenSP) + "đă được xóa khỏi giỏ hàng của bạn.",
				CartTotal = cart.GetTotal(),
				CartCount = cart.GetCount(),
				ItemCount = itemCount,
				DeleteId  = id
			};

			return Json(result);
		}

		[ChildActionOnly]
		public ActionResult CartSummary()
		{
			var cart = ShoppingCart.GetCart(this.HttpContext);
			ViewData["CartCount"] = cart.GetCount();
			return PartialView("CartSummary");
		}
    }
}
