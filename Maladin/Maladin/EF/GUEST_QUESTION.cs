namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GUEST_QUESTION
    {
        [Key]
        public int ID_QUESTION { get; set; }

        [Required]
        [StringLength(10)]
        public string ID_ACC_PRODUCT { get; set; }

        [StringLength(255)]
        public string TITLE_QUESTION { get; set; }

        [Required]
        [StringLength(255)]
        public string CONTENT_QUESTION { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_QUESTION { get; set; }

        public virtual ACC_PRODUCT ACC_PRODUCT { get; set; }
    }
}
