namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Khoa")]
    public class Khoa
    {
        [Key]
        [StringLength(10)]
        public string MaKhoa { get; set; }

        [StringLength(50)]
        public string TenKhoa { get; set; }

        public virtual ICollection<ChuyenNganh> ChuyenNganh { get; set; }
    }
}
