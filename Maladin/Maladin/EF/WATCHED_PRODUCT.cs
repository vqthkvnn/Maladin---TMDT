namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WATCHED_PRODUCT
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string USER_ACC { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string ID_ACC_PRODUCT { get; set; }

        public int? CART_COUNT { get; set; }

        public virtual ACC_PRODUCT ACC_PRODUCT { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }
    }
}
