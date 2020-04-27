using Maladin.Areas.Admin.Common;
using Maladin.Areas.Admin.DAO;
using Maladin.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maladin.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index(LoginAdminModel model)
        {
            if (Session[LoginAdminSession.ADMIN_SESSION] != null)
            {
                return RedirectToAction("Index", "Home", new { user = Session[LoginAdminSession.ADMIN_SESSION].ToString() });
            }
            if (ModelState.IsValid)
            {
                var dao = new AccountAdminDAO();
                var res = dao.CheckLogin(model);
                if (model.UserAdmin == null || model.PasswordAdmin == null)
                {
                    res = -4;
                }
                if (res == 1)
                {
                    var user = dao.GetByNameAccount(model.UserAdmin);
                    var userSession = new LoginAdmin();
                    
                    Session[LoginAdminSession.ADMIN_SESSION] = model.UserAdmin;
                    

                    return RedirectToAction("Index", "Home");

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
        public ActionResult Logout()
        {
            Session.Remove(LoginAdminSession.ADMIN_SESSION);
            return View("Index");
        }
        
    }
}