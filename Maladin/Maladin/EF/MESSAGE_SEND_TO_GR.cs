namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MESSAGE_SEND_TO_GR
    {
        [Key]
        public int ID_MESS_GR { get; set; }

        [Required]
        [StringLength(30)]
        public string USER_ACC { get; set; }

        public int ID_GROUP { get; set; }

        [Required]
        public string CONTENT_MESS { get; set; }

        [Column(TypeName = "date")]
        public DateTime DATE_SEND { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual GROUP_CHAT GROUP_CHAT { get; set; }
    }
}
