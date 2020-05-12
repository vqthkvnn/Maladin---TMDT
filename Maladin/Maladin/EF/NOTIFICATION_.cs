namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NOTIFICATION_
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NOTIFICATION_()
        {
            NOTI_ACC = new HashSet<NOTI_ACC>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_NOTI { get; set; }

        [StringLength(255)]
        public string CONTENT_NOTI { get; set; }

        public int? ID_TYPE_NOTI { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_NOTI { get; set; }

        [StringLength(255)]
        public string TITLE_NOTI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTI_ACC> NOTI_ACC { get; set; }

        public virtual TYPE_NOTIFICATION TYPE_NOTIFICATION { get; set; }
    }
}
