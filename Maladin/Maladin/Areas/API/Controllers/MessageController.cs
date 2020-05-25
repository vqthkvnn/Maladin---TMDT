using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Maladin.Areas.API.DAO;
using Maladin.EF;

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
        [HttpPost]
        public JsonResult NewMessage(string user, string userTo)
        {
            if (user == null)
            {
                return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
            }
            var dao = new MessageDAO();
            var res = dao.CountNewMessage(user, userTo);
            if (res <= 0)
            {
                return Json(new { status = res }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = res, data = dao.GetNewMessage(user, userTo) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public JsonResult UpdateRead(string id)
        {
            int Id = Convert.ToInt32(id);
            var dao = new MessageDAO();
            var res = dao.UpdateIsRead(Id);
            if (res)
            {
                return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = "false" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getChat(string user, string userto, string page)
        {
            try
            {
                if (user == null)
                {
                    return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
                }
                int i = Convert.ToInt32(page);
                var d = new AccountDAO().getChat(user, userto, i);
                return Json(new { count = d.Count, data = d }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Send(string user, string to, string content)
        {
            if (user == null)
            {
                return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
            }

            var dao = new MessageDAO();
            MESSAGE_SEND_TO msg = new MESSAGE_SEND_TO();
            msg.FROM_ACC = user;
            msg.TO_ACC = to;

            msg.CONTEN_MESSAGE = WebUtility.UrlDecode(content);
            msg.DATA_SEND_MESSAGE = DateTime.Now;
            msg.IS_READ = false;
            var res = dao.InsertMessage(msg);
            if (res > 0)
            {
                return Json(new { status = "true", id = res }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = "false" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetInfoForMessageFromIDP(string idp)
        {
            var dao = new MessageDAO();
            return Json(new { data = dao.getInfomationForIDP(idp) }, JsonRequestBehavior.AllowGet);
        }
    }
}