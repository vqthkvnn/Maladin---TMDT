using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maladin.EF;

namespace Maladin.Areas.Partner.Models
{
    public class OderModels
    {
        public List<OderModel> oders { get; set; }
        public List<TYPE_ODER> listTypeOder { get; set; }
    }
}