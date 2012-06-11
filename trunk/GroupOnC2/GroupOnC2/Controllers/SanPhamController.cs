using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupOnC2.Models;

namespace GroupOnC2.Controllers
{ 
    public class SanPhamController : Controller
    {
        private GROUPONEntities1 db = new GROUPONEntities1();

        //
        // GET: /SanPham/

        public ViewResult Index()
        {
            var chitietsanphams = db.CHITIETSANPHAMs.Include(c => c.SANPHAM);
            return View(chitietsanphams.ToList());
        }

        //
        // GET: /SanPham/Details/5

        public ViewResult Details(string id)
        {
            /*CHITIETSANPHAM chitietsanpham = db.CHITIETSANPHAMs.Find(id);
            return View(chitietsanpham);*/
			var ctSp = from ct in db.CHITIETSANPHAMs
					   where ct.MaSP == id
					   select ct;
			return View(ctSp);
        }

        //
        // GET: /SanPham/Create

        public ActionResult Create()
        {
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "MaLoaiSP");
            return View();
        } 

        //
        // POST: /SanPham/Create

        [HttpPost]
        public ActionResult Create(CHITIETSANPHAM chitietsanpham)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETSANPHAMs.Add(chitietsanpham);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "MaLoaiSP", chitietsanpham.MaSP);
            return View(chitietsanpham);
        }
        
        //
        // GET: /SanPham/Edit/5
 
        public ActionResult Edit(string id)
        {
            CHITIETSANPHAM chitietsanpham = db.CHITIETSANPHAMs.Find(id);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "MaLoaiSP", chitietsanpham.MaSP);
            return View(chitietsanpham);
        }

        //
        // POST: /SanPham/Edit/5

        [HttpPost]
        public ActionResult Edit(CHITIETSANPHAM chitietsanpham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chitietsanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "MaLoaiSP", chitietsanpham.MaSP);
            return View(chitietsanpham);
        }

        //
        // GET: /SanPham/Delete/5
 
        public ActionResult Delete(string id)
        {
            CHITIETSANPHAM chitietsanpham = db.CHITIETSANPHAMs.Find(id);
            return View(chitietsanpham);
        }

        //
        // POST: /SanPham/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {            
            CHITIETSANPHAM chitietsanpham = db.CHITIETSANPHAMs.Find(id);
            db.CHITIETSANPHAMs.Remove(chitietsanpham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}