using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.DAO;
using Maladin.Common;
namespace Maladin.Areas.Customer.Controllers
{
    public class PaymentController : BaseController
    {
        // GET: Customer/Payment
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult addToCart(string idp, string count)
        {
            string user = Session[CustomerLoginSession.CUSTOMER_SESSION].ToString();
            count = (count ?? "1");
            var dao = new PaymentDAO();
            try
            {
                if (dao.IsProduct(idp))
                {
                    var kq = dao.AddToCart(user, idp, Convert.ToInt32(count));
                    if (kq)
                    {
                        return Json(new { status = 1 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { status = -2 }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { status = -1 }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult RemoveCart(string idp)
        {
            string user = Session[CustomerLoginSession.CUSTOMER_SESSION].ToString();
            var dao = new PaymentDAO();
            try
            {
                if (dao.IsProduct(idp))
                {
                    var kq = dao.RemoveCart(user, idp);
                    if (kq)
                    {
                        return Json(new { status = 1 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { status = -2 }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { status = -1 }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Confirm()
        {
            return View();
        }
    }
}