using Maladin.Areas.Partner.Common;
using Maladin.Areas.Partner.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.EF;

namespace Maladin.Areas.Partner.Controllers
{
    public class PartialRenderController : Controller
    {
        // GET: Partner/PartialRender
        public ActionResult RenderHeaderPartner()
        {
            var dao = new AccountPartnerDAO();
            ViewBag.Name = Session[LoginPartnerSession.USER_SESSION].ToString();
            ViewBag.AVT = dao.getInfomationByAccount(ViewBag.Name);
            return PartialView("_layout_partner_header");
        }
    }
}