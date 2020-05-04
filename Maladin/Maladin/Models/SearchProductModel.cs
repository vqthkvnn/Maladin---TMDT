using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Models
{
    public class SearchProductModel
    {
        public string idLo { get; set; }
        public string idproduct { get; set; }
        public string  name{ get; set; }
        public int price { get; set; }
        public int salePerce { get; set; }
        public int saleMoney { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public int total { get; set; }
        public int sell { get; set; }
        public int rating { get; set; }

    }
}