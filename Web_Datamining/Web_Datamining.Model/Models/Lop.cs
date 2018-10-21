namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Lop")]
    public class Lop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_Lop { get; set; }

        [Required]
        [StringLength(10)]
        public string MaChuyenNganh { get; set; }

        [Required]
        [StringLength(10)]
        public string MaHeDaoTao { get; set; }

        public int MaKhoaHoc { get; set; }

        [StringLength(10)]
        public string TenLop { get; set; }

        public string MaKhoa { get; set; }
        [ForeignKey("MaKhoa")]
        public virtual Khoa Khoa { get; set; }

        public virtual ChuyenNganh ChuyenNganh { get; set; }

        public virtual HeDaoTao HeDaoTao { get; set; }

        public virtual KhoaHoc KhoaHoc { get; set; }

        public virtual ICollection<SinhVien> SinhVien { get; set; }
    }
}
