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
        public JsonResult Changepass(string oldpass, string newpass, string user)
        {
            
            var res = new AccountDAO().Changepass(user, oldpass, newpass);
            return Json(new { status = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getNotiBy(string page, string user)
        {
            var data = new AccountDAO().getNotiBy(Convert.ToInt32(page), user);
            
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
        [HttpPost]
        public JsonResult getFavorite(string user, string page)
        {
            var dao = new ProductDAO();
            var res = dao.getFavoriteProduct(user, Convert.ToInt32(page));
            return Json(new { count = res.Count(), data = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getWatched(string user, string page)
        {
            var dao = new ProductDAO();
            var res = dao.getWatchedProduct(user, Convert.ToInt32(page));
            return Json(new { count = res.Count(), data = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getAllOderList(string user, string page)
        {
            var dao = new AccountDAO();
            var res = dao.getAllOderProduct(user, Convert.ToInt32(page));
            return Json(new { data = res, count = res.Count() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult autoGetNotification(string user)
        {
            var dao = new AccountDAO();
            return Json(new { data = dao.autoGetNotification(user)}, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public JsonResult autoMakeReadNotification(string user)
        {
            var dao = new AccountDAO();
            return Json(new { status = dao.autoUpdateReadNotification(user) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPut]
        public JsonResult updateInformationApp(string user, string name, string phone, string adrs, string gt, string birth)
        {
            var dao = new AccountDAO();
            bool sex = true;
            if(gt=="false")
            {
                sex = false;
            }
            DateTime date = DateTime.ParseExact(birth, "dd/MM/yyyy", null);
            return Json(new { status = dao.UpdateIF(user, name, phone, adrs, sex, date) }, JsonRequestBehavior.AllowGet);
        }
    }
}