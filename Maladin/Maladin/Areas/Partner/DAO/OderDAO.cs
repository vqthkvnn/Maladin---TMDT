using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.EF;
using Maladin.Areas.Partner.Models;
namespace Maladin.Areas.Partner.DAO
{
    public class OderDAO
    {
        TMDT_Maladin dbContext = null;
        public OderDAO()
        {
            dbContext = new TMDT_Maladin();
        }
        public bool Insert(ODER oDER)
        {
            try
            {
                dbContext.ODERs.Add(oDER);
                dbContext.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
        public ODER findByID(string id)
        {
            return dbContext.ODERs.SingleOrDefault(x => x.ID_ODER == id);
        }
        public bool Update(ODER oDER)
        {
            try {
                var od = findByID(oDER.ID_ODER);
                dbContext.ODERs.Attach(od);
                od.STATUS_ODER = oDER.STATUS_ODER;
                od.COUNT_ODER = oDER.COUNT_ODER;
                od.DATE_COMPLATE_ODER = oDER.DATE_COMPLATE_ODER;
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception e) { return false; }
        }
        public List<OderModel> getAllOder(string user)
        {
            string sql = "SELECT ID_ODER as id, NAME_PRODUCT as name, COUNT_ODER as countOder, ID_TYPE_ODER as typeOder, DATE_ODER as dateOder, " +
                "SUM_PRICE_ODER as sumPrice, STATUS_ODER as st, ACC_PRODUCT.ID_ACC_PRODUCT as idAccProduct " +
                "FROM dbo.ODER, dbo.ACC_PRODUCT, dbo.PRODUCT WHERE ACC_PRODUCT.ID_ACC_PRODUCT = ODER.ID_ACC_PRODUCT " +
                "AND ACC_PRODUCT.ID_PRODUCT = PRODUCT.ID_PRODUCT AND ACC_PRODUCT.USER_ACC ='"+user+"'";
            var data = dbContext.Database.SqlQuery<OderModel>(sql)
                .Select(b => new OderModel
                { 
                id = b.id,
                name = b.name,
                countOder = b.countOder,
                typeOder = b.typeOder,
                dateOder = b.dateOder,
                sumPrice = b.sumPrice,
                st = b.st,
                idAccProduct = b.idAccProduct
                }).ToList();
            return data;
        }
        public List<TYPE_ODER> getAllTypeOder()
        {
            return dbContext.TYPE_ODER.ToList();
        }
    }
}