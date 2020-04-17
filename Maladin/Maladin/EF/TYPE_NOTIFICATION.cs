namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TYPE_NOTIFICATION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TYPE_NOTIFICATION()
        {
            NOTIFICATION_ = new HashSet<NOTIFICATION_>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_TYPE_NOTI { get; set; }

        [StringLength(255)]
        public string NAME_TYPE_NOTI { get; set; }

        [StringLength(255)]
        public string IMAGE_TYPE_NOTI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTIFICATION_> NOTIFICATION_ { get; set; }
    }
}
