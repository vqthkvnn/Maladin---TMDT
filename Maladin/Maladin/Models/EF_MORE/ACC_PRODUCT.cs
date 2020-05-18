namespace Maladin.Models.EF_MORE
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
            FAVORITE_PRODUCT = new HashSet<FAVORITE_PRODUCT>();
            FAVORITE_PRODUCT1 = new HashSet<FAVORITE_PRODUCT>();
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

        public int? TOTAL_COUNT { get; set; }

        public int? SELL_COUNT { get; set; }

        public bool IS_SELL { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAVORITE_PRODUCT> FAVORITE_PRODUCT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAVORITE_PRODUCT> FAVORITE_PRODUCT1 { get; set; }
    }
}
