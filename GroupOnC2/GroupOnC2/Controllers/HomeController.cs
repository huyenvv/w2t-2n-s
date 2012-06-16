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
using System.Web.Mvc;

namespace GroupOnC2.Controllers
{
	public class PagedList<T>
	{
		public bool HasNext { get; set; }
		public bool HasPrevious { get; set; }
		public List<T> Entities { get; set; }
	}
    public class HomeController : Controller
    {
        private GROUPONEntities1 db = new GROUPONEntities1();

        //
        // GET: /Home/

		public ViewResult Index(int? page)
		{
			var sanPhams = db.SANPHAMs.ToList();
			int pageSize = 12;
			int pageNumber = (page ?? 1);
			return View(sanPhams.ToPagedList(pageNumber, pageSize));
		}

		//
		// GET: /Home/Browse
		public ViewResult Browse(string MaLoaiSP, int? page)
		{
			var loai = db.LOAISANPHAMs.Single(g => g.MaLoaiSP == MaLoaiSP);
			var sanPhams = loai.SANPHAMs;
			int pageSize = 12;
			int n = sanPhams.Count;
			int pageNumber = (page ?? 1);
			return View(sanPhams.ToPagedList(pageNumber, pageSize));
			
		}
        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            ViewBag.MaLoaiSP = new SelectList(db.LOAISANPHAMs, "MaLoaiSP", "TenLoaiSP");
            return View();
        } 

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(SANPHAM sanpham)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sanpham);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.MaLoaiSP = new SelectList(db.LOAISANPHAMs, "MaLoaiSP", "TenLoaiSP", sanpham.MaLoaiSP);
            return View(sanpham);
        }

		//
		// GET: /Home/Details/5

		public ActionResult Details(string id)
		{
			var ctSp = db.CHITIETSANPHAMs.Single(r => r.MaSP == id);
			Response.Cookies["MaSP"]["MaSP"] = id;
			
			return View(ctSp);

		}
		[HttpPost]
		public ActionResult Details(FormCollection collection)
		{
			string MaTK = Request.Cookies["MaTK"]["MaTK"] ?? "";
			if (MaTK == ""|| !Request.IsAuthenticated)
				return RedirectToAction("Login", "Account");
			else
			{
				COMMENT comment = new COMMENT();
				comment.MaCM = "CM" + db.LaySoLuongComment().ToString();
				string MaSP = Request.Cookies["MaSP"]["MaSP"];
				comment.MaSP = MaSP;
				comment.MaTK = MaTK;
				comment.Ngay = DateTime.Now;
				comment.Noidung = collection["NoiDung"];

				db.COMMENTs.Add(comment);
				db.SaveChanges();
				//Request.Cookies["MaSP"]["MaSP"]
				comment.UserName = db.LayUserNameTheoMaTK(MaTK);
				return RedirectToAction("Details", "Home");
			}
		}

        //
        // GET: /Home/Delete/5
 
        public ActionResult Delete(string id)
        {
            SANPHAM sanpham = db.SANPHAMs.Find(id);
            return View(sanpham);
        }

        //
        // POST: /Home/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            SANPHAM sanpham = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

		private List<SANPHAM> GetTopSellingSanPhams(int skip, int count)
		{
			
		return db.SANPHAMs
				.OrderByDescending(a => a.CHITIETDONHANGs.Count()).Take(count).ToList();
			
		}


        //
        // GET: /Home/OrderInformation
		//public ActionResult MuaHang(string MaSP)
		//{
		//    CT_GIOHANG ctGioHang = new CT_GIOHANG(MaSP);
		//    ctGioHang.SoLuong += 1;
		//    string maTK = "";
		//    maTK = Request.Cookies["MaTK"]["MaTK"] ?? "";

		//    GIOHANG gioHang = db.GIOHANGs.SingleOrDefault(p => p.MaTK == maTK);
		//    if (gioHang == null)
		//    {
		//        gioHang = new GIOHANG();
		//        gioHang.MaTK = maTK;
		//    }
		//    gioHang.Them_CTGioHang(ctGioHang);TAIKHOANs.SingleOrDefault(p => p.MaTK == MaTK);
		//    //db.ThemGioHang(gioHang);
		//    return View(gioHang);
		//}

        public ActionResult AccountInformation()
        {
			string maTK = Request.Cookies["MaTK"]["MaTK"];
			THONGTINMEMBER member = db.THONGTINMEMBERs.SingleOrDefault(p => p.MaTK == maTK);
            return View(member);
        }

		[HttpPost]
		public ActionResult AccountInformation(FormCollection collection)
		{
			return RedirectToAction("ChangedAccIformation", "Home");
		}

		public ActionResult ChangedAccIformation()
		{
			string maTK = Request.Cookies["MaTK"]["MaTK"];
			THONGTINMEMBER member = db.THONGTINMEMBERs.SingleOrDefault(p => p.MaTK == maTK);
			return View(member);
		}

		[HttpPost]
		public ActionResult ChangedAccIformation( FormCollection collection)
		{
			string maTK = Request.Cookies["MaTK"]["MaTK"];
			THONGTINMEMBER member = db.THONGTINMEMBERs.SingleOrDefault(p => p.MaTK == maTK);
			TAIKHOAN taiKhoan = db.TAIKHOANs.SingleOrDefault(p => p.MaTK == maTK);

			string DiaChi = collection["DiaChi"];
			string phuong = collection["Phuong"];
			string quan = collection["Quan"];
			string thanhPho = collection["ThanhPho"];
			member.DiaChi = DiaChi + " phường " + phuong + " Quận " + quan + " thành phố " + thanhPho;
			member.Email = collection["Email"];
			var gioiTinh = ValueProvider.GetValue("GioiTinh");
			if (gioiTinh != null)
			{
				string gt = gioiTinh.AttemptedValue;
				if (gt == "Nam")
					member.GioiTinh = true;
				else member.GioiTinh = false;
			}

			string ngaySinh = collection["Ngay"];
			string thangSinh = collection["Thang"];
			string namSinh = collection["Nam"];
			member.NgaySinh = DateTime.Parse(thangSinh + "/" + ngaySinh + "/" + namSinh);
			member.Ten = collection["Ten"];

			taiKhoan.DiaChi = member.DiaChi;
			taiKhoan.Email = member.Email;
			taiKhoan.SDT = collection["SDT"];

			db.SaveChanges();
			return RedirectToAction("AccountInformation", "Home");
		}
		public ActionResult OrderInformation(string MaSP)
		{
			CHITIETDONHANG ctDonHang = new CHITIETDONHANG(MaSP);
			Response.Cookies["MaSP"]["MaSP"] = MaSP;
			return View(ctDonHang);
		}
		
		[HttpPost]
		public ActionResult OrderInformation(FormCollection collection)
		{
			string maTK = Request.Cookies["MaTK"]["MaTK"] ?? "";
			if (maTK == "" || !Request.IsAuthenticated)
				return RedirectToAction("Login", "Account");
			else
			{
				using (TransactionScope ts = new TransactionScope())
					{
				int nDonHangs = db.LaySoLuongDonHang();
				CHITIETDONHANG ctDOnHang = new CHITIETDONHANG();
				ctDOnHang.DonGia = Decimal.Parse(collection["Vouchers[0].Price"]);
				ctDOnHang.MaDH = nDonHangs.ToString();
				ctDOnHang.MaSP = Request.Cookies["MaSP"]["MaSP"];
				ctDOnHang.SoLuong = int.Parse(collection["Vouchers[0].SelectedQuantity"]);

				DONHANG donHang = new DONHANG();
				donHang.MaDH = ctDOnHang.MaDH;
				donHang.MaHTTT = "TT";
				donHang.MaTK = maTK;
				donHang.MaTT = "TT01";
				donHang.NgayDatHang = DateTime.Now;
				donHang.SDT = collection["DienThoai"];
				donHang.TenNguoiNhan = collection["HoTen"];
				donHang.TongTien = int.Parse(collection["Vouchers[0].SelectedQuantity"]) * Decimal.Parse(collection["Vouchers[0].Price"]);
				string diaChi = collection["DiaChi"];
				string thanhPho = collection["ThanhPho"];
				string quan = collection["Quan"];
				string phuong = collection["Phuong"];
				donHang.DiaChi = diaChi + " phường" + phuong + " quận" + quan + " thành phố Hồ Chí Minh";
					
				db.DONHANGs.Add(donHang);
				db.SaveChanges();

				db.CHITIETDONHANGs.Add(ctDOnHang);
				db.SaveChanges();
				ts.Complete();
					}
				return RedirectToAction("Index", "Home");
			}
		}

        public ActionResult OrderList()
        {
			string MaTK = Request.Cookies["MaTK"]["MaTK"] ?? " ";
			if (MaTK == " " || !Request.IsAuthenticated)
				return RedirectToAction("Login", "Account");
			else
			{
				List<DONHANG> lstDonHang = db.LayDSDonHangTheoMaTK(MaTK);

				return View(lstDonHang);
			}
            
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

		[HttpPost]
		public ActionResult ChangePassword(FormCollection collection)
		{
			string old = collection["OldPassword"];
			string newpass = collection["NewPassword"];
			string maTK = Request.Cookies["MaTK"]["MaTK"];
			TAIKHOAN taiKhoan = db.LayTaiKhoanTheoMaTK(maTK);
			if (old == taiKhoan.Password)
			{
				taiKhoan.Password = newpass;
				db.SaveChanges();
				
			}
			return RedirectToAction("Index", "Home");
		}

		
		[HttpPost]
		public ActionResult FeedbackSent(FormCollection collection)
		{
			string email = collection["Email"];
			try
			{
				WebMail.SmtpServer = "smtp.gmail.com";// "antispam2.emt.ee"; "gprsmail.emt.ee";
				WebMail.EnableSsl = true;
				WebMail.SmtpPort = 25;
				WebMail.UserName = "lanrung1706@gmail.com";
				WebMail.Password = "17061991";
				WebMail.From = "lanrung1706@gmail.com";
				WebMail.Send(
						email,
						subject: "GroupOnC2 Thông tin khuyến mãi",
						body: "Hehehe"
					//email
					);

				//return RedirectToAction("FeedbackSent");
			}
			catch (System.Exception ex)
			{
				ViewData.ModelState.AddModelError("_Form", ex.ToString());
			}
			return RedirectToAction("Index", "Home");
		}
	}
}