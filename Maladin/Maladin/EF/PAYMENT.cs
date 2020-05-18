namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PAYMENT")]
    public partial class PAYMENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PAYMENT()
        {
            PAYMENT_ODER = new HashSet<PAYMENT_ODER>();
        }

        [Key]
        public int ID_PAYMENT { get; set; }

        [Required]
        [StringLength(30)]
        public string USER_ACC { get; set; }

        public int SEND_COINT { get; set; }

        public int RECEIVE_COINT { get; set; }

        [StringLength(255)]
        public string CONTENT_PAYMENT { get; set; }

        public bool? IS_CONFIRM { get; set; }

        public DateTime? DATE_PAYMENT { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAYMENT_ODER> PAYMENT_ODER { get; set; }
    }
}
