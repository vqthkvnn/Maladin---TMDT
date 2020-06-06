using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
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
        public IPagedList<OderModel> getListOder(string user, int page, int option)
        {
            string sql = "";
            if (option >=0 && option<=3)
            {
                sql = "SELECT ID_ODER as IDO,ODER.ID_ACC_PRODUCT as IDAP, ODER.USER_ACC as AccountO, ID_GUEST_NO_ACC as GuestO,COUNT_ODER as CountO, " +
                "SUM_PRICE_ODER as SumPriceO, ID_TYPE_ODER as TypeO, DATE_ODER as DateO, DATE_COMPLATE_ODER as DateEO, STATUS_ODER as StatusO " +
                "FROM dbo.ODER, dbo.ACC_PRODUCT WHERE ACC_PRODUCT.ID_ACC_PRODUCT = ODER.ID_ACC_PRODUCT " +
                "AND ACC_PRODUCT.USER_ACC = '" + user + "' and STATUS_ODER>=0 and STATUS_ODER<=3 ";
            }
            else if (option ==5)
            {
                sql = "SELECT ID_ODER as IDO,ODER.ID_ACC_PRODUCT as IDAP, ODER.USER_ACC as AccountO, ID_GUEST_NO_ACC as GuestO,COUNT_ODER as CountO, " +
                "SUM_PRICE_ODER as SumPriceO, ID_TYPE_ODER as TypeO, DATE_ODER as DateO, DATE_COMPLATE_ODER as DateEO, STATUS_ODER as StatusO " +
                "FROM dbo.ODER, dbo.ACC_PRODUCT WHERE ACC_PRODUCT.ID_ACC_PRODUCT = ODER.ID_ACC_PRODUCT " +
                "AND ACC_PRODUCT.USER_ACC = '" + user + "'";
            }
            else
            {
                sql = "SELECT ID_ODER as IDO,ODER.ID_ACC_PRODUCT as IDAP, ODER.USER_ACC as AccountO, ID_GUEST_NO_ACC as GuestO,COUNT_ODER as CountO, " +
                "SUM_PRICE_ODER as SumPriceO, ID_TYPE_ODER as TypeO, DATE_ODER as DateO, DATE_COMPLATE_ODER as DateEO, STATUS_ODER as StatusO " +
                "FROM dbo.ODER, dbo.ACC_PRODUCT WHERE ACC_PRODUCT.ID_ACC_PRODUCT = ODER.ID_ACC_PRODUCT " +
                "AND ACC_PRODUCT.USER_ACC = '" + user + "' and STATUS_ODER = "+Convert.ToString(option);
            }
            
            var data = dbContext.Database.SqlQuery<OderModel>(sql)
                .Select(b => new OderModel {
                AccountO = b.AccountO,
                CountO = b.CountO,
                DateEO = (DateTime?)b.DateEO,
                DateO = b.DateO,
                GuestO = b.GuestO,
                IDAP = b.IDAP,
                IDO = b.IDO,
                StatusO = b.StatusO,
                SumPriceO =b.SumPriceO,
                TypeO = b.TypeO
                }).ToList().ToPagedList(page, 10);
            return data;
        }
        public List<TYPE_ODER> getAllTypeOder()
        {
            return dbContext.TYPE_ODER.ToList();
        }
        public int MaxPageOrder(string user)
        {
            return dbContext.ODERs.Count()/10;
        }
    }
}