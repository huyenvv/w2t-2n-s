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
namespace GroupOnC2.Models
{    
    public partial class LOAISANPHAM
    {
           
        public string MaLoaiSP { get; set; }
        public string TenLoaiSP { get; set; }
        public Nullable<int> SoLuongSP { get; set; }

		private List<SANPHAM> _SANPHAMs;

		public List<SANPHAM> SANPHAMs
		{
			get {
				GROUPONEntities1 db = new GROUPONEntities1();
				List<SANPHAM> sanPhams = db.SANPHAMs.Where(p => p.MaLoaiSP == MaLoaiSP).ToList();
				return sanPhams;
			}
			set { _SANPHAMs = value; }
		}
    }
}
