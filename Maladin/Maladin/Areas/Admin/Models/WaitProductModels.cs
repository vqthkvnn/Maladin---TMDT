using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Areas.Admin.Models
{
    public class WaitProductModels
    {
        public List<WaitProductModel> ListProduct { get; set; }
        public int maxPage { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public int idPage { get; set; }
    }
}
