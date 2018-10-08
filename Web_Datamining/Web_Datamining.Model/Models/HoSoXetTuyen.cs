namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoSoXetTuyen")]
    public class HoSoXetTuyen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHoSo { get; set; }

        public int MaTruongTHPT { get; set; }

        [StringLength(15)]
        public string CMDN { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        public int? GioiTinh { get; set; }

        [StringLength(30)]
        public string DanToc { get; set; }

        public int? TinhTrangTrungTuyen { get; set; }

        public int DXT_ID { get; set; }

        public virtual DiemXetTuyen DiemXetTuyen { get; set; }

        public virtual ICollection<DSNguyenVong> DSNguyenVong { get; set; }

        public virtual TruongTHPT TruongTHPT { get; set; }

        public virtual ICollection<SinhVien> SinhVien { get; set; }
    }
}
