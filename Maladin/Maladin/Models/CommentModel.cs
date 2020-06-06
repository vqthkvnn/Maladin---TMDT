using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Models
{
    public class CommentModel
    {
        public Maladin.EF.ACCOUNT_COMMENT commment { get; set; }
        public string Name { get; set; }
        public string avt { get; set; }
        public bool isSale { get; set;}
       
    }
}