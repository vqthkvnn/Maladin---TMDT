using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.EF;
namespace Maladin.Areas.Partner.Models
{
    public class InfomationProduct
    {
        public PRODUCT PRODUCT { get; set; }
        public TYPE_PRODUCT TYPE_PRODUCT {get;set;}
        public ORIGIN ORIGIN { get; set; }
        public PRODUCT_IMAGE PRODUCT_IMAGE { get; set; }
        public PRODUCER_INFO PRODUCER_INFO { get; set; }




    }
}