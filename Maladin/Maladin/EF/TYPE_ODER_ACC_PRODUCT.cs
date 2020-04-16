namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TYPE_ODER_ACC_PRODUCT
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string ID_ACC_PRODUCT { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string ID_TYPE_ODER { get; set; }

        [StringLength(255)]
        public string NOTE { get; set; }

        public virtual ACC_PRODUCT ACC_PRODUCT { get; set; }

        public virtual TYPE_ODER TYPE_ODER { get; set; }
    }
}
