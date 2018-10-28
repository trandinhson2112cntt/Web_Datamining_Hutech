using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Datamining.Web.Models
{
    public class MonHocViewModel
    {
        public string MaMon { get; set; }

        public string MaKhoa { get; set; }

        public string TenMon { get; set; }

        public bool? TichLuy { get; set; }

        public double? DiemDat { get; set; }
    }
}