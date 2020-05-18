using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Areas.Customer.Common;
using Maladin.DAO;
using Maladin.Models;
using Maladin.Common;
using Maladin.EF;

namespace Maladin.Areas.Customer.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Customer/Account
        public ActionResult Index()
        {

            var dao = new CustomerLoginDAO();
            INFOMATION_ACCOUNT info = dao.getInformationByUser(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString());
            var email = dao.getAccountByUser(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString());
            ViewBag.email = email.EMAIL_INFO;
            ViewBag.information = info;
            
            ViewBag.day = info.BIRTH_INFO.Value.Day;
            ViewBag.month = info.BIRTH_INFO.Value.Month;
            ViewBag.year = info.BIRTH_INFO.Value.Year;
            return View();
        }
        
        
        
        public ActionResult Infomation()
        {
            
            return View();
        }
        public ActionResult Cart()
        {
            
            var dao = new CustomerLoginDAO();
            var data = dao.getAllCart(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString());
            return View(data);
        }
        public ActionResult Notification()
        {

            return View();
        }
        public ActionResult Oder()
        {

            return View();
        }
        public ActionResult Comment()
        {

            return View();
        }
        public ActionResult Question()
        {

            return View();
        }
        public ActionResult Coin()
        {
            var dao = new CustomerLoginDAO();
            ViewBag.Coin = dao.getCoin(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString());
            return View();
        }
        public ActionResult Favourite()
        {

            return View();
        }
        public ActionResult Watched()
        {

            return View();
        }
    }
}