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
        [HttpDelete]
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
        public JsonResult GetIF(string user)
        {
            
            var res = new AccountDAO().getinfo(user);
            return Json(new { data = res }, JsonRequestBehavior.AllowGet);


        }
        [HttpPut]
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
        [HttpPut]
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
        
        
    }
}