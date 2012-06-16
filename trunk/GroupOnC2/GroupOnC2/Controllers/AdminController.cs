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

        public ActionResult MemberManager(string userName, string Email)
        {
            var member =
                from tk in db.TAIKHOANs
                join m in db.THONGTINMEMBERs on tk.MaTK equals m.MaTK 
                orderby tk.UserName
                select new Member { member = m, account = tk };
            return View(member);
        }

        [HttpGet]
        public ActionResult NewAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewAccount(accountMember member)
        {
            TAIKHOAN t = new TAIKHOAN(member.maTK, member.userName, member.passWord, member.email, member.avatar,"TT01", member.diachi, member.sdt);
            THONGTINMEMBER m = new THONGTINMEMBER(member.maTK, member.ten, member.gioiTinh, member.ngaySinh);
            db.TAIKHOANs.Add(t);
            db.SaveChanges();
            db.THONGTINMEMBERs.Add(m);
            db.SaveChanges();
            return View();
        }

        public ActionResult Delete(string id)
        {
            Member member = new Member();
            member.account = db.TAIKHOANs.Find(id);
            member.member = db.THONGTINMEMBERs.Find(id);
            db.Entry(member.member).State = EntityState.Deleted;
            db.Entry(member.account).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("MemberManager");
        }
    }
}
