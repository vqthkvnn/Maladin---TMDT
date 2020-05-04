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

            return Json(id + name, JsonRequestBehavior.DenyGet);

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
            return Json("delete complate "+id,JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult editProduct(string id)
        {
            return Json("", JsonRequestBehavior.DenyGet);
        }
        public JsonResult getProductBy(string page, string q)
        {
            var data = new ProductDAO().getByNameAndPage(page, q);

            return Json(new {count = data.Count,data = data }, JsonRequestBehavior.AllowGet);
        }
    }
}