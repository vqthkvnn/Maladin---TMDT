using Maladin.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.Partner.DAO
{
    public class ProductDao
    {
        TMDT_Maladin dbContext = null;
        public ProductDao()
        {
            dbContext = new TMDT_Maladin();
        }
        public bool Insert(PRODUCT entity)
        {
            try
            {
                dbContext.PRODUCTs.Add(entity);
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public PRODUCT getByName(string id)
        {
            return dbContext.PRODUCTs.SingleOrDefault(x => x.ID_PRODUCT == id);
        }
        public bool Delete(string id)
        {
            try
            {
                var product = getByName(id);
                dbContext.PRODUCTs.Attach(product);
                product.IS_SELL = false;
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<PRODUCT> getAllProduct()
        {
            return dbContext.PRODUCTs.ToList<PRODUCT>();
        }
        public int GetCount()
        {
            return dbContext.PRODUCTs.Count();
        }
        public List<PRODUCT> GetListProductByPage(int pageSize, int page)
        {
            int count = GetCount();
            int size = count - pageSize * (page-1);
            if (size >=0)
            {

                if (size>=page)
                {
                    string sql = "SELECT  RowConstrainedResult.ID_PRODUCT, RowConstrainedResult.NAME_PRODUCT, RowConstrainedResult.ID_PRODUCER, RowConstrainedResult.ID_TYPE_PRODUCT, " +
                " RowConstrainedResult.DESCRIBE_PRODUCT, RowConstrainedResult.ID_ORIGIN, RowConstrainedResult.PRICE_PRODUCT, RowConstrainedResult.RATING_PRODUCT, " +
                " RowConstrainedResult.NOTE_PRODUCT,RowConstrainedResult.ID_INFO, RowConstrainedResult.DATE_PRODUCT, RowConstrainedResult.USER_ACC, RowConstrainedResult.IS_SELL " +
                " FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY DATE_PRODUCT ) AS RowNum, *FROM dbo.PRODUCT ) AS RowConstrainedResult" +
                " WHERE   RowNum > " + Convert.ToString(pageSize * (page - 1)) + " AND RowNum <= " + Convert.ToString(page * pageSize) + " ORDER BY RowNum";
                    var data = dbContext.Database.SqlQuery<PRODUCT>(sql)
                        .Select(b => new PRODUCT { 
                        ID_PRODUCER = b.ID_PRODUCER,
                        NAME_PRODUCT = b.NAME_PRODUCT,
                        ID_PRODUCT = b.ID_PRODUCT,
                        ID_TYPE_PRODUCT = b.ID_TYPE_PRODUCT,
                        DESCRIBE_PRODUCT = b.DESCRIBE_PRODUCT,
                        ID_ORIGIN = b.ID_ORIGIN,
                        PRICE_PRODUCT =Convert.ToInt32(b.PRICE_PRODUCT),
                        RATING_PRODUCT = Convert.ToInt32(b.RATING_PRODUCT),
                        NOTE_PRODUCT = b.NOTE_PRODUCT,
                        ID_INFO = Convert.ToString(b.ID_INFO),
                        DATE_PRODUCT = b.DATE_PRODUCT,
                        USER_ACC = b.USER_ACC,
                        IS_SELL = b.IS_SELL



                        }).ToList();
                    return data;
                }
                else
                {
                    string sql = "SELECT  RowConstrainedResult.ID_PRODUCT, RowConstrainedResult.NAME_PRODUCT, RowConstrainedResult.ID_PRODUCER, RowConstrainedResult.ID_TYPE_PRODUCT, " +
                "RowConstrainedResult.DESCRIBE_PRODUCT, RowConstrainedResult.ID_ORIGIN, RowConstrainedResult.PRICE_PRODUCT, RowConstrainedResult.RATING_PRODUCT, " +
                " RowConstrainedResult.NOTE_PRODUCT,RowConstrainedResult.ID_INFO, RowConstrainedResult.DATE_PRODUCT, RowConstrainedResult.USER_ACC, RowConstrainedResult.IS_SELL " +
                " FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY DATE_PRODUCT ) AS RowNum, *FROM dbo.PRODUCT ) AS RowConstrainedResult" +
                " WHERE   RowNum > " + Convert.ToString(pageSize * (page - 1)) + " AND RowNum <= " + Convert.ToString(count) + " ORDER BY RowNum";
                    var data = dbContext.Database.SqlQuery<PRODUCT>(sql)
                        .Select(b => new PRODUCT
                        {
                            ID_PRODUCER = b.ID_PRODUCER,
                            NAME_PRODUCT = b.NAME_PRODUCT,
                            ID_PRODUCT = b.ID_PRODUCT,
                            ID_TYPE_PRODUCT = b.ID_TYPE_PRODUCT,
                            DESCRIBE_PRODUCT = b.DESCRIBE_PRODUCT,
                            ID_ORIGIN = b.ID_ORIGIN,
                            PRICE_PRODUCT = Convert.ToInt32(b.PRICE_PRODUCT),
                            RATING_PRODUCT = Convert.ToInt32(b.RATING_PRODUCT),
                            NOTE_PRODUCT = b.NOTE_PRODUCT,
                            ID_INFO = Convert.ToString(b.ID_INFO),
                            DATE_PRODUCT = b.DATE_PRODUCT,
                            USER_ACC = b.USER_ACC,
                            IS_SELL = b.IS_SELL



                        }).ToList();
                    return data;
                }
                
            }
            else
            {
                return getAllProduct();
            }
            
            
            
        }
    }
}