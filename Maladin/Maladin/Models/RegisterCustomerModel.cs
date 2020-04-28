using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maladin.Models
{
    public class RegisterCustomerModel
    {
        
        public string UserName { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Mật khẩu phải trùng khớp")]
        public string RePassword { get; set; }
        public string Email { get; set; }
    }
}