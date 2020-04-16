namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class USES_WAIT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USES_WAIT()
        {
            APPROVED_USER_WAIT = new HashSet<APPROVED_USER_WAIT>();
        }

        [Key]
        public int ID_USER_WAIT { get; set; }

        [Required]
        [StringLength(30)]
        public string USER_ACC { get; set; }

        [Required]
        [StringLength(30)]
        public string USER_ACC_WANT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_WANT { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual ACCOUNT ACCOUNT1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPROVED_USER_WAIT> APPROVED_USER_WAIT { get; set; }
    }
}
