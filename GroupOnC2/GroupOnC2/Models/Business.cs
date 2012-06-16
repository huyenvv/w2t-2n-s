using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupOnC2.Models
{
    public class Business
    {
        public THONGTINDOANHNGHIEP business { get; set; }
        public TAIKHOAN account { get; set; }
        public TRANGTHAITAIKHOAN trangThai { get; set; }
    }
}