using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupOnC2.Models;
using PagedList.Mvc;
using PagedList;
using System.Transactions;
using System.Web.Helpers;


namespace GroupOnC2.Controllers
{

	public class DoanhNghiepController : Controller
	{
		private GROUPONEntities1 db = new GROUPONEntities1();
		//
		// GET: /DoanhNghiep/

		public ActionResult DoanhNghiep()
		{
			return View();
		}

		public ActionResult SanPham()
		{
			var lstSanPham = from p in db.SANPHAMs
							 where p.MaLoaiSP == "DL"
							 select p;
			return View(lstSanPham.ToList());
		}

		public ActionResult DonHang()
		{
			List<DONHANG> lstDonHangs = db.LayDSDonHangTheoLoai("DL");
			ThongKe thongKe = new ThongKe(lstDonHangs);
			return View(thongKe);
		}

		public ActionResult ThongTinTaiKhoan()
		{
			string maTK = Request.Cookies["MaTK"]["MaTK"];
			THONGTINDOANHNGHIEP doanhNghiep = db.THONGTINDOANHNGHIEPs.SingleOrDefault(p => p.MaTK == maTK);
			return View(doanhNghiep);
		}

		

		public ActionResult ChangedAccIformation(string id)
		{
			THONGTINDOANHNGHIEP dn = db.THONGTINDOANHNGHIEPs.SingleOrDefault(p => p.MaTK == id);
			return View(dn);
		}

		[HttpPost]
		public ActionResult ChangedAccIformation(FormCollection collection)
		{
			string maTK = Request.Cookies["MaTK"]["MaTK"];
			THONGTINDOANHNGHIEP doanhNghiep = db.THONGTINDOANHNGHIEPs.SingleOrDefault(p => p.MaTK == maTK);
			TAIKHOAN taiKhoan = db.TAIKHOANs.SingleOrDefault(p => p.MaTK == maTK);

			string DiaChi = collection["DiaChi"];
			
			doanhNghiep.DiaChi = DiaChi ;
			doanhNghiep.Email = collection["Email"];
			doanhNghiep.Website = collection["Website"];
			
			doanhNghiep.TenDN = collection["TenDN"];

			taiKhoan.DiaChi = doanhNghiep.DiaChi;
			taiKhoan.Email = doanhNghiep.Email;
			taiKhoan.SDT = collection["SDT"];

			db.SaveChanges();
			return RedirectToAction("DoanhNghiep", "DoanhNghiep");
		}
	}
}
