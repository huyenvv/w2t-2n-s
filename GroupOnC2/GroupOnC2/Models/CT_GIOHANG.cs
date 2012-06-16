using System;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupOnC2.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;
using Microsoft.SqlServer;

namespace GroupOnC2.Models
{
	public class CT_GIOHANG
	{
		GROUPONEntities1 db = new GROUPONEntities1();
		public string MaSP { get; set; }
		public int SoLuong { get; set; }
		public Decimal DonGia { get; set; }

		public string TenSP {get;set;}

		private Decimal _ThanhTien;

		public Decimal ThanhTien
		{
			get { return DonGia*SoLuong; }
			set { _ThanhTien = value; }
		}

		public CT_GIOHANG(string MaSP)
		{
			this.MaSP = MaSP;
			SANPHAM sp = LaySanPham();
			this.TenSP = sp.TenSP;
			this.DonGia =	sp.GiaBan ?? 0;
		}

		public SANPHAM LaySanPham()
		{
			SANPHAM sanPham = db.SANPHAMs.SingleOrDefault(p=>p.MaSP == MaSP);
			return sanPham;
		}
	}
}