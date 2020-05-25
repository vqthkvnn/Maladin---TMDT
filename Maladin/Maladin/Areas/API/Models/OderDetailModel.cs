using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.API.Models
{
    public class OderDetailModel
    {
        public string IDO { get; set; }
        public DateTime DateOder { get; set; }
        public int status { get; set; }
        public string IDType { get; set; }
        public string NameOder { get; set; }
        public string Phone { get; set; }
        public string Adrs { get; set; }
        public double Sumprice { get; set; }
        public int count { get; set; }
        public string ImagePath { get; set; }
        public string NameProduct { get; set; }
    }
}