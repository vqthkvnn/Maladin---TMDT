using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.EF;
namespace Maladin.DAO
{
    public class PaymentDAO
    {
        TMDT_Maladin db = null;
        public PaymentDAO()
        {
            db = new TMDT_Maladin();
        }
        public bool InsertGuest(string s)
        {
            return true;
        }
        public string InserOderByCustomer(string IDACC_PRO, string User, string type, int count, string IDVocher)
        {
            try
            {
                /*
                * them moi cac hoa don
                */
                int leght = db.ODERs.Where(x => x.ID_ACC_PRODUCT == IDACC_PRO).Count();
                /*
                 Tao ma cho oder
                 */
                var product = db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == IDACC_PRO);
                string idOder = IDACC_PRO.Substring(0, 4) + Convert.ToString(leght + 1);
                ODER oDER = new ODER();
                oDER.ID_ODER = idOder;
                oDER.USER_ACC = User;
                oDER.ID_ACC_PRODUCT = IDACC_PRO;
                oDER.COUNT_ODER = count;
                oDER.ID_TYPE_ODER = type;
                oDER.STATUS_ODER = 0; // chuyển đơn cho đơn vị bán -> check tiền auto
                /*
                 * check vocher:
                 */
                var MonneyFromVocher = 0;
                /*
                 * fix lỗi liên quan đến việc khách k dùng vocher
                 * đối với khách dùng vocher cần comment lại ở note
                 */
                if (CheckAreaVocher(IDVocher, IDACC_PRO))
                {
                    var vocher = db.VOCHERs.SingleOrDefault(x => x.ID_VOCHER == IDVocher);
                    /*
                     * xử lý max price
                     */
                    if (vocher.MAX_SUM_PRICE != 0)
                    {
                        var maxPricent = vocher.PERCENT_VOCHER * count * product.AMOUNT;
                        if (maxPricent >= vocher.MAX_SUM_PRICE || vocher.MONEY_VOCHER >= vocher.MAX_SUM_PRICE)
                        {
                            MonneyFromVocher = (int)vocher.MAX_SUM_PRICE;
                        }
                        else if (maxPricent > vocher.MONEY_VOCHER)
                        {
                            MonneyFromVocher = maxPricent;
                        }
                        else
                        {
                            MonneyFromVocher = (int)vocher.MONEY_VOCHER;
                        }
                    }
                    else
                    {
                        var maxPricent = vocher.PERCENT_VOCHER * count * product.AMOUNT + vocher.MONEY_VOCHER;
                    }
                    oDER.NOTE_ODER = "Khách thanh toán có sử dụng Voucher: " + IDVocher;
                }
                if (count * product.AMOUNT - MonneyFromVocher <= 0)
                {
                    oDER.SUM_PRICE_ODER = 0;
                }
                else
                {
                    oDER.SUM_PRICE_ODER = count * product.AMOUNT - MonneyFromVocher;
                }
                oDER.DATE_ODER = DateTime.UtcNow;
                db.ODERs.Add(oDER);
                db.SaveChanges();
                
                return idOder;
            }
            catch(Exception e)
            {
                return "false";
            }
            
        }
        public bool CheckAreaVocher(string IDVocher, string IDACC_PRO)
        {
            /*
             * check xem vocher con han k
             */

            /*check xem vocher co duoc kick hoat cho lo hang kia k*/
            var vocher = db.VOCHERs.SingleOrDefault(x => x.ID_VOCHER == IDVocher);
            if (vocher == null)
            {
                return false;
            }
            if (!vocher.IS_STATUS|| vocher.AMOUNT_VOCHER<=0)
            {
                return false;
            }
            var resAll = db.VOCHER_AREA.SingleOrDefault(x => x.ID_VOCHER == IDVocher);
            if (resAll==null)
            {
                return false; //
            }
            else
            {
                var date = (DateTime.Today - db.VOCHERs.SingleOrDefault(x => x.ID_VOCHER == IDVocher).DATE_END_VOCHER).Value.Days;
                if (date<=0)
                {
                    if (resAll.ID_ACC_PRODUCT == IDACC_PRO)
                    {
                        return true;
                    }
                    if (resAll.ID_ACC_PRODUCT == null
                        && resAll.ID_PRODUCT == db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == IDACC_PRO).ID_PRODUCT)
                    {
                        
                        return true; // cai nay la vocher cho tat ca id product
                    }
                    return false;
                }
                return false;
                
            }
        }
        public string InserOderByGuest(string IDACC_PRO, int guest, string type, int count, string IDVocher)
        {
            /*
             * thanh toan qua thang guest -> 
             */
            try
            {
                var leght = db.ODERs.Count();
                var product = db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == IDACC_PRO);
                ODER oDER = new ODER();
                string idOder = IDACC_PRO.Substring(0, 4) + Convert.ToString(leght + 1);
                oDER.ID_ODER = idOder;
                oDER.ID_GUEST_NO_ACC = guest;
                oDER.ID_ACC_PRODUCT = IDACC_PRO;
                oDER.COUNT_ODER = count;
                oDER.ID_TYPE_ODER = type;
                oDER.STATUS_ODER = 0; // chuyển do nó k cần thanh toán
                /*
                 * check vocher:
                 */
                var MonneyFromVocher = 0;
                if (CheckAreaVocher(IDVocher, IDACC_PRO))
                {
                    var vocher = db.VOCHERs.SingleOrDefault(x => x.ID_VOCHER == IDVocher);
                    /*
                     * xử lý max price
                     */
                    if (vocher.MAX_SUM_PRICE != 0)
                    {
                        var maxPricent = vocher.PERCENT_VOCHER * count * product.AMOUNT;
                        if (maxPricent >= vocher.MAX_SUM_PRICE || vocher.MONEY_VOCHER >= vocher.MAX_SUM_PRICE)
                        {
                            MonneyFromVocher = (int)vocher.MAX_SUM_PRICE;
                        }
                        else if (maxPricent > vocher.MONEY_VOCHER)
                        {
                            MonneyFromVocher = maxPricent;
                        }
                        else
                        {
                            MonneyFromVocher = (int)vocher.MONEY_VOCHER;
                        }
                    }
                    else
                    {
                        var maxPricent = vocher.PERCENT_VOCHER * count * product.AMOUNT + vocher.MONEY_VOCHER;
                    }
                }
                if (count * product.AMOUNT - MonneyFromVocher <= 0)
                {
                    oDER.SUM_PRICE_ODER = 0;
                }
                else
                {
                    oDER.SUM_PRICE_ODER = count * product.AMOUNT - MonneyFromVocher;
                }
                oDER.DATE_ODER = DateTime.UtcNow;
                db.ODERs.Add(oDER);
                db.SaveChanges();
                return idOder; // thằng này không cần id hóa đơn

            }
            catch(Exception e)
            {
                return "false";
            }
        }
        public int InsertGuest(INFOMATION_GUEST entity)
        {
            try
            {
                
                db.INFOMATION_GUEST.Add(entity);
                db.SaveChanges();
                return entity.ID_GUEST;
            }
            catch(Exception e)
            {
                return -1;
            }
        }
        public int Payment(string user, List<string> listoder)
        {
            /*
             * chuc nang nay chi co khi tai khoan
             * hệ thống kiểm tra khả thanh toán trước khi vào đây
             * tức là vào đây auto qua đơn
             */
            var res = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);
            double totalPay=0;
            for(int i=0;i<listoder.Count;i++)
            {
                var product = db.ODERs.SingleOrDefault(x => x.ID_ODER == listoder[i]);
                if (product == null)
                {
                    totalPay += 0;
                }
                else
                {
                    totalPay += product.SUM_PRICE_ODER;
                }
            }
            if (res.COINT_ACC>=totalPay)
            {
                /*
                 * chap nhan thanh toan -> them thanh toan
                 */
                
                PAYMENT aYMENT = new PAYMENT();
                aYMENT.USER_ACC = user;
                aYMENT.SEND_COINT = (int)totalPay;
                aYMENT.DATE_PAYMENT = DateTime.UtcNow;
                aYMENT.CONTENT_PAYMENT = "Thanh toán hóa đơn cho các đơn hàng:";
                aYMENT.IS_CONFIRM = true;
                db.PAYMENTs.Add(aYMENT);
                db.SaveChanges();
                /*
                 * Tru tai' khoan
                 */
                var acc = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);
                db.ACCOUNTs.Attach(acc);
                acc.COINT_ACC -= (int)totalPay;
                db.SaveChanges();
                var last = aYMENT;
                db.PAYMENTs.Attach(last);
                foreach (var i in listoder)
                {
                    PAYMENT_ODER pAYMENT_ODER = new PAYMENT_ODER();
                    pAYMENT_ODER.ID_PAYMENT = last.ID_PAYMENT;
                    pAYMENT_ODER.ID_ODER = i;
                    
                    pAYMENT_ODER.NOTE_PAYMENT_ODER = "Mã giao dịch thanh toán là:" + Convert.ToString(last.ID_PAYMENT);
                    db.PAYMENT_ODER.Add(pAYMENT_ODER);
                    
                    last.CONTENT_PAYMENT += " " + i;
                    /*
                    * sau khi thanh toan thanh cong thi tao giao dịch cho thằng bán và giảm số sản phẩm đã bán của nó đi
                    */
                    var oder = db.ODERs.SingleOrDefault(x => x.ID_ODER == i);
                    var product = db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == oder.ID_ACC_PRODUCT);
                    PAYMENT pAYMENT = new PAYMENT();
                    pAYMENT.USER_ACC = product.USER_ACC;
                    pAYMENT.RECEIVE_COINT = (int)oder.SUM_PRICE_ODER;
                    pAYMENT.IS_CONFIRM = true;
                    pAYMENT.DATE_PAYMENT = DateTime.UtcNow;
                    pAYMENT.CONTENT_PAYMENT = "Tiền nhận được từ việc thanh toán hóa đơn: " + oder.ID_ODER;
                    db.ACC_PRODUCT.Attach(product);
                    product.SELL_COUNT += oder.COUNT_ODER;
                    db.PAYMENTs.Add(pAYMENT);
                }
                db.SaveChanges();
                return aYMENT.ID_PAYMENT;
                /* thanh toan thanh cong*/
            }
            else
            {
                return -1; // thanh toan that bai, oder chuyen het ve trang thai -1 cho thanh toan
            }

            
            
        }
        public bool AddToCart(string user, string idp, int count)
        {
            try
            {
                var res = db.WATCHED_PRODUCT.Where(x => x.USER_ACC == user).Where(x => x.ID_ACC_PRODUCT == idp).SingleOrDefault();
                db.WATCHED_PRODUCT.Attach(res);
                res.CART_COUNT = count;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }
        public bool IsProduct(string product)
        {
            if (db.ACC_PRODUCT.SingleOrDefault(x=>x.ID_ACC_PRODUCT == product) ==null)
            {
                return false;
            }
            return true;
        }
        public bool RemoveCart(string user, string product)
        {
            try
            {
                var res = db.WATCHED_PRODUCT.Where(x => x.USER_ACC == user).SingleOrDefault(x => x.ID_ACC_PRODUCT == product);
                db.WATCHED_PRODUCT.Attach(res);
                res.CART_COUNT = 0;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public int SumPriceVoucher(string user, string idvoucher)
        {
            /*
             * chuc nang nay de tra ve gia cho nguoi dung -> smart web
             */
            var allCart = db.WATCHED_PRODUCT.Where(x => x.USER_ACC == user && x.CART_COUNT > 0).ToList();
            int sumprice = 0;
            foreach(var i in allCart)
            {
                var pro = db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == i.ID_ACC_PRODUCT);

                sumprice -= MaxPriceFromVocher(idvoucher, i.ID_ACC_PRODUCT);
                sumprice+=Convert.ToInt32(i.CART_COUNT*pro.AMOUNT* (100 - pro.SALE_PERCENT) /100);
            }
            return sumprice;
        }
        public int MaxPriceFromVocher(string idV, string idP)
        {
            if (CheckAreaVocher(idV, idP))
            {
                var product = db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == idP);
                var vocher = db.VOCHERs.SingleOrDefault(x => x.ID_VOCHER == idV);
                var price =Convert.ToInt32(product.AMOUNT * (100-product.SALE_PERCENT)/100 * (100-vocher.PERCENT_VOCHER)/100);
                if (price >= vocher.MAX_SUM_PRICE)
                {
                    return (vocher.MAX_SUM_PRICE?? price);
                }
                else
                {
                    return price;
                }
            }
            else
            {

                return 0;
            }
        }
        public bool CheckCoin(string user, int price)
        {
            int coin = (db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user).COINT_ACC ?? 0);
            if (coin - price > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<string> AutoRenderOderFromUser(string user, string idvocher, string type)
        {
            List<WATCHED_PRODUCT> allCart = db.WATCHED_PRODUCT.Where(x => x.USER_ACC == user && x.CART_COUNT > 0).ToList();
            List<string> allIDOder = new List<string>();
            for(int i=0;i<allCart.Count();i++)
            {
                allIDOder.Add(InserOderByCustomer(allCart[i].ID_ACC_PRODUCT, user, type, 
                    (allCart[i].CART_COUNT ?? 0), idvocher));
                /*
                 * xong đơn phải reset nó về hết
                 */
                db.WATCHED_PRODUCT.Attach(allCart[i]);
                allCart[i].CART_COUNT = 0;
               
            }
            db.SaveChanges();
            return allIDOder;
        }

    }
}