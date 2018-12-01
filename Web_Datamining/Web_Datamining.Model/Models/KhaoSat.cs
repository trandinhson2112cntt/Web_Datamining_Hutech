using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Datamining.Model.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("KhaoSat")]
    public class KhaoSat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(15)]
        [Required]
        public string CMND { get; set; }

        [StringLength(5)]
        public string Khoi { get; set; }

        public decimal? DiemMon1 { get; set; }

        public decimal? DiemMon2{ get; set; }

        public decimal? DiemMon3 { get; set; }
    }
}
