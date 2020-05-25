using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Maladin.Areas.API.DAO;
namespace Maladin.Areas.API.Controllers
{
    public class ProductController : Controller
    {
        // GET: API/Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult getAllInfomationProduct()
        {
            return Json("hello json from post method", JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult insertProduct(string id, string name, string idProducer, string idTypeProduct, string des)
        {
            /*
             , string idOr,
                string price, string rating, string note, string idacc, string date, string admin = "ADMIN", bool isSell = false*/
            string insertQuery = "Insert PRODUCT(ID_PRODUCT,NAME_PRODUCT,ID_PRODUCER ,ID_TYPE_PRODUCT,DESCRIBE_PRODUCT," +
                "ID_ORIGIN ,PRICE_PRODUCT ,RATING_PRODUCT ,NOTE_PRODUCT,ID_INFO,DATE_PRODUCT,USER_ACC,IS_SELL) " +
                "VALUES ('" + id + "', N'" + name + "', '" + idProducer + "','" + idTypeProduct + "', N'" + des + "'";

            return Json(id + name, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult getOrigin(string id, string name)
        {
            string get = id + " chuoi " + name;
            return Json(get, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult deleteProduct(string id)
        {
            return Json("delete complate "+id,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult editProduct(string id)
        {
            return Json("", JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult getProductBy(string page, string q)
        {
            var data = new ProductDAO().getByNameAndPage(Convert.ToInt32(page), q);

            return Json(new {count = data.Count,data = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getTopSale()
        {
            var dao = new ProductDAO();
            var res = dao.getTopSale();
            return Json(new { data = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getTopNew()
        {
            var dao = new ProductDAO();
            var res = dao.getTopNew();
            return Json(new { data = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getTopSell()
        {
            var dao = new ProductDAO();
            var res = dao.getTopSale();
            return Json(new { data = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getAllForYour(string page)
        {
            var dao = new ProductDAO();
            var res = dao.getallForYour(Convert.ToInt32(page));
            return Json(new {count = res.Count(), data = res }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult getInformationshopFromID(string id)
        {
            var dao = new ProductDAO();
            var res = dao.getInformationShop(id);
            if (res == null)
            {
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = true, data = res }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult getAllOShop(string page, string idp)
        {
            var dao = new ProductDAO();
            var res = dao.getallOfShop(Convert.ToInt32(page), dao.GetIDShopOfIdp(idp));
            return Json(new { count = res.Count(), data = res }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult getAllAttrOfProduct(string idp)
        {
            var dao = new ProductDAO();
            return Json(new {count = dao.getAllAttrOfProduct(idp).Count(),  data = dao.getAllAttrOfProduct(idp) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getAllImageOfProduct(string idp)
        {
            var dao = new ProductDAO();
            return Json(new { count = dao.getAllImageOfProduct(idp).Count(), data = dao.getAllImageOfProduct(idp) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getInformationOfProduct(string idp)
        {
            var dao = new ProductDAO();
            return Json(new { data = dao.getInformationOfProduct(idp)}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getAllQuestionForProduct(string idp)
        {
            var dao = new ProductDAO();
            return Json(new {count = dao.allQuestionForProduct(idp).Count(), data = dao.allQuestionForProduct(idp) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public JsonResult AddToWatched(string user, string idp)
        {
            var dao = new ProductDAO();
            return Json(new { status = dao.AddToWatched(idp,user ) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public JsonResult AddToCart(string user, string idp)
        {
            var dao = new ProductDAO();
            var res = dao.AddToCart(user, idp);
            return Json(new { status = res, count=dao.CountCart(user) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public JsonResult AddToFavorite(string user, string id)
        {
            var dao = new ProductDAO();
            var res = dao.AddToFavorite(id, user);
            return Json(new { status = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CountCart(string user)
        {
            var dao = new ProductDAO();
            return Json(new { count = dao.CountCart(user) }, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete]
        public JsonResult DeleteFormCart(string user, string id)
        {
            var dao = new ProductDAO();
            return Json(new { status = dao.DeleteFromCart(user,id) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public JsonResult UpdateCart(string user, string id, string count)
        {
            var dao = new ProductDAO();
            return Json(new { status = dao.UpdateCart(user, id, Convert.ToInt32(count)) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPriceFromCart(string user, string idv)
        {
            var dao = new ProductDAO();
            return Json(new { sum = dao.GetPriceFromCart(user) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getInformationShopFromIDP(string idp)
        {
            var dao = new ProductDAO();
            var res = dao.getInformationShop(dao.GetIDShopOfIdp(idp));
            if (res == null)
            {
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = true, data = res }, JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpPost]
        public JsonResult getProductOfShop(string id, string page)
        {

            var dao = new ProductDAO();
            return Json(new {data = dao.getProductOfShop(id, Convert.ToInt32(page)) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getTopSaleMax(string page)
        {
            var dao = new ProductDAO();
            var res = dao.getTopSale();
            return Json(new { data = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getTopNewMax(string page)
        {
            var dao = new ProductDAO();
            var res = dao.getTopNew();
            return Json(new { data = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getTopSellMax(string page)
        {
            var dao = new ProductDAO();
            var res = dao.getTopSale();
            return Json(new { data = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getAllComment(string idp)
        {
            var dao = new ProductDAO();
            var res = dao.getAllComment(idp);
            return Json(new { count = res.Count(), data = res }, JsonRequestBehavior.AllowGet);
        }
    }
}