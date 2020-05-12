using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.API.Models
{
    public class MessageModel
    {
        public int IdMess { get; set; }
        public string FromID { get; set; }
        public string ToID { get; set; }
        public string Content { get; set; }
        //public string AVT { get; set; }
        public DateTime DateSend { get; set; }
        public bool IsRead { get; set; }
        //public string Type { get; set; }
    }
}