using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.Admin.Models
{
    public class WaitProductModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NameProducer { get; set; }
        public string NameOrigin { get; set; }
        public string NameType { get; set; }
        public int Price { get; set; }
        public string NameUserSend { get; set; }
        public DateTime DateSend { get; set; }
    }
}