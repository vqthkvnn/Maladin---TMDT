namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class APPROVED_USER_WAIT
    {
        [Key]
        public int ID_APPROVED_USER_WAIT { get; set; }

        public int ID_USER_WAIT { get; set; }

        [Required]
        [StringLength(30)]
        public string USER_ACC { get; set; }

        [Column(TypeName = "date")]
        public DateTime DATE_APPROVED { get; set; }

        public bool ACTION_APPROVED { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual USES_WAIT USES_WAIT { get; set; }
    }
}
