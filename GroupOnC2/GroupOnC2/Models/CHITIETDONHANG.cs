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
  public partial class CHITIETDONHANG
    {
		GROUPONEntities1 db = new GROUPONEntities1();
        public string MaDH { get; set; }
        public string MaSP { get; set; }
        public Nullable<int> SoLuong { get; set; }
		public Nullable<decimal> DonGia { get; set; }

		private string _TenSP;

		public string TenSP
		{
			get { return LaySanPham(MaSP).TenSP; }
			set { _TenSP = value; }
		}

		private Decimal _ThanhTien;

		public Decimal ThanhTien
		{
			get { return SoLuong * DonGia ?? 0; }
			set { _ThanhTien = value; }
		}

		public CT_GIOHANG CT_GIOHANG { get; set; }
        public virtual DONHANG DONHANG { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }

		public CHITIETDONHANG(string MaSP)
		{
			this.MaSP = MaSP;
			SANPHAM sanPham = LaySanPham(MaSP);
			DonGia = sanPham.GiaBan;
			TenSP = sanPham.TenSP;
		}

		public CHITIETDONHANG()
		{
		}
		public CHITIETDONHANG(CT_GIOHANG ctGioHang)
		{
			this.CT_GIOHANG = ctGioHang;
			this.TenSP = ctGioHang.TenSP;
			this.DonGia = ctGioHang.DonGia;
			this.MaSP = ctGioHang.MaSP;
			this.SoLuong = ctGioHang.SoLuong;
			this.ThanhTien = this.DonGia * this.SoLuong ?? 0;
		}
		public SANPHAM LaySanPham(string MaSP)
		{
			return db.SANPHAMs.SingleOrDefault(p => p.MaSP == MaSP);
		}
    }
}
