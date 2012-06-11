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

namespace GroupOnC2.Models
{
   
	 [Bind(Exclude = "MaSP")]
    public partial class CHITIETSANPHAM
    {
		 GROUPONEntities1 db = new GROUPONEntities1();		    
        public string MaCTSP { get; set; }
        public string MaSP { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public string GioiThieu { get; set; }
        public Nullable<decimal> GiaGoc { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
    
        public virtual ICollection<MULTIMEDIA> MULTIMEDIAs { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
		private string _Anh;

		public string Anh
		{
			get { return AnhBia.ElementAt(0); }
			set { _Anh = value; }
		}

		private List<string> _AnhBia;

		public List<string> AnhBia
		{
			get {

				string filePath = LayChiTietSanPham();
				using (var template = File.OpenRead(filePath))
				{
					XMLHelper helper = new XMLHelper(filePath);
					// string path = HttpContext.Current.Server.MapPath(helper.LayAnhBia().ElementAt(0));
					List<string> str = helper.LayAnhBia();
					
					return str;
				}
			}
			set { _AnhBia = value; }
		}

		private List<string> _AnhChiTiet;

		public List<string> AnhChiTiet
		{
			get
			{

				string filePath = LayChiTietSanPham();
				using (var template = File.OpenRead(filePath))
				{
					XMLHelper helper = new XMLHelper(filePath);
					// string path = HttpContext.Current.Server.MapPath(helper.LayAnhBia().ElementAt(0));
					List<string> str = helper.LayAnhChiTiet();
					return str;
				}
			}
			set { _AnhChiTiet = value; }
		}

		public string LayChiTietSanPham()
		{
			MULTIMEDIA mul = db.MULTIMEDIAs.SingleOrDefault(item => item.MaCTSP == MaCTSP);
			string filePath = mul.Multimedia1;
			filePath = HttpContext.Current.Server.MapPath(mul.Multimedia1);
			return filePath;
		}
    }
}
