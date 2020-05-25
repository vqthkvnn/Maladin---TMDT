using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Models;
using Maladin.DAO;
using Maladin.EF;
using Maladin.Areas.Customer.Common;
using Maladin.Common;

namespace Maladin.Areas.Customer.Controllers
{
    public class LoginController : Controller
    {
        // GET: Customer/Login
        public ActionResult Index(CustomerLoginModel model)
        {
            if (Session[CustomerLoginSession.CUSTOMER_SESSION]!=null)
            {
                return RedirectToAction("Index", "Account");
            }
            else
            {
                if(ModelState.IsValid&& model.UserName !=null && model.Password!=null)
                {
                    var dao = new CustomerLoginDAO();
                    var res = dao.Login(model.UserName, model.Password);
                    if (res ==1)
                    {
                        Session[CustomerLoginSession.CUSTOMER_SESSION] = model.UserName;
                        return RedirectToAction("Index", "Account", new { area = "Customer" });
                    }
                    else if (res ==0)
                    {
                        ModelState.AddModelError("", "Tài khoản không đúng hoặc không tồn tại");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Sai mật khẩu");
                    }
                }
            }
            return View();
        }
        public ActionResult Register(RegisterCustomerModel model)
        {
            if (ModelState.IsValid && model.UserName !=null && model.Password != null 
                && model.RePassword != null && model.Email != null)
            {
                var dao = new CustomerLoginDAO();
                var res = dao.Register(model.UserName, model.Email);
                if (res==0)
                {
                    ACCOUNT entity = new ACCOUNT();
                    try
                    {
                        entity.USER_ACC = model.UserName;
                        entity.EMAIL_INFO = model.Email;
                        entity.PASSWORD_ACC = model.Password;
                        entity.ID_TYPE_ACC = "CT";
                        entity.COINT_ACC = 0;
                        entity.DATE_CREATE_ACC = DateTime.Today;
                        entity.IS_ACTIVE_ACC = true;
                        var r = dao.Create(entity);
                        if (r)
                        {
                            return RedirectToAction("Index", "Home", new { area = "" });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Tạo tài khoản thất bại");
                        }

                    }
                    catch(Exception e)
                    {

                        ModelState.AddModelError("", "Tạo tài khoản thất bại");
                    }
                }
                else if (res == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đã tồn tại!");
                }
                else if(res == -2)
                {
                    ModelState.AddModelError("", "Email đã tồn tại!");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Remove(CustomerLoginSession.CUSTOMER_SESSION);
            return RedirectToAction("Index", "Home", new { Area="" });
        }
        
    }
}