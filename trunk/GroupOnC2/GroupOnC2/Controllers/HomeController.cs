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

		public ViewResult Details(string id)
		{
			var ctSp = db.CHITIETSANPHAMs.Single(r => r.MaSP == id);
			return View(ctSp);

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
        public ActionResult OrderInformation()
        {
            OrderInformation order = new OrderInformation();
            return View();
        }


        public ActionResult AccountInformation()
        {
            return View();
        }

        public ActionResult OrderList()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}