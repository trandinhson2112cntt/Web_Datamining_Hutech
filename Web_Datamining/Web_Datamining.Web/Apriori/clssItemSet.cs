using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Datamining.Web
{
    public class clssItemSet : List<string>
    {
        /// <summary>
        /// Độ hỗ trợ của luật
        /// </summary>
        public double Support { get; set; }


        /// <summary>
        /// Kiểm tra 
        /// </summary>
        /// <param name="itemset"></param>
        /// <returns></returns>
        public bool Contains(clssItemSet itemset)
        {
            return (this.Intersect(itemset).Count() == itemset.Count);
        }

        public clssItemSet Remove(clssItemSet itemset)
        {
            clssItemSet removed = new clssItemSet();
            removed.AddRange(from item in this
                             where !itemset.Contains(item)
                             select item);
            return (removed);
        }

        public override string ToString()
        {
            return ("{" + string.Join(", ", this.ToArray()) + "}" + (this.Support > 0 ? " (support: " + Math.Round(this.Support, 2) + "%)" : string.Empty));
        }


    }
}
