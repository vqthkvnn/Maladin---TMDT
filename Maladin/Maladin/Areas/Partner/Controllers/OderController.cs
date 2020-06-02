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
        public ActionResult Index(OderModels models)
        {
            var dao = new OderDAO();
            var user = Session[LoginPartnerSession.USER_SESSION].ToString();
            models.listTypeOder = dao.getAllTypeOder();
            models.oders = dao.getAllOder(user);
            return View(models);
        }
        public ActionResult ChitietHD()
        {
            return View();
        }
    }
}