using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupOnC2.Models
{
	public class ThongKe
	{

		public int SoDonDatHang { get; set; }

		public Decimal TongTien { get; set; }

		public List<DONHANG> DONHANGs { get; set; }

		public ThongKe(List<DONHANG> lstDonhang)
		{
			this.DONHANGs = lstDonhang;
			this.SoDonDatHang = lstDonhang.Count;
			Decimal tien  = 0;
			foreach(DONHANG dh in lstDonhang)
			{
				tien += dh.CHITIETDONHANGs.ElementAt(0).ThanhTien;
			}

			this.TongTien = tien;
		}
	}
}