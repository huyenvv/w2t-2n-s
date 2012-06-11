using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GroupOnC2.Models
{
	public class LOGINFORM
	{
		[Required, DisplayName("Email")]
		public string Email { get; set; }

		[Required, DisplayName("Password")]
		public string Password { get; set; }

		[DisplayName("Remember Me")]
		public bool RememberMe { get; set; }
	}
}
