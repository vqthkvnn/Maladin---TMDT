using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public JsonResult insertProduct()
        {
            return Json("true", JsonRequestBehavior.DenyGet);

        }
        [HttpPost]
        public JsonResult getOrigin()
        {
            return Json("", JsonRequestBehavior.DenyGet);
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
    }
}