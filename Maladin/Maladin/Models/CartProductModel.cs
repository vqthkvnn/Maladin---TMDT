using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Models
{
    public class CartProductModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int saleP { get; set; }
        public int saleM { get; set; }
        public int TotalCount { get; set; }
        public string pathImg { get; set; }
        public string UserBy { get; set; }
    }
}