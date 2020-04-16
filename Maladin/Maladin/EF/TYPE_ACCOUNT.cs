namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TYPE_ACCOUNT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TYPE_ACCOUNT()
        {
            ACCOUNTs = new HashSet<ACCOUNT>();
            INFOMATION_GUEST = new HashSet<INFOMATION_GUEST>();
        }

        [Key]
        [StringLength(10)]
        public string ID_TYPE_ACC { get; set; }

        [StringLength(255)]
        public string NAME_TYPE_ACC { get; set; }

        public bool? IS_USE_TYPE_ACC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCOUNT> ACCOUNTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INFOMATION_GUEST> INFOMATION_GUEST { get; set; }
    }
}
