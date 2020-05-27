using Maladin.Areas.Admin.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maladin.Areas.Admin.Controllers
{
    public class DataSystemController : Controller
    {
        // GET: Admin/DataSystem
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Account(int?page, string q)
        {
            
            var dao = new AccountDAO();
            ViewBag.Data = dao.getAllAccountByPage((page ?? 1), q);
            return View();
        }
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult ProductOfAccount()
        {
            return View();
        }
        public ActionResult Voucher()
        {
            return View();
        }
        public ActionResult Notification()
        {
            return View();
        }
        public ActionResult Producer()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ActiveAccount(string user, string act)
        {
            bool at = true;
            if (act == "true")
            {
                at = true;
            }
            else
            {
                at = false;
            }
            var dao = new AccountDAO();
            var res = dao.Active(user, at);
            return Json(new { res = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteAccount(string user)
        {
            var dao = new AccountDAO();
            var res = dao.Delete(user);
            return Json(new { res = res }, JsonRequestBehavior.AllowGet);
        }
    }
}