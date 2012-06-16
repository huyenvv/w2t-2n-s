using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroupOnC2.Models;
using PagedList.Mvc;
using PagedList;
using System.Data.Entity.Infrastructure;
using System.Transactions;

namespace GroupOnC2.Models
{
	public partial class GROUPONEntities1 : DbContext
	{
		public GROUPONEntities1()
			: base("name=GROUPONEntities1")
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			throw new UnintentionalCodeFirstException();
		}

		public DbSet<CHITIETDONHANG> CHITIETDONHANGs { get; set; }
		public DbSet<CHITIETSANPHAM> CHITIETSANPHAMs { get; set; }
		public DbSet<COMMENT> COMMENTs { get; set; }
		public DbSet<DONHANG> DONHANGs { get; set; }
		public DbSet<HINHTHUCTHANHTOAN> HINHTHUCTHANHTOANs { get; set; }
		public DbSet<LOAISANPHAM> LOAISANPHAMs { get; set; }
		public DbSet<MULTIMEDIA> MULTIMEDIAs { get; set; }
		public DbSet<PAYPAL> PAYPALs { get; set; }
		public DbSet<SANPHAM> SANPHAMs { get; set; }
		public DbSet<sysdiagram> sysdiagrams { get; set; }
		public DbSet<TAIKHOAN> TAIKHOANs { get; set; }
		public DbSet<THANHTOANTRUCTIEP> THANHTOANTRUCTIEPs { get; set; }
		public DbSet<THONGTINADMIN> THONGTINADMINs { get; set; }
		public DbSet<THONGTINDOANHNGHIEP> THONGTINDOANHNGHIEPs { get; set; }
		public DbSet<THONGTINMEMBER> THONGTINMEMBERs { get; set; }
		public DbSet<TINHTRANGDONHANG> TINHTRANGDONHANGs { get; set; }
		public DbSet<TRANGTHAITAIKHOAN> TRANGTHAITAIKHOANs { get; set; }
		
		private List<Cart1> CARTs = new List<Cart1>();
		public List<Cart1> CARTs1
		{
			get { return CARTs; }
			set { CARTs = value; }
		}

		private List<GIOHANG> _GIOHANGs = new List<GIOHANG>();

		public List<GIOHANG> GIOHANGs
		{
			get { return _GIOHANGs; }
			set { _GIOHANGs = value; }
		}
		

		public bool ThemMember(THONGTINMEMBER member)
		{
			//Kiem tra tai khoan nay da ton tai chua
			var taiKhoan = TAIKHOANs.SingleOrDefault(g => g.UserName == member.UserName);

			if (taiKhoan != null) return false;
			else
			{
				try
				{
					using (TransactionScope ts = new TransactionScope())
					{
						#region ThemTaiKhoanMoi
						TAIKHOAN newTaiKhoan = new TAIKHOAN();

						newTaiKhoan.MaTK = member.MaTK;
						newTaiKhoan.Avatar = null;
						newTaiKhoan.DiaChi = member.DiaChi;
						newTaiKhoan.Email = member.Email;
						newTaiKhoan.MaTT = member.MaTT;
						newTaiKhoan.Password = member.Password;
						newTaiKhoan.SDT = member.SDT;
						newTaiKhoan.UserName = member.UserName;

						TAIKHOANs.Add(newTaiKhoan);
						this.SaveChanges();
						#endregion

						#region ThemMember
						THONGTINMEMBERs.Add(member);
						this.SaveChanges();
						#endregion
						ts.Complete();
					}
					return true;
				}
				catch (DbEntityValidationException e)
				{
					foreach (var eve in e.EntityValidationErrors)
					{
						Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
							eve.Entry.Entity.GetType().Name, eve.Entry.State);
						foreach (var ve in eve.ValidationErrors)
						{
							Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
								ve.PropertyName, ve.ErrorMessage);
						}
					}
					//throw;
				}
				return false;

			}
		}
		public int LaySoLuongMember()
		{
			return THONGTINMEMBERs.ToList().Count;
		}

		public bool KiemTraThongTinDangNhap(string userName, string password)
		{
			TAIKHOAN tk = TAIKHOANs.SingleOrDefault(p => p.Password == password && p.UserName == userName);
			if (tk == null) return false;
			else return true;
		}

		public int LaySoLuongDonHang ()
		{
			return DONHANGs.ToList().Count;
		}

		public int LaySoLuongGioHang()
		{
			return GIOHANGs.Count;
		}

		public void ThemGioHang(GIOHANG gioHang)
		{
			this.GIOHANGs.Add(gioHang);
			this.SaveChanges();
		}

		public int LaySoLuongComment()
		{
			return COMMENTs.ToList().Count;
		}

		public List<DONHANG> LayDSDonHangTheoMaTK(string MaTK)
		{
			var dsDonHang =from dh in DONHANGs
							where dh.MaTK == MaTK
							select dh;
			return dsDonHang.ToList();
		}

		//public List<DONHANG> LayDSCTDonHangTheoMaTK(string MaTK)
		//{

		//    var dsDonHang = from dh in CHITIETDONHANGs
		//                    where dh.MaTK == MaTK
		//                    select dh;
		//    return dsDonHang.ToList();
		//}

		public List<COMMENT> LayDSCommentTheoMaSP(string MaSP)
		{
			
			var dsComment = from cm in COMMENTs
									   where cm.MaSP == MaSP
									   select cm;
			return dsComment.ToList();
		}

		public string LayUserNameTheoMaTK(string MaTK)
		{
			TAIKHOAN taiKhoan = TAIKHOANs.SingleOrDefault(p => p.MaTK == MaTK);
			return taiKhoan.UserName;
		}

		public TAIKHOAN LayTaiKhoanTheoMaTK(string MaTK)
		{
			TAIKHOAN taiKhoan = TAIKHOANs.SingleOrDefault(p => p.MaTK == MaTK);
			return taiKhoan;
		}
	}
}
