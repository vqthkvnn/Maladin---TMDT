using Maladin.Areas.Partner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Areas.Partner.DAO;
using Maladin.Areas.Partner.Common;

namespace Maladin.Areas.Partner.Controllers
{
    public class LoginController : Controller
    {
        // GET: Partner/Login
        public ActionResult Index(LoginPartnerModel loginPartnerModel)
        {
            var dao = new AccountPartnerDAO();
            var modelAccount = new AccountInfomationModel();
            if (Session[LoginPartnerSession.USER_SESSION] != null)
            {
                
                return RedirectToAction("Index", "Home", new { username = Session[LoginPartnerSession.USER_SESSION].ToString() });
            }
            
            if (ModelState.IsValid && loginPartnerModel.UserName!="")
            {

                
                var res = dao.CheckLogin(loginPartnerModel.UserName, loginPartnerModel.UserPassword);
                if (loginPartnerModel.UserName == null || loginPartnerModel.UserPassword == null)
                {
                    res = -4;
                }
                if (res == 1)
                {
                    var user = dao.GetByNameAccount(loginPartnerModel.UserName);
                    var userSession = new LoginPartner();
                    userSession.USER_ACC = user.USER_ACC;
                    Session[LoginPartnerSession.USER_SESSION] = loginPartnerModel.UserName;
                    modelAccount.ACCOUNT = dao.GetByNameAccount(loginPartnerModel.UserName);
                    modelAccount.iNFOMATION = dao.getInfomationByAccount(loginPartnerModel.UserName);

                    return RedirectToAction("Index", "Home", new { username = modelAccount.ACCOUNT.USER_ACC });

                }
                else if (res == 0)
                {
                    
                }
                else if (res == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (res == -2)
                {
                    ModelState.AddModelError("", "Tài khoản không được cấp quyền");
                }
                else if (res == -4)
                {
                    
                }
                else
                {
                    ModelState.AddModelError("", "Sai password");
                }
            }
            
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove(LoginPartnerSession.USER_SESSION);
            return View("Index");
        }
    }
}