namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRODUCT_IMAGE
    {
        [Required]
        [StringLength(10)]
        public string ID_PRODUCT { get; set; }

        [Key]
        [StringLength(255)]
        public string IMAGE_PATH { get; set; }

        public virtual PRODUCT PRODUCT { get; set; }
    }
}
