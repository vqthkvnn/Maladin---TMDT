namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PRODUCT")]
    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            ACC_PRODUCT = new HashSet<ACC_PRODUCT>();
            PRODUCT_IMAGE = new HashSet<PRODUCT_IMAGE>();
            VOCHER_AREA = new HashSet<VOCHER_AREA>();
            ACCOUNTs = new HashSet<ACCOUNT>();
        }

        [Key]
        [StringLength(10)]
        public string ID_PRODUCT { get; set; }

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

        [Required]
        [StringLength(10)]
        public string ID_INFO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_PRODUCT { get; set; }

        [StringLength(30)]
        public string USER_ACC { get; set; }

        public bool IS_SELL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACC_PRODUCT> ACC_PRODUCT { get; set; }

        public virtual ORIGIN ORIGIN { get; set; }

        public virtual PRODUCER_INFO PRODUCER_INFO { get; set; }

        public virtual TYPE_PRODUCT TYPE_PRODUCT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT_IMAGE> PRODUCT_IMAGE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VOCHER_AREA> VOCHER_AREA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCOUNT> ACCOUNTs { get; set; }
    }
}
