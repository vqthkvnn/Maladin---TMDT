using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Areas.API.DAO;
namespace Maladin.Areas.API.Controllers
{
    public class MessageController : Controller
    {
        // GET: API/Message
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult getLastMessage(string user)
        {
            var dao = new MessageDAO();
            var data = dao.getAllLastMessage(user);
            if (user == null)
            {
                return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
            }

            return Json(new {count=data.Count, data = data}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateReadMessage(string user, string to)
        {
            var dao = new MessageDAO();
            var res = dao.UpdateReadAll(user, to);
            if (res)
            {
                return Json(new { status="true"},JsonRequestBehavior.AllowGet );
            }
            return Json(new { status = "false" }, JsonRequestBehavior.AllowGet);
        }
    }
}