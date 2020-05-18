using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Models;
using Maladin.DAO;
using Maladin.Areas.Customer.Common;
namespace Maladin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(ItemProductModels models)
        {
            var dao = new ProductHomeDAO();
            models.GiayProduct = dao.getListProduct(1, "TP001");
            models.DongHoProduct = dao.getListProduct(1, "TP002");
            models.KinhProduct = dao.getListProduct(1, "TP003");
            if (Session[CustomerSession.CUSTOMER_SESSION] == null)
            {
                ViewBag.IsLogin = null;
            }
            else
            {
                var da =  new CustomerLoginDAO();

                ViewBag.IsLogin = da.getNameUser(Session[CustomerSession.CUSTOMER_SESSION].ToString(),
                   "CT");
            }
            
            return View(models);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult ShoppingCart()
        {
            return View();
        }

        public ActionResult ProductDetail()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
        public JsonResult GetProductList(int page, string type)
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string type, string page, string sortBy)
        {
            return View();
        }
    }
}