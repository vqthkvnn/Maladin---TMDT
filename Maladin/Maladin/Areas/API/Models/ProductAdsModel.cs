using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.API.Models
{
    public class ProductAdsModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int saleP { get; set; }
        public string imagePath { get; set; }
    }
}