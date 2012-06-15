namespace GroupOnC2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HINHTHUCTHANHTOAN
    {
        public HINHTHUCTHANHTOAN()
        {
            this.DONHANGs = new HashSet<DONHANG>();
        }
    
        public string MaHTTT { get; set; }
        public string TenHTTT { get; set; }
    
        public virtual ICollection<DONHANG> DONHANGs { get; set; }
        public virtual PAYPAL PAYPAL { get; set; }
        public virtual THANHTOANTRUCTIEP THANHTOANTRUCTIEP { get; set; }
    }
}
