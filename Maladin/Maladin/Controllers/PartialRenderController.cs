using Maladin.Areas.Customer.Common;
using Maladin.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maladin.Controllers
{
    public class PartialRenderController : Controller
    {
        // GET: PartialRender
        public ActionResult HeaderRender()
        {
            if (Session[CustomerSession.CUSTOMER_SESSION] == null)
            {
                ViewBag.IsLogin = null;
            }
            else
            {
                var da = new CustomerLoginDAO();

                ViewBag.IsLogin = da.getNameUser(Session[CustomerSession.CUSTOMER_SESSION].ToString(),
                   da.getTypeMax(Session[CustomerSession.CUSTOMER_SESSION].ToString()));
                ViewBag.TotalCart = da.getTotalCart(Session[CustomerSession.CUSTOMER_SESSION].ToString());
            }
            return PartialView("Header");
        }
        public ActionResult HeaderRenderUser()
        {
            if (Session[CustomerSession.CUSTOMER_SESSION] == null)
            {
                ViewBag.IsLogin = null;
            }
            else
            {
                var da = new CustomerLoginDAO();

                ViewBag.IsLogin = da.getNameUser(Session[CustomerSession.CUSTOMER_SESSION].ToString(),
                   da.getTypeMax(Session[CustomerSession.CUSTOMER_SESSION].ToString()));
                ViewBag.TotalCart = da.getTotalCart(Session[CustomerSession.CUSTOMER_SESSION].ToString());
            }
            return PartialView("LeftMenuUser");
        }
        public ActionResult LeftMenuUser()
        {
            var dao = new CustomerLoginDAO();
            var info = dao.getInformationByUser(Session[CustomerSession.CUSTOMER_SESSION].ToString());
            ViewBag.name = info.NAME_INFO;
            ViewBag.avt = info.AVT_ACC;
            return PartialView("LeftMenuUser");
        }
    }
}