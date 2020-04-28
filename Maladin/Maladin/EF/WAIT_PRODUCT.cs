namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WAIT_PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WAIT_PRODUCT()
        {
            APPROVED_PRODUCT_WAIT = new HashSet<APPROVED_PRODUCT_WAIT>();
        }

        [Key]
        public int ID_WAIT_PRODUCT { get; set; }

        [Required]
        [StringLength(255)]
        public string NAME_PRODUCT { get; set; }

        [Required]
        [StringLength(10)]
        public string ID_PRODUCER { get; set; }

        [Required]
        [StringLength(10)]
        public string ID_TYPE_PRODUCT { get; set; }

        [Required]
        public string DESCRIBE_PRODUCT { get; set; }

        [Required]
        [StringLength(10)]
        public string ID_ORIGIN { get; set; }

        public int PRICE_PRODUCT { get; set; }

        public int? RATING_PRODUCT { get; set; }

        [StringLength(255)]
        public string NOTE_PRODUCT { get; set; }

        [StringLength(255)]
        public string IMAGE_PRODUCT { get; set; }

        [Required]
        [StringLength(10)]
        public string ID_INFO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_PRODUCT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPROVED_PRODUCT_WAIT> APPROVED_PRODUCT_WAIT { get; set; }

        public virtual ORIGIN ORIGIN { get; set; }

        public virtual PRODUCER_INFO PRODUCER_INFO { get; set; }

        public virtual TYPE_PRODUCT TYPE_PRODUCT { get; set; }
    }
}
