namespace Maladin.Models.EF_MORE
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
            FAVORITE_PRODUCT = new HashSet<FAVORITE_PRODUCT>();
            FAVORITE_PRODUCT1 = new HashSet<FAVORITE_PRODUCT>();
        }

        [Key]
        [StringLength(30)]
        public string USER_ACC { get; set; }

        [Required]
        [StringLength(30)]
        public string PASSWORD_ACC { get; set; }

        [Required]
        [StringLength(255)]
        public string EMAIL_INFO { get; set; }

        [Required]
        [StringLength(10)]
        public string ID_TYPE_ACC { get; set; }

        public int? COINT_ACC { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_CREATE_ACC { get; set; }

        public bool? IS_ACTIVE_ACC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACC_PRODUCT> ACC_PRODUCT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAVORITE_PRODUCT> FAVORITE_PRODUCT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAVORITE_PRODUCT> FAVORITE_PRODUCT1 { get; set; }
    }
}
