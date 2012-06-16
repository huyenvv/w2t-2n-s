using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupOnC2.Models
{
    public class Member
    {
        public THONGTINMEMBER member { get; set; }
        public TAIKHOAN account { get; set; }
        public TRANGTHAITAIKHOAN trangThai { get; set; }
    }
}