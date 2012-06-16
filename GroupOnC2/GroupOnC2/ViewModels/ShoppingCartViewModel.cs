using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupOnC2.Models;

namespace GroupOnC2.ViewModels
{
	public class ShoppingCartViewModel
	{
		public List<Cart1> CartItems { get; set; }
		public decimal CartTotal { get; set; }
	}
}