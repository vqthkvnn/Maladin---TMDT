namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VOCHER_AREA
    {
        [Key]
        [StringLength(10)]
        public string ID_VOCHER { get; set; }

        [StringLength(10)]
        public string ID_PRODUCT { get; set; }

        [StringLength(10)]
        public string ID_ACC_PRODUCT { get; set; }

        public virtual ACC_PRODUCT ACC_PRODUCT { get; set; }

        public virtual PRODUCT PRODUCT { get; set; }

        public virtual VOCHER VOCHER { get; set; }
    }
}
