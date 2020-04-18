using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.EF;
namespace Maladin.Areas.Partner.Models
{
    public class InfomationProductModel
    {
        
        public List<TYPE_PRODUCT> TYPE_PRODUCT {get;set;}
        public List<ORIGIN>  ORIGIN { get; set; }
        
        public List<PRODUCER_INFO>  PRODUCER_INFO { get; set; }




    }
}