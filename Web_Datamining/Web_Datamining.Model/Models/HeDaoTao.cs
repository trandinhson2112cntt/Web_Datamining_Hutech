namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HeDaoTao")]
    public class HeDaoTao
    {
        [Key]
        [StringLength(10)]
        public string MaHeDaoTao { get; set; }

        [StringLength(50)]
        public string TenHeDaoTao { get; set; }

        public virtual ICollection<Lop> Lop { get; set; }
    }
}
