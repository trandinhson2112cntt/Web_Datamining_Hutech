namespace Web_Datamining.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HocKy")]
    public class HocKy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_HocKi { get; set; }

        [StringLength(10)]
        public string NamHoc { get; set; }

        public int? KyHoc { get; set; }

        public virtual ICollection<DiemHocKy> DiemHocKy { get; set; }
    }
}
