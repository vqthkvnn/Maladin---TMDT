namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRODUCER_INFO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCER_INFO()
        {
            PRODUCTs = new HashSet<PRODUCT>();
            WAIT_PRODUCT = new HashSet<WAIT_PRODUCT>();
        }

        [Key]
        [StringLength(10)]
        public string ID_PRODUCER { get; set; }

        [Required]
        [StringLength(255)]
        public string NAME_PRODUCER { get; set; }

        [StringLength(15)]
        public string PHONE_PRODUCER { get; set; }

        [Required]
        [StringLength(255)]
        public string EMAIL_PRODUCER { get; set; }

        [Required]
        [StringLength(255)]
        public string ADRESS_PRODUCER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT> PRODUCTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WAIT_PRODUCT> WAIT_PRODUCT { get; set; }
    }
}
