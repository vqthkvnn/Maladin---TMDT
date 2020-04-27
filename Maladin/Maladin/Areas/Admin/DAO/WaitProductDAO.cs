using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.Areas.Admin.Models;
using Maladin.EF;

namespace Maladin.Areas.Admin.DAO
{
    public class WaitProductDAO
    {
        TMDT_Maladin db = null;
        public WaitProductDAO()
        {
            db = new TMDT_Maladin();
        }
        public List<WaitProductModel> getAll()
        {
            string sql = "SELECT ID_WAIT_PRODUCT AS ID, NAME_PRODUCT as Name, NAME_PRODUCER as NameProducer, NAME_ORIGIN as NameOrigin, " +
                "NAME_TYPE_PRODUCT as NameType, PRICE_PRODUCT as Price, ID_INFO as NameUserSend, DATE_PRODUCT as DateSend " +
                "FROM dbo.WAIT_PRODUCT, dbo.TYPE_PRODUCT, dbo.PRODUCER_INFO, dbo.ORIGIN " +
                "WHERE ORIGIN.ID_ORIGIN = WAIT_PRODUCT.ID_ORIGIN AND PRODUCER_INFO.ID_PRODUCER = WAIT_PRODUCT.ID_PRODUCER AND " +
                "TYPE_PRODUCT.ID_TYPE_PRODUCT = WAIT_PRODUCT.ID_TYPE_PRODUCT";
            var data = db.Database.SqlQuery<WaitProductModel>(sql)
                .Select(b => new WaitProductModel { 
                ID = b.ID,
                Name = b.Name,
                NameProducer = b.NameProducer,
                NameOrigin = b.NameOrigin,
                NameType = b.NameType,
                Price = (int)b.Price,
                NameUserSend = b.NameUserSend,
                DateSend = b.DateSend
                
                }).ToList();
            return data;
        }
        public bool Delete(WAIT_PRODUCT entity)
        {
            try
            {
                db.WAIT_PRODUCT.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool Delete(string id)
        {
            try
            {
                
                var entity = findByID(Convert.ToInt32(id));
                db.WAIT_PRODUCT.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var entity = findByID(id);
                db.WAIT_PRODUCT.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public WAIT_PRODUCT findByID(string id)
        {
            int i = Convert.ToInt32(id);

            return db.WAIT_PRODUCT.SingleOrDefault(x => x.ID_WAIT_PRODUCT == i);
        }
        public WAIT_PRODUCT findByID(int id)
        {

            return db.WAIT_PRODUCT.SingleOrDefault(x => x.ID_WAIT_PRODUCT == id);
        }
        public bool Accept(string id,string admin)
        {
            try
            {
                var wait = findByID(id);
                PRODUCT entity = new PRODUCT();
                string idProduct = wait.ID_TYPE_PRODUCT.Substring(0,2)+wait.ID_PRODUCER.Substring(0,2)+wait.ID_ORIGIN+
                    Convert.ToString(getCountByType(wait.ID_TYPE_PRODUCT)+1);
                entity.ID_PRODUCT = idProduct;
                entity.NAME_PRODUCT = wait.NAME_PRODUCT;
                entity.ID_PRODUCER = wait.ID_PRODUCER;
                entity.ID_TYPE_PRODUCT = wait.ID_TYPE_PRODUCT;
                entity.ID_ORIGIN = wait.ID_ORIGIN;
                entity.DESCRIBE_PRODUCT = wait.DESCRIBE_PRODUCT;
                entity.PRICE_PRODUCT = wait.PRICE_PRODUCT;
                entity.RATING_PRODUCT = wait.RATING_PRODUCT;
                entity.NOTE_PRODUCT = wait.NOTE_PRODUCT;
                entity.ID_INFO = wait.ID_INFO;
                entity.DATE_PRODUCT = wait.DATE_PRODUCT;
                entity.IS_SELL = true;
                entity.USER_ACC = admin;
                db.PRODUCTs.Add(entity);
                db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }
        public int getCountByType(string id)
        {
            return db.PRODUCTs.Count(x => x.ID_TYPE_PRODUCT == id);
        }
        
    }
}