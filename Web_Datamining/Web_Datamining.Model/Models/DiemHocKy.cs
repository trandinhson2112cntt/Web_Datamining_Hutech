namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiemHocKy")]
    public class DiemHocKy
    {
        public int? SoTCDK { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string MSSV { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ID_HocKi { get; set; }

        public int? SoTCD { get; set; }

        public int? SoTCTL { get; set; }

        public double? DiemTBTLHe4 { get; set; }

        public virtual ICollection<DiemCTHKy> DiemCTHKy { get; set; }

        public virtual HocKy HocKy { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
