using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Datamining.Web
{
    public class ClssRules : List<string>
    {
        /// <summary>
        /// Lấy hoặc gán tập phần tử X
        /// </summary>
        public clssItemSet X { get; set; }
        /// <summary>
        /// Lấy hoặc gán tập phần tử Y
        /// </summary>
        public clssItemSet Y { get; set; }
        /// <summary>
        /// Lấy hoặc gán Độ hỗ trợ của luật
        /// </summary>
        public double Support { get; set; }
        /// <summary>
        /// Lấy hoặc gán Độ tin cậy của luật
        /// </summary>
        public double Confidence { get; set; }

        /// <summary>
        /// Khởi tạo luật kết hợp
        /// </summary>
        public ClssRules()
        {
            X = new clssItemSet();
            Y = new clssItemSet();
            Support = 0.0;
            Confidence = 0.0;
        }


        public override string ToString()
        {
            return (X + " => " + Y + " (support: " + Math.Round(Support, 2) + "%, confidence: " + Math.Round(Confidence, 2) + "%)");
        }

    }
}
