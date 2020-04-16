namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ACCOUNT")]
    public partial class ACCOUNT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACCOUNT()
        {
            ACC_PRODUCT = new HashSet<ACC_PRODUCT>();
            ACCOUNT_COMMENT = new HashSet<ACCOUNT_COMMENT>();
            APPROVED_PRODUCT_WAIT = new HashSet<APPROVED_PRODUCT_WAIT>();
            APPROVED_USER_WAIT = new HashSet<APPROVED_USER_WAIT>();
            INFOMATION_ACCOUNT = new HashSet<INFOMATION_ACCOUNT>();
            INFOMATION_GUEST = new HashSet<INFOMATION_GUEST>();
            NOTI_ACC = new HashSet<NOTI_ACC>();
            ODERs = new HashSet<ODER>();
            USES_WAIT = new HashSet<USES_WAIT>();
            USES_WAIT1 = new HashSet<USES_WAIT>();
        }

        [Key]
        [StringLength(30)]
        public string USER_ACC { get; set; }

        [Required]
        [StringLength(30)]
        public string PASSWORD_ACC { get; set; }

        [Required]
        [StringLength(10)]
        public string ID_TYPE_ACC { get; set; }

        public int? COINT_ACC { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_CREATE_ACC { get; set; }

        public bool? IS_ACTIVE_ACC { get; set; }

        public bool HAS_CTV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACC_PRODUCT> ACC_PRODUCT { get; set; }

        public virtual TYPE_ACCOUNT TYPE_ACCOUNT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCOUNT_COMMENT> ACCOUNT_COMMENT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPROVED_PRODUCT_WAIT> APPROVED_PRODUCT_WAIT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPROVED_USER_WAIT> APPROVED_USER_WAIT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INFOMATION_ACCOUNT> INFOMATION_ACCOUNT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INFOMATION_GUEST> INFOMATION_GUEST { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTI_ACC> NOTI_ACC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ODER> ODERs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USES_WAIT> USES_WAIT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USES_WAIT> USES_WAIT1 { get; set; }
    }
}