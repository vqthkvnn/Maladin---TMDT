using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.Partner.Models
{
    public class OderModel
    {
        public string id { get; set; }
        public string idAccProduct { get; set; }
        public string name { get; set; }
        public int countOder { get; set; }
        public string typeOder { get; set; }
        public DateTime dateOder { get; set; }
        public double sumPrice { get; set; }
        public int st { get; set; }
    }
}