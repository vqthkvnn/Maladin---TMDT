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
            if (ModelState.IsValid|| loginPartnerModel.UserName=="")
            {
                var dao = new AccountPartnerDAO();
                var res = dao.CheckLogin(loginPartnerModel.UserName, loginPartnerModel.UserPassword);
                if (res == 1)
                {
                    var user = dao.GetByNameAccount(loginPartnerModel.UserName);
                    var userSession = new LoginPartner();
                    userSession.USER_ACC = user.USER_ACC;
                    Session.Add(LoginPartnerSession.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (res == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (res == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (res == -2)
                {
                    ModelState.AddModelError("", "Tài khoản không được cấp quyền");
                }
                else
                {
                    ModelState.AddModelError("", "Sai password");
                }
            }
            else
            {

            }
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}