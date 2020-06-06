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
using System.IO;
using System.Threading;
using System.Text;

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
            string[] Scopes = { GmailService.Scope.GmailSend };
            string ApplicationName = "SendMail";
            var infoDAO = new CustomerLoginDAO();
            var daoP = new PaymentDAO();
            List<string> allID = new List<string>();
            allID = daoP.AutoRenderOderFromUser(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString(), IDVocher, type);
            /*
             * tiếp theo là thanh toán
             */
            var resIDP = daoP.Payment(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString(), allID);
            /*var resdb = infoDAO.getinfo(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString());
            string lod = "";
            foreach (var i in allID)
                lod += i;
            string contentemail = "<h1>Chi tiết hóa đơn</h1><br>" +
                "<p>Họ tên người nhận:" + resdb.name +
                "</p><br>" +
                "<p>Số điện thoại:" + resdb.phone +
                "</p><br>" +
                "<p>Email:" + resdb.email +
                "</p><br>" +
                "<p>Số tiền thanh toán:" + Convert.ToString(daoP.SumPriceOrder(allID)) +
                " VND</p><br>" +
                "<p>Hình thức thanh toán:Tài khoản</p><br>" +
                "<p>Danh sách hóa đơn bạn mua:" + lod +
                "</p>";
            UserCredential credential;
            //read your credentials file
            using (var stream =
            new FileStream("C:\\Users\\Tuyen\\Documents\\GitHub\\Maladin---TMDT\\Maladin\\Maladin\\public\\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            string message = $"To: {resdb.email}\r\nSubject:Maladin.com - Payment Oder\r\nContent-Type: text/html;charset=utf-8\r\n\r\n{contentemail}";
            //call your gmail service
            var service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential, ApplicationName = ApplicationName });
            var msg = new Google.Apis.Gmail.v1.Data.Message();
            msg.Raw = Base64UrlEncode(message.ToString());
            service.Users.Messages.Send(msg, "me").Execute();*/
            return Json(new { status = resIDP}, JsonRequestBehavior.AllowGet);
        }
        string Base64UrlEncode(string input)
        {
            var data = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(data).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
        [HttpPost]
        public JsonResult AcceptPaymentNoCoin(string IDVocher, string type)
        {
            /*
             * chấp nhận thanh toán -> xử lý thanh toán theo hình thức có tài khoản
             * thêm mới n-> đơn -> n sản phẩm trong lô hàng
             */
            string[] Scopes = { GmailService.Scope.GmailSend };
            string ApplicationName = "SendMail";
            var infoDAO = new CustomerLoginDAO();
            var daoP = new PaymentDAO();
            List<string> allID = new List<string>();
            try
            {
                allID = daoP.AutoRenderOderFromUser(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString(), IDVocher, type);
                var resdb = infoDAO.getinfo(Session[CustomerLoginSession.CUSTOMER_SESSION].ToString());
                string lod = "";
                foreach (var i in allID)
                    lod += i;
                string contentemail = "<h1>Chi tiết hóa đơn</h1><br>" +
                    "<p>Họ tên người nhận:" + resdb.name +
                    "</p><br>" +
                    "<p>Số điện thoại:" + resdb.phone +
                    "</p><br>" +
                    "<p>Email:" + resdb.email +
                    "</p><br>" +
                    "<p>Số tiền thanh toán:" + Convert.ToString(daoP.SumPriceOrder(allID)) +
                    " VND</p><br>" +
                    "<p>Hình thức thanh toán:Tài khoản</p><br>" +
                    "<p>Danh sách hóa đơn bạn mua:" + lod +
                    "</p>";
                UserCredential credential;
                //read your credentials file
                using (var stream =
                new FileStream("C:\\Users\\Tuyen\\Documents\\GitHub\\Maladin---TMDT\\Maladin\\Maladin\\public\\client_secret.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }
                string message = $"To: {resdb.email}\r\nSubject:Maladin.com - Payment Oder\r\nContent-Type: text/html;charset=utf-8\r\n\r\n{contentemail}";
                //call your gmail service
                var service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential, ApplicationName = ApplicationName });
                var msg = new Google.Apis.Gmail.v1.Data.Message();
                msg.Raw = Base64UrlEncode(message.ToString());
                service.Users.Messages.Send(msg, "me").Execute();
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