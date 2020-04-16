namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TYPE_PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TYPE_PRODUCT()
        {
            PRODUCTs = new HashSet<PRODUCT>();
            WAIT_PRODUCT = new HashSet<WAIT_PRODUCT>();
        }

        [Key]
        [StringLength(10)]
        public string ID_TYPE_PRODUCT { get; set; }

        [Required]
        [StringLength(255)]
        public string NAME_TYPE_PRODUCT { get; set; }

        [StringLength(255)]
        public string NOTE_TYPE_PRODUCT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT> PRODUCTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WAIT_PRODUCT> WAIT_PRODUCT { get; set; }
    }
}
