using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.API.Models
{
    public class ProductModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int salePercent { get; set; }
        public int rating { get; set; }
        public int totalComment { get; set; }
        public string imagePath { get; set; }
    }
}