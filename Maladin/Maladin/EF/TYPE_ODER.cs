namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TYPE_ODER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TYPE_ODER()
        {
            ODERs = new HashSet<ODER>();
            TYPE_ODER_ACC_PRODUCT = new HashSet<TYPE_ODER_ACC_PRODUCT>();
        }

        [Key]
        [StringLength(10)]
        public string ID_TYPE_ODER { get; set; }

        [Required]
        [StringLength(255)]
        public string NAME_ODER { get; set; }

        [StringLength(255)]
        public string NOTE_ODER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ODER> ODERs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TYPE_ODER_ACC_PRODUCT> TYPE_ODER_ACC_PRODUCT { get; set; }
    }
}
