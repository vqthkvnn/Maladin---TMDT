using Maladin.Areas.API.Models;
using Maladin.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.API.DAO
{
    public class PayDAO
    {
        TMDT_Maladin db = null;
        public PayDAO()
        {
            db = new TMDT_Maladin();
        }
        public bool InsertGuest(string s)
        {
            return true;
        }
        public Tuple<string, string , string, string> getInformation(string user)
        {
            var info = db.INFOMATION_ACCOUNT.SingleOrDefault(x => x.USER_ACC == user && x.ID_TYPE_ACC == "CT");
            var email = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user).EMAIL_INFO;
            return new Tuple<string, string, string, string>(info.NAME_INFO,  info.PHONE_INFO, info.ADRESS_INFO, email);
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
                string idOder = IDACC_PRO.Substring(0, 3) +product.ID_PRODUCT.Substring(0,2)+type+ Convert.ToString(leght + 1);
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
                    oDER.ID_VOCHER = IDVocher;
                    db.VOCHERs.Attach(vocher);
                    vocher.AMOUNT_VOCHER = vocher.AMOUNT_VOCHER - 1;
                    db.SaveChanges();
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
                    oDER.SUM_PRICE_ODER = count * (product.AMOUNT-product.SALE_PERCENT*product.AMOUNT/100) - MonneyFromVocher;
                }
                oDER.DATE_ODER = DateTime.Now;
                db.ODERs.Add(oDER);
                db.SaveChanges();

                return idOder;
            }
            catch (Exception e)
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
            if (vocher == null )
            {
                return false;
            }
            if (!vocher.IS_STATUS || vocher.AMOUNT_VOCHER <= 0)
            {
                return false;
            }
            var resAll = db.Database.SqlQuery<VOCHER_AREA>("SELECT * FROM dbo.VOCHER_AREA WHERE ID_VOCHER = '" + IDVocher + "'").ToList();
            if (resAll == null)
            {
                return false; //
            }
            else
            {
                
                var date = (DateTime.Today - db.VOCHERs.SingleOrDefault(x => x.ID_VOCHER == IDVocher).DATE_END_VOCHER).Value.Days;
                
                if (date <= 0)
                {
                    foreach (var i in resAll)
                    {
                        if (i.ID_ACC_PRODUCT == IDACC_PRO)
                        {
                            return true;
                        }
                        if (i.ID_ACC_PRODUCT == null
                            && i.ID_PRODUCT == db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == IDACC_PRO).ID_PRODUCT)
                        {

                            return true; // cai nay la vocher cho tat ca id product
                        }
                        
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
            catch (Exception e)
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
            catch (Exception e)
            {
                return -1;
            }
        }
        public List<string> AutoRenderOderFromUser(string user, string idvocher, string type)
        {
            List<WATCHED_PRODUCT> allCart = db.WATCHED_PRODUCT.Where(x => x.USER_ACC == user && x.CART_COUNT > 0).ToList();
            List<string> allIDOder = new List<string>();
            for (int i = 0; i < allCart.Count(); i++)
            {
                allIDOder.Add(InserOderByCustomer(allCart[i].ID_ACC_PRODUCT, user, type,
                    (allCart[i].CART_COUNT ?? 0), idvocher));
                /*
                 * xong đơn phải reset nó về hết
                 */
                db.WATCHED_PRODUCT.Attach(allCart[i]);
                allCart[i].CART_COUNT = 0;
                string idpi = allCart[i].ID_ACC_PRODUCT;
                var pro = db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == idpi);
                db.ACC_PRODUCT.Attach(pro);
                pro.SELL_COUNT =pro.SELL_COUNT+ 1;
                db.SaveChanges();

            }
            db.SaveChanges();
            return allIDOder;
        }
        public int Payment(string user, List<string> listoder)
        {
            /*
             * chuc nang nay chi co khi tai khoan
             * hệ thống kiểm tra khả thanh toán trước khi vào đây
             * tức là vào đây auto qua đơn
             */
            if (listoder.Count() == 0)
                return -1;
            var res = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);
            double totalPay = 0;
            foreach (var i in listoder)
            {
                var product = db.ODERs.SingleOrDefault(x => x.ID_ODER == i);
                if (product == null)
                {
                    totalPay += 0;
                }
                else
                {
                    totalPay += product.SUM_PRICE_ODER;
                }
            }
            if (res.COINT_ACC >= totalPay)
            {
                /*
                 * chap nhan thanh toan -> them thanh toan
                 */

                PAYMENT aYMENT = new PAYMENT();
                aYMENT.USER_ACC = user;
                aYMENT.SEND_COINT = (int)totalPay;
                aYMENT.DATE_PAYMENT = DateTime.Now;
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
            catch (Exception e)
            {
                return false;
            }

        }
        public bool IsProduct(string product)
        {
            if (db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == product) == null)
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
            foreach (var i in allCart)
            {
                var pro = db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == i.ID_ACC_PRODUCT);

                sumprice -= MaxPriceFromVocher(idvoucher, i.ID_ACC_PRODUCT);
                sumprice += Convert.ToInt32(i.CART_COUNT * pro.AMOUNT * (100 - pro.SALE_PERCENT) / 100);
            }
            return sumprice;
        }
        public int MaxPriceFromVocher(string idV, string idP)
        {
            if (CheckAreaVocher(idV, idP))
            {
                var product = db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == idP);
                var vocher = db.VOCHERs.SingleOrDefault(x => x.ID_VOCHER == idV);
                var price = Convert.ToInt32(product.AMOUNT * (100 - product.SALE_PERCENT) / 100 * (100 - vocher.PERCENT_VOCHER) / 100);
                if (price >= vocher.MAX_SUM_PRICE)
                {
                    return (vocher.MAX_SUM_PRICE ?? price);
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
            if (coin - price >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public OderDetailModel getInfoByIDO(string ido, string user)
        {
            string sql = "SELECT OD.ID_ODER as IDO,OD.DATE_ODER as DateOder, OD.STATUS_ODER as status, OD.ID_TYPE_ODER as IDType, NAME_INFO as NameOder, " +
                "PHONE_INFO as Phone, ADRESS_INFO as Adrs,OD.SUM_PRICE_ODER as Sumprice, OD.COUNT_ODER as count," +
                "(SELECT TOP 1 IMAGE_PATH FROM dbo.ACC_PRODUCT, dbo.PRODUCT, dbo.PRODUCT_IMAGE " +
                "WHERE ACC_PRODUCT.ID_PRODUCT = PRODUCT.ID_PRODUCT AND PRODUCT.ID_PRODUCT=PRODUCT_IMAGE.ID_PRODUCT AND OD.ID_ACC_PRODUCT = ID_ACC_PRODUCT) AS ImagePath," +
                "(SELECT NAME_PRODUCT FROM dbo.ACC_PRODUCT, dbo.PRODUCT WHERE ACC_PRODUCT.ID_PRODUCT = PRODUCT.ID_PRODUCT AND ID_ACC_PRODUCT = OD.ID_ACC_PRODUCT) AS NameProduct " +
                "FROM dbo.ODER AS OD, dbo.INFOMATION_ACCOUNT " +
                "WHERE INFOMATION_ACCOUNT.USER_ACC = OD.USER_ACC " +
                "AND OD.ID_ODER='" + ido +
                "' AND ID_TYPE_ACC ='CT' and OD.USER_ACC = '"+user+"'";
            var data = db.Database.SqlQuery<OderDetailModel>(sql)
                .Select(b => new OderDetailModel {
                Adrs = b.Adrs,
                count = b.count,
                DateOder = b.DateOder,
                IDO = b.IDO,
                IDType = b.IDType,
                NameOder = b.NameOder,
                ImagePath = b.ImagePath,
                NameProduct = b.NameProduct,
                Phone = b.Phone,
                status = b.status,
                Sumprice = b.Sumprice,
                }).SingleOrDefault();
            return data;
        }
        public bool CancelOder(string ido, string user)
        {
            var res = db.ODERs.SingleOrDefault(x => x.ID_ODER == ido && x.USER_ACC == user);
            if(res.STATUS_ODER>=3)
            {
                return false;
            }
            else
            {
                db.ODERs.Attach(res);
                res.STATUS_ODER = -1;
                db.SaveChanges();
                return true;
            }
        }

    }
}