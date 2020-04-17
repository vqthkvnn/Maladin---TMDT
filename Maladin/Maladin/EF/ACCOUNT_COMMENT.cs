namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ACCOUNT_COMMENT
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string USER_ACC { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string ID_ACC_PRODUCT { get; set; }

        [StringLength(255)]
        public string TITLE_COMMENT { get; set; }

        [Required]
        [StringLength(255)]
        public string CONTEN_COMMENT { get; set; }

        public int? RATING_COMMENT { get; set; }

        [Column(TypeName = "date")]
        public DateTime DATE_COMMENT { get; set; }

        public virtual ACC_PRODUCT ACC_PRODUCT { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }
    }
}
