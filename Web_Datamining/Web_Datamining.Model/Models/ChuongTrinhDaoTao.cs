using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Datamining.Models;

namespace Web_Datamining.Models
{
    [Table("ChuongTrinhDaoTao")]
    public class ChuongTrinhDaoTao
    {
        [Key]
        [Column(Order = 0)]
        public string MaKhoa { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ID_HocKi { get; set; }
        [Key]
        [Column(Order = 2)]
        public string MaMon { get; set; }
        [ForeignKey("MaKhoa")]
        public virtual Khoa Khoa { get; set; }
        [ForeignKey("ID_HocKi")]
        public virtual HocKy HocKy { get; set; }
        [ForeignKey("MaMon")]
        public virtual MonHoc MonHoc { get; set; }
    }
}
