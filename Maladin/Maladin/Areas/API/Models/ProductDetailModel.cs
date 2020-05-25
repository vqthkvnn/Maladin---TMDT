using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.API.Models
{
    public class ProductDetailModel
    {
        
        public string ID { get; set; }
        public string NameCTV { get; set; }
        public string name { get; set; }
        public int priceGoc { get; set; }
        public int saleP { get; set; }
        public int totalComment { get; set; }
        public int ratting { get; set; }
        public string contentProduct { get; set; }
    }
}