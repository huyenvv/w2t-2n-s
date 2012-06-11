using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupOnC2.Models
{
    public class OrderInformation
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public bool PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}