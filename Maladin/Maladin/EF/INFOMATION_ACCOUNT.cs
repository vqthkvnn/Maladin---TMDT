namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class INFOMATION_ACCOUNT
    {
        [Key]
        [StringLength(10)]
        public string ID_INFO { get; set; }

        [Required]
        [StringLength(12)]
        public string CMND_INFO { get; set; }

        [Required]
        [StringLength(255)]
        public string NAME_INFO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BIRTH_INFO { get; set; }

        public bool? SEX_INFO { get; set; }

        [StringLength(255)]
        public string ADRESS_INFO { get; set; }

        [StringLength(255)]
        public string PHONE_INFO { get; set; }

        [StringLength(255)]
        public string NOTE_INFO { get; set; }

        [StringLength(30)]
        public string USER_ACC { get; set; }
        [StringLength(10)]
        public string ID_TYPE_ACC { get; set; }
        [StringLength(255)]
        public string AVT_ACC { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }
        public virtual TYPE_ACCOUNT TYPE_ACCOUNT { get; set; }
    }
}
