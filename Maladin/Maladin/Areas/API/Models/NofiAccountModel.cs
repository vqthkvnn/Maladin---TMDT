using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.API.Models
{
    public class NofiAccountModel
    {
        public int id { get; set; }
        public string titel { get; set; }
        public string content { get; set; }
        public DateTime dateTime { get; set; }
        public string ImagePath { get; set; }
        public bool isRead { get; set; }
    }
}