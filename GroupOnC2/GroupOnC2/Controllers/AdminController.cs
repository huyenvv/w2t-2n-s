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
using GroupOnC2.ViewModels;

namespace GroupOnC2.Controllers
{
	public class AdminController : Controller
	{
		private GROUPONEntities1 db = new GROUPONEntities1();
		//
		// GET: /Admin/

		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult MemberManager(string userNam, string email)
		{
			var member =
				from tk in db.TAIKHOANs
				join m in db.THONGTINMEMBERs on tk.MaTK equals m.MaTK
				orderby tk.UserName
				select new Member { member = m, account = tk };
			if (!string.IsNullOrEmpty(userNam))
			{
				member = from x in member
						 where x.account.UserName.ToLower().Contains(userNam.ToLower())
						 orderby x.account.UserName
						 select x;
			}
			if (!string.IsNullOrEmpty(email))
			{
				member = from x in member
						 where x.account.Email.ToLower().Contains(email.ToLower())
						 orderby x.account.UserName
						 select x;
			}
			return View(member.ToList());
		}

		[HttpPost]
		public ActionResult MemberManager(FormCollection collection)
		{
			string userName = collection["UserName"];
			string email = collection["Email"];

			var member =
				from tk in db.TAIKHOANs
				where tk.UserName == userName && tk.Email == email
				join m in db.THONGTINMEMBERs on tk.MaTK equals m.MaTK
				orderby tk.UserName
				select new Member { member = m, account = tk };
			return View(member.ToList());
		}


		[HttpGet]
		public ActionResult NewAccount()
		{
			return View();
		}

		[HttpPost]
		public ActionResult NewAccount(FormCollection collection)
		{
			THONGTINMEMBER member = new THONGTINMEMBER();
			member.Avatar = null;
			member.UserName = collection["UserName"];
			member.Email = collection["Email"];
			var gioiTinh = ValueProvider.GetValue("Gender");
			if (gioiTinh != null)
			{
				string gt = gioiTinh.AttemptedValue;
				if (gt == "Nam")
					member.GioiTinh = true;
				else member.GioiTinh = false;
			}

			member.MaTK = "MB" + (db.LaySoLuongMember() + 1).ToString();
			member.MaTT = "TT01";
			string ngaySinh = collection["Date"];
			string thangSinh = collection["Month"];
			string namSinh = collection["Year"];
			member.NgaySinh = DateTime.Parse(thangSinh + "/" + ngaySinh + "/" + namSinh);
			member.Password = collection["Password"];

			member.SDT = collection["Phone"];
			member.Ten = collection["Name"];


			bool res = db.ThemMember(member);

			return RedirectToAction("MemberManager", "Admin");
		}

		public ActionResult DeleteMember(string id)
		{
			Member member = new Member();
			member.account = db.TAIKHOANs.Find(id);
			member.member = db.THONGTINMEMBERs.Find(id);
			db.Entry(member.member).State = EntityState.Deleted;
			db.Entry(member.account).State = EntityState.Deleted;
			db.SaveChanges();
			return RedirectToAction("MemberManager");
		}

		public ActionResult BusinessManager()
		{
			var business =
				from tk in db.TAIKHOANs
				join dn in db.THONGTINDOANHNGHIEPs on tk.MaTK equals dn.MaTK
				orderby tk.UserName
				select new Business { business = dn, account = tk };
			return View(business);
		}

		[HttpGet]
		public ActionResult NewBusiness()
		{
			return View();
		}

		[HttpPost]
		public ActionResult NewBusiness(accountBusiness business)
		{
			TAIKHOAN t = new TAIKHOAN(business.maTK, business.userName, business.passWord, business.email, business.avatar, "TT01", business.diachi, business.sdt);
			THONGTINDOANHNGHIEP dn = new THONGTINDOANHNGHIEP(business.maTK, business.website, business.tenDN);
			db.TAIKHOANs.Add(t);
			db.SaveChanges();
			db.THONGTINDOANHNGHIEPs.Add(dn);
			db.SaveChanges();
			return View();
		}

		public ActionResult DeleteBusiness(string id)
		{
			Business business = new Business();
			business.account = db.TAIKHOANs.Find(id);
			business.business = db.THONGTINDOANHNGHIEPs.Find(id);
			db.Entry(business.business).State = EntityState.Deleted;
			db.Entry(business.account).State = EntityState.Deleted;
			db.SaveChanges();
			return RedirectToAction("BusinessManager");
		}

		public ActionResult HotDealManager()
		{
			var hotDeal =
				from sp in db.SANPHAMs
				join ctsp in db.CHITIETSANPHAMs on sp.MaSP equals ctsp.MaSP
				join lsp in db.LOAISANPHAMs on sp.MaLoaiSP equals lsp.MaLoaiSP
				orderby sp.TenSP
				select new HotDeal { sp = sp, ctsp = ctsp, lsp = lsp };
			return View(hotDeal);
		}

		public ActionResult CommentManager()
		{
			List<COMMENT> lstCmds = db.LayDSComment();
			return View(lstCmds);
		}

        public ActionResult OrderManager()
        {
            List<DONHANG> dsDh = db.LayDSDonHang();
            return View(dsDh);
        }

        public ActionResult DeleteOrder(string id)
        {
            DONHANG donhang =  db.DONHANGs.Find(id);
            
            db.Entry(donhang.CHITIETDONHANGs).State = EntityState.Deleted;
            db.Entry(donhang).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("OrderManager");
        }

        public ActionResult UpdateOrder(string id)
        {
            List<string> tinhtrangDH = new List<string>();
            tinhtrangDH.Add("abc");
            tinhtrangDH.Add("ede");
            var dh = db.DONHANGs.Single(r => r.MaDH == id);
            return View(dh);
        }

	}


}
