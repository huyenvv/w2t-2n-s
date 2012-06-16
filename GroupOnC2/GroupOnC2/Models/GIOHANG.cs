using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupOnC2.Models;


namespace GroupOnC2.Models
{
	public class GIOHANG
	{
		GROUPONEntities1 db = new GROUPONEntities1();
		[HiddenInput]
		public string MaTK { get; set; }

		public string MaGH { get; set; }
		public string MaDH { get; set; }
		public List<CT_GIOHANG> lstCTGioHang { get; set; }

		
		public Decimal _ThanhTien = 0;

		public Decimal ThanhTien
		{
			get 
			{	
				Decimal thanhTien = 0;
				foreach (CT_GIOHANG ctGioHang in lstCTGioHang)
					thanhTien += ctGioHang.ThanhTien;
				return thanhTien;
			}

			set { _ThanhTien = value; }
		}
		public GIOHANG()
		{
			int nGioHangs = db.LaySoLuongGioHang();
			MaGH = nGioHangs.ToString();
			lstCTGioHang = new List<CT_GIOHANG>();
			ThanhTien = 0;

		}
		public GIOHANG(List<CT_GIOHANG> lstCTGioHang)
		{
			this.lstCTGioHang = lstCTGioHang;
			int nGioHangs = db.LaySoLuongGioHang();
			MaGH = nGioHangs.ToString();
		}
		public void Them_CTGioHang(CT_GIOHANG ctGioHang)
		{
			this.lstCTGioHang.Add(ctGioHang);
			this.ThanhTien += ctGioHang.ThanhTien;
			db.SaveChanges();
		}

	}
}