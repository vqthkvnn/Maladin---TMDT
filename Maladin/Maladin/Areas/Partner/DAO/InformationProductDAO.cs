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
    }
}