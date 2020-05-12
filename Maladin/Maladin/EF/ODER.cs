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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ODER()
        {
            PAYMENT_ODER = new HashSet<PAYMENT_ODER>();
        }

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

        [StringLength(255)]
        public string NOTE_ODER { get; set; }

        public virtual ACC_PRODUCT ACC_PRODUCT { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual INFOMATION_GUEST INFOMATION_GUEST { get; set; }

        public virtual TYPE_ODER TYPE_ODER { get; set; }

        public virtual VOCHER VOCHER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAYMENT_ODER> PAYMENT_ODER { get; set; }
    }
}
