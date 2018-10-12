﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Datamining.Web
{
    public class ClssItemCollection : List<clssItemSet>
    {

        public clssItemSet GetUniqueItems()
        {
            clssItemSet unique = new clssItemSet();

            foreach (clssItemSet itemset in this)
            {
                unique.AddRange(from item in itemset
                                where !unique.Contains(item)
                                select item);
            }

            return (unique);
        }

        // ham tinh toan do pho bien
        public double FindSupport(string item)
        {
            int matchCount = (from itemset in this
                              where itemset.Contains(item)
                              select itemset).Count();

            double support = ((double)matchCount / (double)this.Count) * 100.0;
            return (support);
        }

        public double FindSupport(clssItemSet itemset)
        {
            int matchCount = (from i in this
                              where i.Contains(itemset)
                              select i).Count();

            double support = ((double)matchCount / (double)this.Count) * 100.0;
            return (support);
        }

        public override string ToString()
        {
            return (string.Join("\r\n", (from itemset in this select itemset.ToString()).ToArray()));
        }


    }
}
