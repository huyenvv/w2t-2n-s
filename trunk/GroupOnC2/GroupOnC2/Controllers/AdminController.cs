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

        public ActionResult MemberManager(int? page)
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
        public ViewResult NewAccount(Member member)
        {
            TRANGTHAITAIKHOAN t = new TRANGTHAITAIKHOAN();
            t.MaTT = "chết";
            t.TenTT = "chết rồi";
            db.TRANGTHAITAIKHOANs.Add(t);
            db.SaveChanges();
            return View();
        }

        public ActionResult Delete(int id)
        {
            Member member = new Member();
            member.account = db.TAIKHOANs.Find(id);
            member.member = db.THONGTINMEMBERs.Find(id);
            return View(member);
        }

        [HttpPost]
        public ActionResult Delete(Member member)
        {
            db.Entry(member.account).State = EntityState.Deleted;
            db.Entry(member.member).State = EntityState.Deleted;
            return RedirectToAction("MemberManager");
        }
    }
}
