using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Models;

namespace Maladin.Areas.Customer.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Customer/Account
        public ActionResult Index()
        {
            return View();
        }
        
        
        
        public ActionResult Infomation()
        {
            return View();
        }
    }
}