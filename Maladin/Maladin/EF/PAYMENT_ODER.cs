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
        public int ID_PAYMENT { get; set; }

        [Required]
        [StringLength(30)]
        public string USER_ACC { get; set; }

        [Required]
        [StringLength(10)]
        public string ID_ODER { get; set; }

        public int SEND_COINT { get; set; }

        public int RECEIVE_COINT { get; set; }

        public bool? IS_CONFIRM { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual ODER ODER { get; set; }
    }
}
