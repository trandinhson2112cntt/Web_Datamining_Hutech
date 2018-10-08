namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhoaHoc")]
    public class KhoaHoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaKhoaHoc { get; set; }

        [StringLength(10)]
        public string NamHoc { get; set; }

        public virtual ICollection<Lop> Lop { get; set; }
    }
}
