using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.DAO;
using Maladin.Common;

namespace Maladin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(string id)
        {
            ViewBag.IsLogin = Session[CustomerLoginSession.CUSTOMER_SESSION];


            var dao = new ProductHomeDAO();
            ViewBag.Product = dao.GetInformationProduct(id);
            ViewBag.CountImage = dao.countTopImage(id);
            ViewBag.ListImage = dao.getTopImage(id);
            ViewBag.CountQuest = dao.countQuestion(id);
            ViewBag.ListQuest = dao.getAllQuest(id);
            ViewBag.ListComment = dao.getAllComment(id);
            
            ViewBag.ListAttr = dao.getAllAttr(id);
            return View();
        }
        [HttpPost]
        public JsonResult AddToCart(string id)
        {
            var dao = new ProductHomeDAO();
            var res = dao.AddToCart(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString(), id);
            return Json(new {status = res}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddToFarotive(string id)
        {
            var dao = new ProductHomeDAO();
            var res = dao.AddToFavorite(id,Session[CustomerLoginSession.CUSTOMER_SESSION].ToString());
            if (res)
            {
                return Json(new { status = 1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}