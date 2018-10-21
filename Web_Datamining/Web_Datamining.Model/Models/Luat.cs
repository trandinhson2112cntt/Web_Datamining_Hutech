namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Luat")]
    public class Luat
    {
        [Key]        
        public int Id { get; set; }

        [StringLength(100)]
        public string X { get; set; }

        [StringLength(100)]
        public string Y { get; set; }

        public decimal? Support { get; set; }

        public decimal? Confidence { get; set; }

        public int? LuatId { get; set; }

        [ForeignKey("LuatId")]
        public virtual LoaiLuat LoaiLuat { get; set; }

    }
}
