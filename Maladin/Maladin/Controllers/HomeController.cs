using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Models;
using Maladin.DAO;
using Maladin.Areas.Customer.Common;
using Facebook;
using Newtonsoft.Json;
using System.Web.Security;

namespace Maladin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(ItemProductModels models)
        {
            var dao = new ProductHomeDAO();
            models.GiayProduct = dao.getListProduct(1, "TP001");
            models.DongHoProduct = dao.getListProduct(1, "TP002");
            models.KinhProduct = dao.getListProduct(1, "TP003");
            if (Session[CustomerSession.CUSTOMER_SESSION] == null)
            {
                ViewBag.IsLogin = null;
            }
            else
            {
                var da =  new CustomerLoginDAO();

                ViewBag.IsLogin = da.getNameUser(Session[CustomerSession.CUSTOMER_SESSION].ToString(),
                   "CT");
            }
            
            return View(models);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult ShoppingCart()
        {
            return View();
        }

        public ActionResult ProductDetail()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
        public JsonResult GetProductList(int page, string type)
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string type, string page, string sortBy)
        {
            return View();
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("facebookCallback");
                return uriBuilder.Uri;
            }
        }

        private ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "371532303805461",
                client_secret = "395a4d72c278fb142b6371549e61d2df", 
                redirect_uri = RedirectUri.AbsoluteUri,
                respone_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "371532303805461",
                client_secret = "395a4d72c278fb142b6371549e61d2df",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            Session["AccessToken"] = accessToken;
            fb.AccessToken = accessToken;
            dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
            string email = me.email;
            string lastName = me.last_name;
            string picture = me.picture.data.url;
            FormsAuthentication.SetAuthCookie(email, false);
            return RedirectToAction("FacebookLogin", "Home");
        }
    }
}