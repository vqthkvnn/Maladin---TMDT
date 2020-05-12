namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class INFOMATION_GUEST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public INFOMATION_GUEST()
        {
            ODERs = new HashSet<ODER>();
        }

        [Key]
        public int ID_GUEST { get; set; }

        [StringLength(255)]
        public string NAME_GUEST { get; set; }

        [Required]
        [StringLength(11)]
        public string PHONE_GUEST { get; set; }

        [Required]
        [StringLength(255)]
        public string ADRESS_GUEST { get; set; }

        public bool? SEX_GUEST { get; set; }

        [StringLength(255)]
        public string EMAIL_GUEST { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ODER> ODERs { get; set; }
    }
}
