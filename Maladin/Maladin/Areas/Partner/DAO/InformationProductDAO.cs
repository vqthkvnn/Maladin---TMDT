using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.EF;
namespace Maladin.Areas.Partner.DAO
{
    public class InformationProductDAO
    {
        TMDT_Maladin dbContext = null;
        public InformationProductDAO()
        {
            dbContext = new TMDT_Maladin();
        }
        public List<ORIGIN> GetAllOrigin()
        {
            return dbContext.ORIGINs.ToList<ORIGIN>();

        }
        public List<PRODUCER_INFO> GetAllProducer()
        {
            return dbContext.PRODUCER_INFO.ToList<PRODUCER_INFO>();

        }
        public List<TYPE_PRODUCT> GetAllTypePRoduct()
        {
            return dbContext.TYPE_PRODUCT.ToList<TYPE_PRODUCT>();

        }
        public INFOMATION_ACCOUNT getInfoByUser(string user)
        {
            return dbContext.INFOMATION_ACCOUNT.SingleOrDefault(x=>x.USER_ACC == user);
        }
        public bool Update(INFOMATION_ACCOUNT aCCOUNT)
        {
            try
            {
                //var dao = new AccountPartnerDAO();
                var acc = getInfoByUser(aCCOUNT.USER_ACC);
                dbContext.INFOMATION_ACCOUNT.Attach(acc);
                acc.NAME_INFO = aCCOUNT.NAME_INFO;
                acc.PHONE_INFO = aCCOUNT.PHONE_INFO;
                acc.ADRESS_INFO = aCCOUNT.ADRESS_INFO;
                
                acc.BIRTH_INFO = aCCOUNT.BIRTH_INFO;
                acc.NOTE_INFO = aCCOUNT.NOTE_INFO;
                acc.CMND_INFO = aCCOUNT.CMND_INFO;
                acc.SEX_INFO = aCCOUNT.SEX_INFO;
                acc.AVT_ACC = aCCOUNT.AVT_ACC;
                dbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}