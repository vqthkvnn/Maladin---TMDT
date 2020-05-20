namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MESSAGE_SEND_TO
    {
        [Key]
        public int ID_MESSAGE { get; set; }

        [Required]
        public string CONTEN_MESSAGE { get; set; }

        [Required]
        [StringLength(30)]
        public string FROM_ACC { get; set; }

        [Required]
        [StringLength(30)]
        public string TO_ACC { get; set; }

        public bool? IS_READ { get; set; }

        public DateTime? DATA_SEND_MESSAGE { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual ACCOUNT ACCOUNT1 { get; set; }
    }
}
