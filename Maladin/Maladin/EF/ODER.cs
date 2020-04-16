namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ODER")]
    public partial class ODER
    {
        [Key]
        [StringLength(10)]
        public string ID_ODER { get; set; }

        [StringLength(30)]
        public string USER_ACC { get; set; }

        public int? ID_GUEST_NO_ACC { get; set; }

        [Required]
        [StringLength(10)]
        public string ID_ACC_PRODUCT { get; set; }

        public int? STATUS_ODER { get; set; }

        [Required]
        [StringLength(10)]
        public string ID_TYPE_ODER { get; set; }

        public int? COUNT_ODER { get; set; }

        [StringLength(10)]
        public string ID_VOCHER { get; set; }

        public double SUM_PRICE_ODER { get; set; }

        [Column(TypeName = "date")]
        public DateTime DATE_ODER { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_COMPLATE_ODER { get; set; }

        public virtual ACC_PRODUCT ACC_PRODUCT { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual INFOMATION_GUEST INFOMATION_GUEST { get; set; }

        public virtual TYPE_ODER TYPE_ODER { get; set; }

        public virtual VOCHER VOCHER { get; set; }
    }
}
