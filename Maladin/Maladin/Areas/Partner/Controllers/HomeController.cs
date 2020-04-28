using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maladin.Areas.Partner.Models;
using Maladin.Areas.Partner.DAO;
using Maladin.EF;
using Maladin.Areas.Partner.Common;
using System.Globalization;

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
        public ActionResult Infomation()
        {
            AccountInfomationModel model = new AccountInfomationModel();
            string user = Session[LoginPartnerSession.USER_SESSION].ToString();
            var dao = new AccountPartnerDAO();
            model.iNFOMATION = dao.getInfomationByAccount(user);
            
            return View(model);
        }
        [HttpPost]
        public JsonResult Infomation(string cmnd, string name, string adr, string sdt, string birth,
                string gt, string email, string note)
        {
            try
            {
                string[] formats = {"dd/MM/yyyy", "dd-MMM-yyyy", "yyyy-MM-dd",
                   "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy"};
                
                var user = Session[LoginPartnerSession.USER_SESSION].ToString();
                var dao = new InformationProductDAO();
                INFOMATION_ACCOUNT icc = dao.getInfoByUser(user);
                INFOMATION_ACCOUNT iNFOMATION = new INFOMATION_ACCOUNT();
                iNFOMATION.CMND_INFO = cmnd;
                iNFOMATION.NAME_INFO = name;
                iNFOMATION.ADRESS_INFO = adr;
                iNFOMATION.PHONE_INFO = sdt;
                iNFOMATION.ID_INFO = icc.ID_INFO;
                
                iNFOMATION.AVT_ACC = icc.AVT_ACC;
                iNFOMATION.USER_ACC = icc.USER_ACC;
                iNFOMATION.BIRTH_INFO = Convert.ToDateTime(DateTime.ParseExact(birth, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM-dd-yyyy"));
                if (gt == "true")
                {
                    iNFOMATION.SEX_INFO = true;
                }
                else
                {
                    iNFOMATION.SEX_INFO = false;
                }

                //iNFOMATION.EMAIL_INFO = email; -- cấm update
                iNFOMATION.NOTE_INFO = note;
                bool res = dao.Update(iNFOMATION);
                if (res)
                {
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                return Json("false", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Reward()
        {
            return View();
        }
    }
}