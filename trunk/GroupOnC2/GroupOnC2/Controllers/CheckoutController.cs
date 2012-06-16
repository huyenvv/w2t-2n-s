using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupOnC2.Models;

namespace GroupOnC2.Controllers
{
	[Authorize]
    public class CheckoutController : Controller
    {
		GROUPONEntities1 db = new GROUPONEntities1();
		//
        // GET: /Checkout/

        public ActionResult AddressAndPayment()
        {
            return View();
        }

		[HttpPost]
		public ActionResult AddressAndPayment(FormCollection values)
		{
			var donHang = new DONHANG();
			var ctDonHang = new CHITIETDONHANG();

			TryUpdateModel(ctDonHang);
			TryUpdateModel(donHang);

			try
			{
					donHang.MaTK = Request.Cookies["MaTK"]["MaTK"];
					donHang.MaDH = (db.LaySoLuongDonHang() + 1).ToString();
					donHang.UserName = values["UserName"];
					donHang.NgayDatHang = DateTime.Now;
					donHang.MaTT = "TT01";
					string thanhPho = values["ThanhPho"];
					string quan = values["Quan"];
					string phuong = values["Phuong"];
					string diaChi = values["DiaChi"];
					donHang.DiaChi = diaChi + " " + phuong + " " + quan + " " + thanhPho;
					donHang.Email = values["Email"];

					donHang.MaHTTT = "TT";
					donHang.MaTK = null;
					donHang.SDT = values["SDT"];
					donHang.TenNguoiNhan = values["Ten"];
					donHang.TongTien = Decimal.Parse(values["TongTien"]);

					ctDonHang.DonGia = Decimal.Parse(values["DonGia"]);
					ctDonHang.MaDH = donHang.MaDH;
					ctDonHang.MaSP = donHang.MaDH;

					db.DONHANGs.Add(donHang);
					db.SaveChanges();

					var cart = ShoppingCart.GetCart(this.HttpContext);
					cart.TaoDonHang(donHang);

					return RedirectToAction("Complete", new { id = donHang.MaDH });
				//}
			}
			catch (System.Exception ex)
			{
				return View(donHang);
			}
		}

		public ActionResult Complete(string id)
		{
			bool isValid = db.DONHANGs.Any(o => o.MaDH == id);
			if (isValid)
				return View(id);
			else return View("Error");
		}
    }
}
