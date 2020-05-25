using Maladin.Areas.API.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Text;
using System.Threading;

namespace Maladin.Areas.API.Controllers
{
    public class PaymentController : Controller
    {
        // GET: API/Payment
        
        [HttpPost]
        public JsonResult ApplyVoucher(string user, string vocher)
        {
            var dao = new PayDAO();
            return Json(new { sum = dao.SumPriceVoucher(user, vocher) },
                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getInformationPayMent(string user)
        {
            var dao = new PayDAO();
            return Json(new { data = dao.getInformation(user) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult checkCoinWithVoucher(string user, string voucher)
        {
            var dao = new PayDAO();
            var sumprice = dao.SumPriceVoucher(user, voucher);
            var res = dao.CheckCoin(user, sumprice);
            return Json(new { res = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult PaymentFor(string user, string voucher, string type)
        {
            try
            {
                
                var dao = new PayDAO();
                var sumprice = dao.SumPriceVoucher(user, voucher);
                if (type == "TK")
                {
                    var res = dao.CheckCoin(user, sumprice);
                    if (res)
                    {
                        var listoder = dao.AutoRenderOderFromUser(user, voucher, type);

                        var IDpay = dao.Payment(user, listoder);
                        if (IDpay == -1)
                        {
                            return Json(new { status = IDpay, content = "Lỗi khi nhận dữ liệu hóa đơn. Vui lòng liên hệ admin để được trợ giúp" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            
                            //
                            String date = DateTime.Now.ToString("dd-MM-yyyy");
                            return Json(new { status = IDpay, content = "Thanh toán bằng Coin thành công", 
                                data = listoder, count = listoder.Count(), sum = sumprice, date = date }, 
                                JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { status = -1, content = "Không đủ Coin để thanh toán" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if(type == "TT")
                {
                    var listoder = dao.AutoRenderOderFromUser(user, voucher, type);
                    if(listoder.Count()>0)
                    {
                        String date = DateTime.Now.ToString("dd-MM-yyyy");
                        return Json(new { status = 1, content = "Đơn hàng của bạn đã được tiếp nhận", data = listoder, count=listoder.Count(), sum = sumprice,
                        date = date}, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { status = -1, content = "Không có đơn hàng nào được tiếp nhận" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { status = -1, content = "Hệ thống từ chối hình thức thanh toán của bạn" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e)
            {
                return Json(new { status = -1, content = "Thanh toán không thành công do lỗi hệ thống" }, JsonRequestBehavior.AllowGet);
            }
            
            
        }
        [HttpPost]
        public JsonResult PaymentForEmail(string user, string voucher, string type)
        {
            try
            {
                //
                string[] Scopes = { GmailService.Scope.GmailSend };
                string ApplicationName = "SendMail";
                //
                var dao = new PayDAO();
                var daoacc = new AccountDAO();

                var sumprice = dao.SumPriceVoucher(user, voucher);
                if (type == "TK")
                {
                    var res = dao.CheckCoin(user, sumprice);
                    if (res)
                    {
                        var listoder = dao.AutoRenderOderFromUser(user, voucher, type);

                        var IDpay = dao.Payment(user, listoder);
                        if (IDpay == -1)
                        {
                            return Json(new { status = IDpay, content = "Lỗi khi nhận dữ liệu hóa đơn. Vui lòng liên hệ admin để được trợ giúp" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var resdb = daoacc.getinfo(user);
                            string lod = "";
                            foreach (var i in listoder)
                                lod += i;
                            string contentemail = "<h1>Chi tiết hóa đơn</h1><br>" +
                                "<p>Họ tên người nhận:" + resdb.name +
                                "</p><br>" +
                                "<p>Số điện thoại:" + resdb.phone +
                                "</p><br>" +
                                "<p>Email:" + resdb.email +
                                "</p><br>" +
                                "<p>Số tiền thanh toán:" + Convert.ToString(sumprice) +
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
                            //
                            String date = DateTime.Now.ToString("dd-MM-yyyy");
                            return Json(new
                            {
                                status = IDpay,
                                content = "Thanh toán bằng Coin thành công",
                                data = listoder,
                                count = listoder.Count(),
                                sum = sumprice,
                                date = date
                            },
                                JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { status = -1, content = "Không đủ Coin để thanh toán" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (type == "TT")
                {
                    var listoder = dao.AutoRenderOderFromUser(user, voucher, type);
                    if (listoder.Count() > 0)
                    {
                        String date = DateTime.Now.ToString("dd-MM-yyyy");
                        return Json(new
                        {
                            status = 1,
                            content = "Đơn hàng của bạn đã được tiếp nhận",
                            data = listoder,
                            count = listoder.Count(),
                            sum = sumprice,
                            date = date
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { status = -1, content = "Không có đơn hàng nào được tiếp nhận" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { status = -1, content = "Hệ thống từ chối hình thức thanh toán của bạn" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { status = -1, content = "Thanh toán không thành công do lỗi hệ thống" }, JsonRequestBehavior.AllowGet);
            }


        }
        [HttpPost]
        public JsonResult getInfoByIDO(string ido, string user)
        {
            var dao = new PayDAO();
            var res = dao.getInfoByIDO(ido, user);
            if(res == null)
            {
                return Json(new { res = res, status = -1 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { res = res, status = 1 }, JsonRequestBehavior.AllowGet);
            }
            
        }
        string Base64UrlEncode(string input)
        {
            var data = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(data).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
        [HttpDelete]
        public JsonResult CancelOderBy(string ido, string user)
        {
            var dao = new PayDAO();
            return Json(new {status = dao.CancelOder(ido, user) }, JsonRequestBehavior.AllowGet);
        }
    }
}