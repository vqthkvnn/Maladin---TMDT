using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maladin.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WishList()
        {
            return View();
        }

        public ActionResult AccountInformation()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult MyNotification()        //quan ly thong bao
        {
            return View();
        }

        public ActionResult ManagerOrder()      //quan ly don hang
        {
            return View();
        }

        public ActionResult ReviewProductBought()       //nhan xet san pham da mua
        {
            return View();
        }
        public ActionResult MyReview()      //nhan xet cua toi
        {
            return View();
        }
        public ActionResult ManagerMaladinCoin()        //quan ly maladin coin
        {
            return View();
        }
    }
}