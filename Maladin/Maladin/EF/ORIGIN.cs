namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ORIGIN")]
    public partial class ORIGIN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORIGIN()
        {
            PRODUCTs = new HashSet<PRODUCT>();
            WAIT_PRODUCT = new HashSet<WAIT_PRODUCT>();
        }

        [Key]
        [StringLength(10)]
        public string ID_ORIGIN { get; set; }

        [Required]
        [StringLength(255)]
        public string NAME_ORIGIN { get; set; }

        [StringLength(255)]
        public string NOTE_ORIGIN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT> PRODUCTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WAIT_PRODUCT> WAIT_PRODUCT { get; set; }
    }
}
