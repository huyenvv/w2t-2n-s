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
    public partial class DONHANG
    {
		GROUPONEntities1 db = new GROUPONEntities1();
       
        public string MaDH { get; set; }
        public string MaTK { get; set; }
		private string _UserName;

		public string UserName
		{
			get
			{
				var userName = from p in db.TAIKHOANs
								  where p.MaTK == MaTK
								  select p.UserName;
				return userName.ToString();
			}
			set { _UserName = value; }
		}
        public Nullable<System.DateTime> NgayDatHang { get; set; }
        public string MaHTTT { get; set; }
        public string MaTT { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string TenNguoiNhan { get; set; }
		public Decimal TongTien { get; set; }
		public GIOHANG GIOHANG { get; set; }
    
        public virtual ICollection<CHITIETDONHANG> CHITIETDONHANGs { get; set; }
        public virtual HINHTHUCTHANHTOAN HINHTHUCTHANHTOAN { get; set; }
        public virtual TAIKHOAN TAIKHOAN { get; set; }
        public virtual TINHTRANGDONHANG TINHTRANGDONHANG { get; set; }
    }
}
