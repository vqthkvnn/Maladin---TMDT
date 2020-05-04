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
        public int InserOderByCustomer(string IDACC_PRO, string User, string type, int count, string IDVocher)
        {
            return 1;
        }
        public bool CheckAreaVocher(string IDVocher, string IDACC_PRO)
        {
            /*
             * check xem vocher con han k
             */

            /*check xem vocher co duoc kick hoat cho lo hang kia k*/
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
        public int InserOderByGuest()
        {
            return 1;
        }
        public int Payment(string user, List<string> listoder)
        {
            /*
             * chuc nang nay chi co khi tai khoan 
             check kha nang thanh toan cua account -> đang trạng thái chờ đơn
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
                PAYMENT pAYMENT = new PAYMENT();
                pAYMENT.USER_ACC = user;
                pAYMENT.SEND_COINT = (int)totalPay;
                pAYMENT.DATE_PAYMENT = DateTime.Today;
                pAYMENT.CONTENT_PAYMENT = "Thanh toán cho các đơn:";
                pAYMENT.IS_CONFIRM = true;
                db.PAYMENTs.Add(pAYMENT);
                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.PAYMENT ON;");
                db.SaveChanges();
                db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.PAYMENT ON;");
                foreach (var i in listoder)
                {
                    

                }
            }
            else
            {
                return -1; // thanh toan that bai, oder chuyen het ve trang thai -1 cho thanh toan
            }

            return 1;
            
        }
    }
}