using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.API.Models
{
    public class AccountModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string idType { get; set; }
        public int coint { get; set; }
        public DateTime dateCreate { get; set; }
        public string avt { get; set; }
        public string cmnd { get; set; }
        public DateTime birth { get; set; }
        public bool gt { get; set; }
        public string adr { get; set; }
        public string phone { get; set; }
        public string note { get; set; }
    }
}