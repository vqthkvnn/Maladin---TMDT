using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Models
{
    public class OrderDetailModel
    {
        public string IDProduct { get; set; }
        public DateTime DateOrder { get; set; }
        public int status { get; set; }
        public string IDOrder { get; set; }
        public string NameGuest { get; set; }
        public string AdressGuest { get; set; }
        public string PhoneGuest { get; set; }
        public string TypeOrder { get; set; }
        public string NameProduct { get; set; }
        public double SumPrice { get; set; }
        public int CountOrder { get; set; }
        public int SalePrice { get; set; }
        public string UserSell { get; set; }
        public string ImagePath { get; set; }
    }
}