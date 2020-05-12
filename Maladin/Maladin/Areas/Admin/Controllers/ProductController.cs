using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Areas.Admin.Models;
using Maladin.Areas.Admin.DAO;
using Maladin.Areas.Admin.Common;

namespace Maladin.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Approved(WaitProductModels models)
        {
            var dao = new WaitProductDAO();
            models.ListProduct = dao.getAll();
            // danh sách kiểm duyệt
            return View(models);
        }
        [HttpPost]
        public JsonResult DeleteApproved(string id)
        {
            var dao = new WaitProductDAO();
            try
            {
                dao.Delete(id);
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                dao.Delete(id);
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult AcceptApproved(string id)
        {
            var dao = new WaitProductDAO();
            try
            {
                var res = dao.Accept(id, Session[LoginAdminSession.ADMIN_SESSION].ToString());
                if (res)
                {
                    dao.Delete(id);
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }

            }
            catch
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult AccProduct()
        {
            return View();
        }
        public ActionResult TypeProducer()
        {
            return View();
        }


    }
}