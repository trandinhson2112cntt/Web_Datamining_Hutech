namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DSNguyenVong")]
    public class DSNguyenVong
    {
        [Key]
        [Column(Order = 0)]
        public int MaHoSo { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ThuTuNV { get; set; }

        [Required]
        [StringLength(5)]
        public string MaToHop { get; set; }

        [Required]
        [StringLength(50)]
        public string MaNganh { get; set; }

        [StringLength(10)]
        public string MaTDH { get; set; }
        
        public int DiemTong { get; set; }


        [StringLength(10)]
        public string TrangThaiNV { get; set; }

        public virtual HoSoXetTuyen HoSoXetTuyen { get; set; }

        public virtual NganhTheoBo NganhTheoBo { get; set; }

        public virtual ToHopMon ToHopMon { get; set; }
    }
}
