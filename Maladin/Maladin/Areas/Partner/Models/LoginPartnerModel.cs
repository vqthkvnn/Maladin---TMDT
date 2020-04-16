using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.Partner.Models
{
    public class LoginPartnerModel
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public bool Remember { get; set; }
    }
}