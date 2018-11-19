using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Datamining.Web.Models
{
    public class LuatViewModel
    {
        public int Id { get; set; }

        public string X { get; set; }

        public string Y { get; set; }

        public decimal? Support { get; set; }

        public decimal? Confidence { get; set; }
    }
}

