namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GROUP_TYPE_PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GROUP_TYPE_PRODUCT()
        {
            TYPE_PRODUCT = new HashSet<TYPE_PRODUCT>();
        }

        [Key]
        [StringLength(10)]
        public string ID_GR_TYPE_PR { get; set; }

        [StringLength(255)]
        public string NAME_GROUP { get; set; }

        public bool? IS_ACTIVE { get; set; }

        [StringLength(255)]
        public string NOTE_GR_TYPE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TYPE_PRODUCT> TYPE_PRODUCT { get; set; }
    }
}
