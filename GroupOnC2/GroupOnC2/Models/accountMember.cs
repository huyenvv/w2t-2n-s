using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; 

namespace GroupOnC2.Models
{
    public class accountMember
    {
        public string maTK { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }

        public string email { get; set; }
        public string avatar { get; set; }
        public string maTT { get; set; }

        public string diachi { get; set; }
        public string sdt { get; set; }
        public string ten { get; set; }

        public DateTime ngaySinh { get; set; }
        public bool gioiTinh { get; set; }
    }
}