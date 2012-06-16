using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupOnC2.Models
{
    public class HotDeal
    {
        public CHITIETSANPHAM ctsp { get; set; }
        public LOAISANPHAM lsp { get; set; }
        public SANPHAM sp { get; set; }
    }
}