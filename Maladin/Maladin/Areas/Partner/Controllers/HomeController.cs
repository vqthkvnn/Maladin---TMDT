using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Areas.Partner.Models;
using Maladin.Areas.Partner.DAO;
using Maladin.Areas.Partner.Common;
namespace Maladin.Areas.Partner.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Partner/Home
        public ActionResult Index(HomeModels models)
        {
            

            var dao = new AccountPartnerDAO();
            models.UserName = Session[LoginPartnerSession.USER_SESSION].ToString();
            models.iNFOMATION = dao.getInfomationByAccount(models.UserName);
            return View(models);
        }
    }
}