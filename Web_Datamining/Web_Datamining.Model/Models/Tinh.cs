namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tinh")]
    public  class Tinh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaTinh { get; set; }

        [StringLength(50)]
        public string TenTinh { get; set; }

        [StringLength(20)]
        public string KhuVuc { get; set; }

        public virtual ICollection<Huyen> Huyen { get; set; }
    }
}
