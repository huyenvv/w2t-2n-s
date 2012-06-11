using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace GroupOnC2.Models
{
    public partial class TAIKHOAN
    {
        public TAIKHOAN()
        {
            this.COMMENTs = new HashSet<COMMENT>();
            this.DONHANGs = new HashSet<DONHANG>();
        }
    
        public string MaTK { get; set; }
		
		[Required]
		public string UserName { get; set; }

		[Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
		public string Email { get; set; }
        public string Avatar { get; set; }
        public string MaTT { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
    
        public virtual ICollection<COMMENT> COMMENTs { get; set; }
        public virtual ICollection<DONHANG> DONHANGs { get; set; }
        public virtual THONGTINADMIN THONGTINADMIN { get; set; }
        public virtual THONGTINDOANHNGHIEP THONGTINDOANHNGHIEP { get; set; }
        public virtual THONGTINMEMBER THONGTINMEMBER { get; set; }
        public virtual TRANGTHAITAIKHOAN TRANGTHAITAIKHOAN { get; set; }
    }


}
