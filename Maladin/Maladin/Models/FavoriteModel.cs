﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maladin.Models
{
    public class FavoriteModel
    {
        public string ID { get; set; }
        public string NameProduct { get; set; }
        public int PriceGoc { get; set; }
        public int SalePricent { get; set; }
        public int SaleMoney { get; set; }
        public string ContentP { get; set; }
        public string PathIamge { get; set; }
        
    }
}