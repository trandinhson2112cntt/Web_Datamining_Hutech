namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiemCTHKy")]
    public class DiemCTHKy
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaMon { get; set; }

        public double? DiemTH { get; set; }

        public double? DiemQT { get; set; }

        public double? DiemThi1 { get; set; }

        public double? DiemThi2 { get; set; }

        public double? TiLeDiemTH { get; set; }

        public double? TiLeDiemQT { get; set; }

        public double? TiLeDiemThi1 { get; set; }

        public double? TiLeDiemThi2 { get; set; }

        public double? DiemTKHe10 { get; set; }

        public double? DiemTKHe4 { get; set; }

        [StringLength(50)]
        public string DiemTKChu { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string MSSV { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ID_HocKi { get; set; }

        public virtual DiemHocKy DiemHocKy { get; set; }

        public virtual MonHoc MonHoc { get; set; }
    }
}
