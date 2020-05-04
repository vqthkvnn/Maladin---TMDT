using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Areas.API.Common;
using Maladin.Areas.API.DAO;
using Maladin.EF;
namespace Maladin.Areas.API.Controllers
{
    public class LoginController : Controller
    {
        // GET: API/Login
        [HttpPost]
        public JsonResult Index(string users, string password)
        {
            if (Session[LoginSession.USER_SESSION] !=null)
            {
                return Json(new {status = 1, user = Session[LoginSession.USER_SESSION].ToString() }, JsonRequestBehavior.AllowGet);
            }
            
            var dao = new AccountDAO();
            var res = dao.Login(users, password);
            if (res == 1)
            {
                Session[LoginSession.USER_SESSION] = users;
                return Json(new { status = res, user = users }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = res, user = users }, JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpPost]
        public JsonResult Logout(string user)
        {
            try
            {
                if (Session[LoginSession.USER_SESSION]!=null)
                {
                    Session.Remove(LoginSession.USER_SESSION);
                    return Json(new { status = 1, user = user }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = 0, user = user }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch(Exception)
            {
                return Json(new { status = 0, user = user }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult Create(string user, string pass, string email)
        {
            ACCOUNT aCCOUNT = new ACCOUNT();
            aCCOUNT.USER_ACC = user;
            aCCOUNT.PASSWORD_ACC = pass;
            aCCOUNT.ID_TYPE_ACC = "CT";
            aCCOUNT.COINT_ACC = 0;
            aCCOUNT.DATE_CREATE_ACC = DateTime.Today;
            aCCOUNT.EMAIL_INFO = email;
            var res = new AccountDAO().Insert(aCCOUNT);
            return Json(new { res = res }, JsonRequestBehavior.AllowGet);
        }

    }
}