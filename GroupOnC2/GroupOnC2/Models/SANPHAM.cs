using System;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web;
using System.Web.Mvc;
using GroupOnC2.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;
using Microsoft.SqlServer;

using System.ComponentModel.DataAnnotations;

namespace GroupOnC2.Models
{

	 [Bind(Exclude = "MaSP")]
    public  class SANPHAM
    {
		 GROUPONEntities1 db = new GROUPONEntities1();
		
        public SANPHAM()
        {
            this.CHITIETDONHANGs = new HashSet<CHITIETDONHANG>();
            this.CHITIETSANPHAMs = new HashSet<CHITIETSANPHAM>();
            this.COMMENTs = new HashSet<COMMENT>();
			
        }
		//[ScaffoldColumn(false)]
        public string MaSP { get; set; }
		//[ScaffoldColumn(false)]
        public string MaLoaiSP { get; set; }
        public string TenSP { get; set; }
		
		protected string _HinhAnh;
		public string HinhAnh
		 {
			 get
			 {
				 CHITIETSANPHAM ctSp = db.CHITIETSANPHAMs.Single(r => r.MaSP == MaSP);
				 return ctSp.Anh;
				 //string filePath = LayChiTietSanPham();
				 //using (var template = File.OpenRead(filePath))
				 //{
				 //    XMLHelper helper = new XMLHelper(filePath);
				 //   // string path = HttpContext.Current.Server.MapPath(helper.LayAnhBia().ElementAt(0));
				 //   string str =   helper.LayAnhBia().ElementAt(0);
					
				 //   // str.Replace(Convert.ToChar(92), Convert.ToChar(47));
				 //   return str;// "../../" + str;
				 //}
				 }
			 set { _HinhAnh = value; }
		 }

		private Nullable<decimal> _GiaBan;

		public Nullable<decimal> GiaBan
		{
			get {
				CHITIETSANPHAM ctSp = db.CHITIETSANPHAMs.Single(r => r.MaSP == MaSP);
				return ctSp.GiaBan;
			}
			set { _GiaBan = value; }
		}
		private Nullable<decimal> _GiaGoc;

		public Nullable<decimal> GiaGoc
		{
			get
			{
				CHITIETSANPHAM ctSp = db.CHITIETSANPHAMs.Single(r => r.MaSP == MaSP);
				return ctSp.GiaGoc;
			}
			set { _GiaGoc = value; }
		}
        public virtual ICollection<CHITIETDONHANG> CHITIETDONHANGs { get; set; }
        public ICollection<CHITIETSANPHAM> CHITIETSANPHAMs { get; set; }
        public virtual ICollection<COMMENT> COMMENTs { get; set; }
        public virtual LOAISANPHAM LOAISANPHAM { get; set; }


		public string LayChiTietSanPham()
		{
			CHITIETSANPHAM ctSp = db.CHITIETSANPHAMs.Single(r => r.MaSP == MaSP);
			MULTIMEDIA mul = db.MULTIMEDIAs.Single(item => item.MaCTSP == ctSp.MaCTSP);
			string filePath = mul.Multimedia1;
			filePath = HttpContext.Current.Server.MapPath(mul.Multimedia1);
			return filePath;
		}

		 public List<SANPHAM> LayDSHotDeal()
		{
			return db.SANPHAMs
			   .OrderByDescending(a => a.CHITIETDONHANGs.Count()).Take(5).ToList();
		}

    }
}
