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
using System.ComponentModel.DataAnnotations;

namespace GroupOnC2.Models
{
	public class Cart1 
	{
		[Key]
		public int RecordId { get; set; }
		public string CartId { get; set; }
		public string MaSP { get; set; }
		public int Count { get; set; }
		public System.DateTime NgayTao { get; set; }

		public virtual SANPHAM SanPham { get; set; }
	}

	
}