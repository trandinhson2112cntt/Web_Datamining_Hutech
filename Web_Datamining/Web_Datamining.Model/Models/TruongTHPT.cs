namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TruongTHPT")]
    public class TruongTHPT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaTruongTHPT { get; set; }

        public int MaHuyen { get; set; }

        public int MaTinh { get; set; }

        [StringLength(50)]
        public string TenTruong { get; set; }

        public virtual ICollection<HoSoXetTuyen> HoSoXetTuyen { get; set; }

        public virtual Huyen Huyen { get; set; }
    }
}
