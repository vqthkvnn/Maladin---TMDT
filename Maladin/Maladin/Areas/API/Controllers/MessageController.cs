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

            return Json(new {count=data.Count, data = data}, JsonRequestBehavior.AllowGet);
        }
    }
}