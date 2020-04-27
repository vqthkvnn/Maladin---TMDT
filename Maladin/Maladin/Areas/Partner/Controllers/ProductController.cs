using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Areas.Partner.DAO;
using Maladin.Areas.Partner.Models;
using Maladin.Areas.Partner.Common;
using System.Text;
using Maladin.EF;

namespace Maladin.Areas.Partner.Controllers
{
    public class ProductController : BaseController
    {


        // GET: Partner/Product
        
        public ActionResult Index(string page, string pageSize, string q)
        {
            int p =1, pS = 8;
            if (page != null || pageSize != null)
            {
                p = Convert.ToInt32(page);
                pS = Convert.ToInt32(pageSize);
            }

            var dao = new ProductDao();
            var model = new ProductModels();
            if (q == null)
            {
                model.maxPage = (int)dao.GetCount() / pS + 1;
                if (model.maxPage < p || p < 1)
                {
                    model.PRODUCT = dao.GetListProductByPage(pS, 1);
                }
                else
                {
                    model.PRODUCT = dao.GetListProductByPage(pS, p);
                }

                model.page = p;
                model.pageSize = pS;

                return View(model);
            }
            else
            {
                model.maxPage = (int)dao.GetCount(q) / pS + 1;
                if (model.maxPage < p || p < 1)
                {
                    model.PRODUCT = dao.GetListProductByPage(pS, 1,q);
                }
                else
                {
                    model.PRODUCT = dao.GetListProductByPage(pS, p,q);
                }

                model.page = p;
                model.pageSize = pS;

                return View(model);
            }
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
        public ActionResult Edit(string id)
        {
            return View();
        }
        public ActionResult Add(InfomationProductModel model)
        {
            var dao = new InformationProductDAO();
            model.TYPE_PRODUCT = dao.GetAllTypePRoduct();
            model.PRODUCER_INFO = dao.GetAllProducer();
            model.ORIGIN = dao.GetAllOrigin();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddProduct(string nameProduct, string price, string nameProducer, string nameType, string nameOrigin, string desPro, 
            string note)
        {
            try
            {
                string timeNow = DateTime.Now.ToString("yyyy-MM-dd");
                var dao = new WaitProductDAO();
                WAIT_PRODUCT wAIT_PRODUCT = new WAIT_PRODUCT();
                wAIT_PRODUCT.NAME_PRODUCT = nameProduct;
                wAIT_PRODUCT.ID_PRODUCER = nameProducer;
                wAIT_PRODUCT.ID_ORIGIN = nameOrigin;
                wAIT_PRODUCT.ID_TYPE_PRODUCT = nameType;
                wAIT_PRODUCT.PRICE_PRODUCT = Int32.Parse(price);
                wAIT_PRODUCT.DESCRIBE_PRODUCT = desPro;
                wAIT_PRODUCT.NOTE_PRODUCT = note;
                wAIT_PRODUCT.DATE_PRODUCT = Convert.ToDateTime(timeNow);
                wAIT_PRODUCT.ID_INFO = Session[LoginPartnerSession.USER_SESSION].ToString();
                if (dao.InsertByCode(wAIT_PRODUCT))
                {
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                return Json("false", JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Search(string key, string option, int pageSize, int page)
        {
            ProductModels models = new ProductModels();
            return View("Index", models);
        }
    }
}