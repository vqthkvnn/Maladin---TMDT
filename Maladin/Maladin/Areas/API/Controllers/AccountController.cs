using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Areas.API.DAO;
using Maladin.Areas.API.Common;
using Maladin.EF;
using System.Net;

namespace Maladin.Areas.API.Controllers
{
    public class AccountController : Controller
    {
        // GET: API/Account
        [HttpPost]
        public JsonResult Index()
        {
            var dao = new AccountDAO();
            string user = Session[LoginSession.USER_SESSION].ToString();
            var data = dao.getinfo(user);
            return Json(new {user=user, dataInformation=data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete()
        {
            string user = Session[LoginSession.USER_SESSION].ToString();
            var res = new AccountDAO().Delete(user);
            if (res)
            {
                Session.Remove(LoginSession.USER_SESSION);
            }
            return Json(new { status = res}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(string cmnd, string name, string birth,string gt, string adrs, string phone, 
            string note, string type)
        {
            INFOMATION_ACCOUNT entity = new INFOMATION_ACCOUNT();
            if (type == null)
            {
                entity.ID_TYPE_ACC = "CT";
            }
            else
            {
                entity.ID_TYPE_ACC = type;
            }
            entity.NAME_INFO = name;
            entity.ADRESS_INFO = adrs;
            entity.BIRTH_INFO = Convert.ToDateTime(birth);
            entity.CMND_INFO = cmnd;
            entity.PHONE_INFO = phone;
            entity.NOTE_INFO = note;
            entity.SEX_INFO = Convert.ToBoolean(gt);
            entity.USER_ACC = Session[LoginSession.USER_SESSION].ToString();
            var res = new AccountDAO().Update(entity);
            return Json(new { status = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Changepass(string oldpass, string newpass)
        {
            string user = Session[LoginSession.USER_SESSION].ToString();
            var res = new AccountDAO().Changepass(user, oldpass, newpass);
            return Json(new { status = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getNotiBy(string page)
        {
            var data = new AccountDAO().getNotiBy(1);
            
            
            return Json(new {count=data.Count, data=data }, JsonRequestBehavior.AllowGet);
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
                var d = new AccountDAO().getChat(user, userto,i);
                return Json(new { count = d.Count, data = d }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new {count =0}, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult getAllCart(string user)
        {
            if (user == null)
            {
                return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
            }
            var dao = new AccountDAO();
            var data = dao.getAllCart(user);
            return Json(new {data = data, count = data.Count() }, JsonRequestBehavior.AllowGet);
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
            if (res>0)
            {
                return Json(new {status = "true", id=res}, JsonRequestBehavior.AllowGet);
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
            if (res<=0)
            {
                return Json(new { status = res}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = res, data = dao.GetNewMessage(user, userTo) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateRead(string id)
        {
            int Id = Convert.ToInt32(id);
            var dao = new MessageDAO();
            var res = dao.UpdateIsRead(Id);
            if(res)
            {
                return Json(new { status = "true" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = "false" }, JsonRequestBehavior.AllowGet);
        }
    }
}