using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Models
{
    public class OrderModel
    {
        public string ID { get; set; }
        public DateTime DateOrder { get; set; }
        public string TypeOrder { get; set; }
        public double SumPrice { get; set; }
        public int StatusOrder { get; set; }
    }
}