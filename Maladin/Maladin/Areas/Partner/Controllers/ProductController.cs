using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Areas.Partner.DAO;
using Maladin.Areas.Partner.Models;
using Maladin.Areas.Partner.Common;
namespace Maladin.Areas.Partner.Controllers
{
    public class ProductController : BaseController
    {
        

        // GET: Partner/Product
        public ActionResult Index()
        {
            var dao = new ProductDao();
            var model = new ProductModels();
            model.PRODUCT = dao.getAllProduct();
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var dao = new ProductDao();
            if (dao.Delete(id))
            {
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}