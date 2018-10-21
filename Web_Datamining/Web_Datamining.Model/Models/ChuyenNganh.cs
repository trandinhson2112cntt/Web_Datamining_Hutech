namespace Web_Datamining.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ChuyenNganh")]
    public class ChuyenNganh
    {
        [Key]
        [StringLength(10)]
        public string MaChuyenNganh { get; set; }

        [StringLength(50)]
        public string TenChuyenNganh { get; set; }

        [Required]
        [StringLength(10)]
        public string MaKhoa { get; set; }

        public virtual Khoa Khoa { get; set; }

        public virtual ICollection<Lop> Lop { get; set; }
    }
}