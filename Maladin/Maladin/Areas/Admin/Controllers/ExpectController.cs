using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maladin.Areas.Admin.Controllers
{
    public class ExpectController : Controller
    {
        // GET: Admin/Expect
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExpectProduct()
        {
            return View();
        }
        public ActionResult ExpectAccount()
        {
            return View();
        }
    }
}