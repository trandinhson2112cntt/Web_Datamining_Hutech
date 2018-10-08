namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NganhTheoBo")]
    public class NganhTheoBo
    {
        [Key]
        [StringLength(50)]
        public string MaNganh { get; set; }

        [StringLength(50)]
        public string TeNganh { get; set; }

        public virtual ICollection<DSNguyenVong> DSNguyenVong { get; set; }
    }
}
