namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MonHoc")]
    public class MonHoc
    {
        [Key]
        [StringLength(10)]
        public string MaMon { get; set; }

        public string MaKhoa { get; set; }
        [ForeignKey("MaKhoa")]
        public virtual Khoa Khoa { get; set; }
        [StringLength(50)]
        public string TenMon { get; set; }

        public bool? TichLuy { get; set; }

        public double? DiemDat { get; set; }

        public virtual ICollection<DiemCTHKy> DiemCTHKy { get; set; }
    }
}
