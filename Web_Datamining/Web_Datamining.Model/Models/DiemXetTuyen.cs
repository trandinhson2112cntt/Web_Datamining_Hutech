namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiemXetTuyen")]
    public class DiemXetTuyen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DXT_ID { get; set; }

        public double? DiemToan { get; set; }

        public double? DiemVan { get; set; }

        public double? DiemLy { get; set; }

        public double? DiemHoa { get; set; }

        public double? DiemSinh { get; set; }

        public double? DiemDia { get; set; }

        public double? DiemGDCD { get; set; }

        public double? DiemNN { get; set; }

        public bool? HinhThucXetTuyen { get; set; }

        public virtual ICollection<HoSoXetTuyen> HoSoXetTuyen { get; set; }
    }
}
