using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.API.Models
{
    public class OderModelItem
    {
        public string IDOder { get; set; }
        public string NameProduct { get; set; }
        public DateTime DateOder { get; set; }
        public int StatusOder { get; set; }
    }
}