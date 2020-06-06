using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.DAO;
using Maladin.Common;
using Maladin.Models;

namespace Maladin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return View("Error");
            }
            ViewBag.IsLogin = Session[CustomerLoginSession.CUSTOMER_SESSION];


            var dao = new ProductHomeDAO();
            ViewBag.Product = dao.GetInformationProduct(id);
            ViewBag.CountImage = dao.countTopImage(id);
            ViewBag.ListImage = dao.getTopImage(id);
            ViewBag.CountQuest = dao.countQuestion(id);
            ViewBag.ListQuest = dao.getAllQuest(id);
            ViewBag.ListComment = dao.getAllComment(id);
            ViewBag.StarList = dao.CountStarAVG(id);
            ViewBag.ListAttr = dao.getAllAttr(id);
            ViewBag.ListWith = dao.getProductWithOf(id);
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
        public ActionResult PayCOD(string id)
        {
            return View();
        }
        [HttpPost]
        public JsonResult Comment(int? rating, string title, string content, string idp)
        {
            if (Session[CustomerLoginSession.CUSTOMER_SESSION] == null)
            {
                return Json(new { status = 0, content = "Bạn chưa đăng nhập" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var dao = new ProductHomeDAO();
                var status = dao.InsertComment(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString(), title, content, (rating ?? 5), idp);
                if (status == 1)
                {
                    return Json(new { status = 1, content = "Bình luận thành công!" }, JsonRequestBehavior.AllowGet);
                }
                else if (status ==0)
                {
                    return Json(new { status = -1, content = "Sản phẩm này đã bình luận" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = -2, content = "Lỗi hệ thống!" }, JsonRequestBehavior.AllowGet);
                }
                
            }
            
        }
        [HttpPost]
        public JsonResult QuestionAjax(string idp, string content, string title)
        {
            var dao = new ProductHomeDAO();
            var res = dao.InsertQuest(idp, content, title);
            return Json(new { res = res }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FollowOrder(string ido)
        {
            ViewBag.ID = ido;
            var dao = new CustomerLoginDAO();
            var res = dao.GetOrderDetailModel(ido);

            return View(res);
        }
        public ActionResult Shop(string ids)
        {
            return View();
        }
    }
}