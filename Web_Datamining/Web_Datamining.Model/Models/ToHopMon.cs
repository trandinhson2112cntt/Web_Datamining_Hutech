namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ToHopMon")]
    public class ToHopMon
    {
        [Key]
        [StringLength(5)]
        public string MaToHop { get; set; }

        [StringLength(20)]
        public string Mon1 { get; set; }

        [StringLength(20)]
        public string Mon2 { get; set; }

        [StringLength(20)]
        public string Mon3 { get; set; }

        public virtual ICollection<DSNguyenVong> DSNguyenVong { get; set; }
    }
}
