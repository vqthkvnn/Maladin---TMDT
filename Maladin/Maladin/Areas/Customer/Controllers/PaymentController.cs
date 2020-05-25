using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Web.Mvc;
using Maladin.DAO;
using Maladin.Common;
namespace Maladin.Areas.Customer.Controllers
{
    public class PaymentController : BaseController
    {
        // GET: Customer/Payment
        public ActionResult Index()
        {
            

            return View();
        }
        [HttpPost]
        public JsonResult addToCart(string idp, string count)
        {
            string user = Session[CustomerLoginSession.CUSTOMER_SESSION].ToString();
            count = (count ?? "1");
            var dao = new PaymentDAO();
            try
            {
                if (dao.IsProduct(idp))
                {
                    var kq = dao.AddToCart(user, idp, Convert.ToInt32(count));
                    if (kq)
                    {
                        return Json(new { status = 1 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { status = -2 }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { status = -1 }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult ApplyVoucher(string vocher)
        {
            var dao = new PaymentDAO();
            return Json(new { sumprice = dao.SumPriceVoucher(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString(), vocher) },
                JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoveCart(string idp)
        {
            string user = Session[CustomerLoginSession.CUSTOMER_SESSION].ToString();
            var dao = new PaymentDAO();
            try
            {
                if (dao.IsProduct(idp))
                {
                    var kq = dao.RemoveCart(user, idp);
                    if (kq)
                    {
                        return Json(new { status = 1 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { status = -2 }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { status = -1 }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Confirm(string vocher)
        {
            // configure email
            
            var infoDAO = new CustomerLoginDAO();
            var payDAO = new PaymentDAO();
            ViewBag.Voucher = vocher;
            ViewBag.TotalCart = infoDAO.getTotalCart(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString());
            if (vocher != null)
            {
                /*
                 * Thanh toan bang voucher
                 */
                
                var ifo = infoDAO.getInformationByUser(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString());
                ViewBag.Name = ifo.NAME_INFO;
                ViewBag.Phone = ifo.PHONE_INFO;
                ViewBag.Adress = ifo.ADRESS_INFO;
                var listCart = new List<Tuple<string, string, int>>();
                foreach(var i in infoDAO.getAllCart(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString()))
                {
                    var value =Convert.ToInt32(i.Price * (100 - i.saleP) / 100 * i.TotalCount);
                    listCart.Add(new Tuple<string, string, int>(i.Name, Convert.ToString(value), i.TotalCount));
                }
                ViewBag.Listproduct = listCart;
                ViewBag.Vocher = payDAO.SumPriceVoucher(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString(), vocher);
            }
            else
            {
                /*
                 * Thanh toan k bang voucher
                 */
                
                var ifo = infoDAO.getInformationByUser(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString());
                ViewBag.Name = ifo.NAME_INFO;
                ViewBag.Phone = ifo.PHONE_INFO;
                ViewBag.Adress = ifo.ADRESS_INFO;
                var sumprice = 0;
                var listCart = new List<Tuple<string, string, int>>();
                foreach (var i in infoDAO.getAllCart(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString()))
                {
                    var value = Convert.ToInt32(i.Price * (100 - i.saleP) / 100 * i.TotalCount);
                    sumprice += value;
                    listCart.Add(new Tuple<string, string, int>(i.Name, Convert.ToString(value), i.TotalCount));
                }
                ViewBag.Listproduct = listCart;
                ViewBag.Vocher = sumprice;
            }
            return View();
        }
        [HttpPost]
        public ActionResult CheckCoint(string vocher)
        {
            var infoDAO = new CustomerLoginDAO();
            var payDAO = new PaymentDAO();
            var sumPrice = 0;
            foreach (var i in infoDAO.getAllCart(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString()))
            {
                sumPrice+= Convert.ToInt32(i.Price * (100 - i.saleP) / 100 * i.TotalCount);
            }
            var user = infoDAO.getAccountByUser(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString());
            if (vocher == null || vocher == "")
            {
                if (user.COINT_ACC - sumPrice >= 0)
                {
                    return Json(new { status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new {status = false}, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var priceVocher = payDAO.SumPriceVoucher(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString(), vocher);
                if (user.COINT_ACC - sumPrice+priceVocher >= 0)
                {
                    return Json(new { status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public JsonResult AcceptPaymentCoin(string IDVocher, string type)
        {
            /*
             * chấp nhận thanh toán -> xử lý thanh toán theo hình thức có tài khoản
             * thêm mới n-> đơn -> n sản phẩm trong lô hàng
             */
            var daoP = new PaymentDAO();
            List<string> allID = new List<string>();
            allID = daoP.AutoRenderOderFromUser(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString(), IDVocher, type);
            /*
             * tiếp theo là thanh toán
             */
            var resIDP = daoP.Payment(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString(), allID);
            return Json(new { status = resIDP}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AcceptPaymentNoCoin(string IDVocher, string type)
        {
            /*
             * chấp nhận thanh toán -> xử lý thanh toán theo hình thức có tài khoản
             * thêm mới n-> đơn -> n sản phẩm trong lô hàng
             */
            var daoP = new PaymentDAO();
            List<string> allID = new List<string>();
            try
            {
                allID = daoP.AutoRenderOderFromUser(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString(), IDVocher, type);
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
            /*
             * tiếp theo là thanh toán
             */
            
        }
    }
}