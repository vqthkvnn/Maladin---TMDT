using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maladin.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult getInfomationProduct()
        {
            return Json("hello json from post method", JsonRequestBehavior.DenyGet);
        }

        
    }
}