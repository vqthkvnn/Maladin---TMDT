namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VOCHER")]
    public partial class VOCHER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VOCHER()
        {
            ODERs = new HashSet<ODER>();
        }

        [Key]
        [StringLength(10)]
        public string ID_VOCHER { get; set; }

        [StringLength(255)]
        public string CONENT_VOCHER { get; set; }

        public int PERCENT_VOCHER { get; set; }

        public int MONEY_VOCHER { get; set; }

        public int? MAX_SUM_PRICE { get; set; }

        [Column(TypeName = "date")]
        public DateTime DATE_START_VOCHER { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_END_VOCHER { get; set; }

        public bool IS_STATUS { get; set; }

        public int? AMOUNT_VOCHER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ODER> ODERs { get; set; }

        public virtual VOCHER_AREA VOCHER_AREA { get; set; }
    }
}
