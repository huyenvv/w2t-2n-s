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
    
    public class DoanhNghiepController : Controller
    {
        private GROUPONEntities1 db = new GROUPONEntities1();
        //
        // GET: /DoanhNghiep/

        public ActionResult DoanhNghiep()
        {
            return View();
        }
        // GET: /DoanhNghiep/ThongTinDoanhNghiep

        public ActionResult ThongTinDoanhNghiep()
        {
            return View();
        }
        // GET: /DoanhNgiep/SanPham

        public ActionResult SanPhamDoanhNgiep()
        {
            return View();
        }
        // GET: /DoanhNgiep/KhuyenMaiMoi

        public ActionResult SanPhamNews()
        {
            return View();
        }

        public ViewResult Browse(string MaLoaiSP, int? page)
        {
            var loai = db.LOAISANPHAMs.Single(g => g.MaLoaiSP == MaLoaiSP);
            var sanPhams = loai.SANPHAMs;
            int pageSize = 12;
            int n = sanPhams.Count;
            int pageNumber = (page ?? 1);
            return View(sanPhams.ToPagedList(pageNumber, pageSize));

        }

        // GET: /Home/Details/5

        public ViewResult Details(string id)
        {
            var ctSp = db.CHITIETSANPHAMs.Single(r => r.MaSP == id);
            return View(ctSp);

        }
    }
}
