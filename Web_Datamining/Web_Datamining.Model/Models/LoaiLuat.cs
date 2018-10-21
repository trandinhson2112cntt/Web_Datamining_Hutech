namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiLuat")]
    public class LoaiLuat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LuatId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Luat> Luats { get; set; }
    }
}
