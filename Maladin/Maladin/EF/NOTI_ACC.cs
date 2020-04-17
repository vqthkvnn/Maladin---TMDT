namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NOTI_ACC
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string USER_ACC { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string ID_NOTI { get; set; }

        public bool? IS_CHECK_NOTI { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual NOTIFICATION_ NOTIFICATION_ { get; set; }
    }
}
