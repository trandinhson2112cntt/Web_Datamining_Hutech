namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Huyen")]
    public class Huyen
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHuyen { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaTinh { get; set; }

        [StringLength(20)]
        public string khuvuc { get; set; }

        [StringLength(50)]
        public string TenHuyen { get; set; }

        public virtual Tinh Tinh { get; set; }

        public virtual ICollection<TruongTHPT> TruongTHPT { get; set; }
    }
}
