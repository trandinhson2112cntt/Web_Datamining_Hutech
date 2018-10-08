namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SinhVien")]
    public class SinhVien
    {
        [Key]
        [StringLength(15)]
        public string MSSV { get; set; }

        public int MaHoSo { get; set; }

        [Column(TypeName = "ntext")]
        public string CoVanHocTap { get; set; }

        public int ID_Lop { get; set; }

        public virtual ICollection<DiemHocKy> DiemHocKy { get; set; }

        public virtual HoSoXetTuyen HoSoXetTuyen { get; set; }

        public virtual Lop Lop { get; set; }
    }
}
