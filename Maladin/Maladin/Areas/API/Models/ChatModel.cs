using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.API.Models
{
    public class ChatModel
    {
        public int id { get; set; }
        public string content { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string avtPath { get; set; }
        public DateTime dateSend { get; set; }
        public int messNoneRead { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
    }
}