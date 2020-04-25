using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.EF;
namespace Maladin.Areas.Partner.Models
{
    public class ProductModels
    {
        public List<PRODUCT> PRODUCT { get; set; }
        public int maxPage { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public int idPage { get; set; }
    }
}