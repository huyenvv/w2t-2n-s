namespace GroupOnC2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class THONGTINMEMBER:TAIKHOAN
    {
        public string MaTK { get; set; }
        public string Ten { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public Nullable<bool> GioiTinh { get; set; }
    
        public virtual TAIKHOAN TAIKHOAN { get; set; }
    }
}
