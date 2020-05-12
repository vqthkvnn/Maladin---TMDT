using Maladin.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.Admin.DAO
{
    public class ManagerOderDAO
    {
        /*
         * Quản lý liên quan đến đơn, payment, vocher
         */
        TMDT_Maladin db = null;
        public ManagerOderDAO()
        {
            db = new TMDT_Maladin();
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
                if (type == "TK")
                {
                    oDER.STATUS_ODER = -1; // cho thanh toan
                }
                else
                {
                    oDER.STATUS_ODER = 0; // da chuyen
                }
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
            if (!vocher.IS_STATUS)
            {
                return false;
            }
            var resAll = db.VOCHER_AREA.SingleOrDefault(x => x.ID_VOCHER == IDVocher);
            if (resAll == null)
            {
                return false; //
            }
            else
            {
                
                var date = (DateTime.Today - db.VOCHERs.SingleOrDefault(x => x.ID_VOCHER == IDVocher).DATE_END_VOCHER).Value.Days;
                if (date <= 0)
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
            catch (Exception e)
            {
                return "false";
            }
        }
        public int InsertGuest(INFOMATION_GUEST entity)
        {
            try
            {
                var count = db.INFOMATION_GUEST.Count();
                entity.ID_GUEST = count + 1;
                db.INFOMATION_GUEST.Add(entity);
                db.SaveChanges();

                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        public int Payment(string user, List<string> listoder)
        {
            /*
             * chuc nang nay chi co khi tai khoan 
             check kha nang thanh toan cua account -> đang trạng thái chờ đơn
             */
            var res = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);
            double totalPay = 0;
            for (int i = 0; i < listoder.Count; i++)
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
            if (res.COINT_ACC >= totalPay)
            {
                /*
                 * chap nhan thanh toan -> them thanh toan
                 */
                string insertPayment = "INSERT dbo.PAYMENT( USER_ACC ,SEND_COINT ,RECEIVE_COINT ,DATE_PAYMENT ,CONTENT_PAYMENT ,IS_CONFIRM)" +
                    "VALUES  ( '" + user + "', " + Convert.ToString(totalPay) + ",0,'" + DateTime.UtcNow.ToString("mm-dd-yyyy") +
                    "',N'Thanh toán hóa đơn cho các đơn hàng:', 1";
                db.Database.ExecuteSqlCommand(insertPayment);
                db.SaveChanges();
                /*
                 * Tru tai' khoan
                 */
                var acc = db.ACCOUNTs.SingleOrDefault(x => x.USER_ACC == user);
                db.ACCOUNTs.Attach(acc);
                acc.COINT_ACC -= (int)totalPay;
                db.SaveChanges();
                var last = db.PAYMENTs.LastOrDefault(x => x.USER_ACC == user);
                db.PAYMENTs.Attach(last);
                foreach (var i in listoder)
                {
                    PAYMENT_ODER pAYMENT_ODER = new PAYMENT_ODER();
                    pAYMENT_ODER.ID_PAYMENT = last.ID_PAYMENT;
                    pAYMENT_ODER.ID_ODER = i;

                    pAYMENT_ODER.NOTE_PAYMENT_ODER = "Mã giao dịch thanh toán là:" + Convert.ToString(last.ID_PAYMENT);
                    db.PAYMENT_ODER.Add(pAYMENT_ODER);
                    db.SaveChanges();
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
                    db.SaveChanges();

                }
                db.SaveChanges();
                return 1;
                /* thanh toan thanh cong*/
            }
            else
            {
                return -1; // thanh toan that bai, oder chuyen het ve trang thai -1 cho thanh toan
            }



        }
        public int CancelOder(string idoder)
        {
            try
            {
                var res = db.ODERs.SingleOrDefault(x => x.ID_ODER == idoder);
                if (res.STATUS_ODER>2)
                {
                    return -1; // đơn hàng k thể hủy
                }
                if (res ==null)
                {
                    return 0; // don k ton tai
                }
                else
                {
                    /*
                     * hủy đơn sẽ sảy ra 2 trường hợp nếu nó là Ship code thì oke xóa bt chuyển status về dạng -2; hủy đơn
                     * nếu là thanh toán phải refund cho người dùng đã thanh toán đơn rồi. oke xử lý
                     */
                     if (res.ID_TYPE_ODER == "TT")
                    {
                        db.ODERs.Attach(res);
                        res.STATUS_ODER = -2;
                        db.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        /*
                         * để giảm lỗi tối đa ta cần refund trước rồi mới đánh trạng thái, rồi trừ tiền từ phía đối tác bán
                         */
                        PAYMENT pAYMENT = new PAYMENT();
                        pAYMENT.USER_ACC = res.USER_ACC;
                        pAYMENT.SEND_COINT = 0;
                        pAYMENT.RECEIVE_COINT = (int)res.SUM_PRICE_ODER;
                        pAYMENT.CONTENT_PAYMENT = "Hoàn tiền cho đơn hàng '" + idoder + "' bị hủy";
                        pAYMENT.DATE_PAYMENT = DateTime.UtcNow;
                        pAYMENT.IS_CONFIRM = true;
                        db.SaveChanges();
                        /*
                         * trừ tiền của đối tác
                         */
                        PAYMENT aYMENT = new PAYMENT();
                        var product = db.ACC_PRODUCT.SingleOrDefault(x => x.ID_ACC_PRODUCT == res.ID_ACC_PRODUCT);
                        aYMENT.USER_ACC = product.USER_ACC;
                        pAYMENT.SEND_COINT = (int)res.SUM_PRICE_ODER;
                        pAYMENT.RECEIVE_COINT = 0;
                        pAYMENT.CONTENT_PAYMENT = "Trừ tiền do đơn hàng '" + idoder + "' bị hủy";
                        pAYMENT.DATE_PAYMENT = DateTime.UtcNow;
                        pAYMENT.IS_CONFIRM = true;
                        db.SaveChanges();
                        /*
                         * đánh trạng thái cho đơn.
                         */
                        db.ODERs.Attach(res);
                        res.STATUS_ODER = -2;
                        db.SaveChanges();
                        return 1;
                    }
                }
            }
            catch(Exception e)
            {
                return -2;
            }
        }
        public int NewVocher(VOCHER entity)
        {
            /*
             * vocher hay bất kì thứ gì của hệ thống đã triển khai thì k được xóa, chỉ đánh log về 
             */
            try
            {
                var res = db.VOCHERs.SingleOrDefault(x => x.ID_VOCHER == entity.ID_VOCHER);
                if (res == null)
                {
                    return 0; // vocher da ton tai
                }
                else
                {
                    db.VOCHERs.Add(entity);
                    db.SaveChanges();
                    return 1;
                }
            }
            catch(Exception e)
            {
                return -1;
            }
        }
        public int VocherArea(string idVocher, string idproduct, string accproduct)
        {
            try
            {
                VOCHER_AREA vOCHER_AREA = new VOCHER_AREA();
                vOCHER_AREA.ID_VOCHER = idVocher;
                if (idproduct == null)
                {
                    vOCHER_AREA.ID_ACC_PRODUCT = accproduct;

                }
                else if (accproduct == null)
                {
                    vOCHER_AREA.ID_PRODUCT = idproduct;
                }
                else
                {
                    vOCHER_AREA.ID_PRODUCT = idproduct;
                    vOCHER_AREA.ID_ACC_PRODUCT = accproduct;
                }
                return 1;
            }catch(Exception e)
            {
                return -1;
            }
        }
    }
}