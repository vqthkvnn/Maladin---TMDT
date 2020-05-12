namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRODUCT_ATT
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string ID_PRODUCT { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string KEY_ATT { get; set; }

        [StringLength(255)]
        public string VALUE_ATT { get; set; }

        public virtual PRODUCT PRODUCT { get; set; }
    }
}
