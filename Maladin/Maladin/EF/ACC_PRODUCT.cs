namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ACC_PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACC_PRODUCT()
        {
            ACCOUNT_COMMENT = new HashSet<ACCOUNT_COMMENT>();
            GUEST_QUESTION = new HashSet<GUEST_QUESTION>();
            ODERs = new HashSet<ODER>();
            TYPE_ODER_ACC_PRODUCT = new HashSet<TYPE_ODER_ACC_PRODUCT>();
            VOCHER_AREA = new HashSet<VOCHER_AREA>();
            WATCHED_PRODUCT = new HashSet<WATCHED_PRODUCT>();
        }

        [Key]
        [StringLength(10)]
        public string ID_ACC_PRODUCT { get; set; }

        [Required]
        [StringLength(30)]
        public string USER_ACC { get; set; }

        [Required]
        [StringLength(10)]
        public string ID_PRODUCT { get; set; }

        public int AMOUNT { get; set; }

        public int SALE_PERCENT { get; set; }

        public int SALE_MONEY { get; set; }

        [Column(TypeName = "date")]
        public DateTime DATE_START_SELL { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_END_SELL { get; set; }

        public bool IS_SELL { get; set; }

        public virtual PRODUCT PRODUCT { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCOUNT_COMMENT> ACCOUNT_COMMENT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GUEST_QUESTION> GUEST_QUESTION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ODER> ODERs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TYPE_ODER_ACC_PRODUCT> TYPE_ODER_ACC_PRODUCT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VOCHER_AREA> VOCHER_AREA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WATCHED_PRODUCT> WATCHED_PRODUCT { get; set; }
    }
}
