namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MEMBER_GROUP_CHAT
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_GROUP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string USER_ACC { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DATE_INV { get; set; }

        public bool? IS_ADMIN_GROUP { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual GROUP_CHAT GROUP_CHAT { get; set; }
    }
}
