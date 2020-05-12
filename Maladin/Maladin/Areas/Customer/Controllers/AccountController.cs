using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Areas.Customer.Common;
using Maladin.DAO;
using Maladin.Models;
using Maladin.Common;

namespace Maladin.Areas.Customer.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Customer/Account
        public ActionResult Index()
        {
            
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