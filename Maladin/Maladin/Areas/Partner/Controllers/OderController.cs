using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Areas.Partner.DAO;
using Maladin.Areas.Partner.Common;
using Maladin.Areas.Partner.Models;


namespace Maladin.Areas.Partner.Controllers
{
    public class OderController : BaseController
    {
        // GET: Partner/Oder
        public ActionResult Index(int? page, int? option)
        {
            if (page == null)
            {
                return View("Error");
            }
            var dao = new OderDAO();
            var user = Session[LoginPartnerSession.USER_SESSION].ToString();
            var models = dao.getListOder(user, (page??1),(option??5));
            ViewBag.MaxPage = dao.MaxPageOrder(user);
            ViewBag.Page = (page ?? 1);
            ViewBag.Option = (option ?? 5);
            return View(models);
        }
        public ActionResult ChitietHD()
        {
            return View();
        }
    }
}