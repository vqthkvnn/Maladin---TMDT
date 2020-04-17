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
    }
}