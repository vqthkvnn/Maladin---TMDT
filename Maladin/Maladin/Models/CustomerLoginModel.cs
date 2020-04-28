using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maladin.Models
{
    public class CustomerLoginModel
    {
        [Required(ErrorMessage ="Bạn chưa nhập tài khoản")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập nhập khẩu")]
        public string Password { get; set; }
    }
}