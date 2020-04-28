using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Models;

namespace Maladin.Areas.Customer.Controllers
{
    public class AccountController : Controller
    {
        // GET: Customer/Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(CustomerLoginModel model)
        {
            if (model.UserName == null || model.Password == null)
            {
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    return View("Index");
                }
                else
                {
                    return View("Index");
                }
            }
        }
        public ActionResult Register()
        {
            return View();
        }
    }
}