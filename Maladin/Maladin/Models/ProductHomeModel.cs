using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Models
{
    public class ProductHomeModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string producer { get; set; }
        public int priceG { get; set; }
        public int saleP { get; set; }
        public string content { get; set; }
        public int totalComment { get; set; }
        public int totalRating { get; set; }
        public string ctv { get; set; }
    }
}