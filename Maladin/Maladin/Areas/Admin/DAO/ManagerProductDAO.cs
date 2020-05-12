using Maladin.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.Admin.DAO
{
    public class ManagerProductDAO
    {
        /*
         * các chức năng của việc quản lý sản phẩm, lô hàng, công ty,...
         */
        TMDT_Maladin db = null;
        public ManagerProductDAO()
        {
            db = new TMDT_Maladin();
        }
        public int NewAccProduct(string idproduct, int price, int saleP, int saleM, DateTime start, DateTime end,
            int total, string user)
        {
            /*
             * chức năng này viết cho cả phía admin, partner
             */
            try
            {
                ACC_PRODUCT aCC_PRODUCT = new ACC_PRODUCT();
                var product = db.PRODUCTs.SingleOrDefault(x => x.ID_PRODUCT == idproduct);
                string idaccproduct = product.ID_PRODUCT.Substring(0, 3)
                    + Convert.ToString(db.ACC_PRODUCT.Count(x => x.ID_PRODUCT == idproduct));
                aCC_PRODUCT.ID_PRODUCT = idproduct;
                aCC_PRODUCT.AMOUNT = price;
                aCC_PRODUCT.USER_ACC = user;
                aCC_PRODUCT.SALE_MONEY = saleM;
                aCC_PRODUCT.SALE_PERCENT = saleP;
                aCC_PRODUCT.DATE_START_SELL = start;
                if (end != null)
                {
                    aCC_PRODUCT.DATE_END_SELL = end;
                }

                aCC_PRODUCT.TOTAL_COUNT = total;
                db.ACC_PRODUCT.Add(
                aCC_PRODUCT);
                db.SaveChanges();
                return 1;
            }
            catch(Exception e)
            {
                return - 1;
            }

        }
        public int NewProducer(PRODUCER_INFO entity)
        {
            try
            {
                db.PRODUCER_INFO.Add(entity);
                db.SaveChanges();
                return 1;
            }catch(Exception e)
            {
                return -1;
            }
        }
        public int NewTypeProduct(TYPE_PRODUCT entity)
        {
            try
            {
                db.TYPE_PRODUCT.Add(entity);
                db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        public int NewGrProduct(GROUP_TYPE_PRODUCT entity)
        {
            try
            {
                db.GROUP_TYPE_PRODUCT.Add(entity);
                db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        public int Insert(PRODUCT entity)
        {
            try
            {
                db.PRODUCTs.Add(entity);
                db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        
    }
}