using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.API.Models
{
    public class ShopModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public int numProduct { get; set; }
        public DateTime dateJoin { get; set; }
        public int totalRating { get; set; }
        public string imagePath { get; set; }
    }
}