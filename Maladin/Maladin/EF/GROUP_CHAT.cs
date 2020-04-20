namespace Maladin.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GROUP_CHAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GROUP_CHAT()
        {
            MEMBER_GROUP_CHAT = new HashSet<MEMBER_GROUP_CHAT>();
            MESSAGE_SEND_TO_GR = new HashSet<MESSAGE_SEND_TO_GR>();
        }

        [Key]
        public int ID_GROUP { get; set; }

        [StringLength(255)]
        public string NAME_GR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEMBER_GROUP_CHAT> MEMBER_GROUP_CHAT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MESSAGE_SEND_TO_GR> MESSAGE_SEND_TO_GR { get; set; }
    }
}
