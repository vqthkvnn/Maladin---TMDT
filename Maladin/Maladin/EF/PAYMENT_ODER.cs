namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PAYMENT_ODER
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_PAYMENT { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string ID_ODER { get; set; }

        [StringLength(255)]
        public string NOTE_PAYMENT_ODER { get; set; }

        public virtual ODER ODER { get; set; }

        public virtual PAYMENT PAYMENT { get; set; }
    }
}
