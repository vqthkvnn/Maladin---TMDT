using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maladin.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Admin/Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Notification()
        {
            return View();
        }
        public ActionResult ActiveCTV()
        {
            return View();
        }
        public ActionResult CreatAcc()
        {
            return View();
        }
        public ActionResult EditAcc()
        {
            return View();
        }
    }
}