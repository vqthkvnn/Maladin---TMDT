using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.EF;

namespace Maladin.Areas.Partner.DAO
{
    public class WaitProductDAO
    {
        TMDT_Maladin dbContex = null;
        public WaitProductDAO()
        {
            dbContex = new TMDT_Maladin();
        }
        public bool Insert(WAIT_PRODUCT entity)
        {
            try
            {
                dbContex.WAIT_PRODUCT.Add(entity);
                dbContex.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool InsertByCode(WAIT_PRODUCT entity)
        {
            try
            {
                string sql = "INSERT dbo.WAIT_PRODUCT( NAME_PRODUCT ,ID_PRODUCER ,ID_TYPE_PRODUCT ,DESCRIBE_PRODUCT ,ID_ORIGIN ," +
                    "PRICE_PRODUCT ,RATING_PRODUCT ,NOTE_PRODUCT ,ID_INFO ,DATE_PRODUCT) " +
                    "VALUES  ( N'"+entity.NAME_PRODUCT+"','"+entity.ID_PRODUCER+"','"+entity.ID_TYPE_PRODUCT+"',N'"+
                    entity.DESCRIBE_PRODUCT+"','"+entity.ID_ORIGIN+"',"+Convert.ToString(entity.PRICE_PRODUCT)+", 5,N'"+
                    entity.NOTE_PRODUCT+"','"+entity.ID_INFO+"', '"+entity.DATE_PRODUCT+"')";
                dbContex.Database.ExecuteSqlCommand(sql);
                dbContex.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}