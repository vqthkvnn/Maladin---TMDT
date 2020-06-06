using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.Partner.Models
{
    public class OderModel
    {
        public string IDO { get; set; }
        public string IDAP { get; set; }
        public string AccountO { get; set; }
        public string GuestO { get; set; }
        public int CountO { get; set; }
        public double SumPriceO  { get; set; }
        public string TypeO { get; set; }
        public DateTime DateO { get; set; }
        public DateTime? DateEO { get; set; }
        public int StatusO { get; set; }
    }
}