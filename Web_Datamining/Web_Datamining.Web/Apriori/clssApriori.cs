using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Datamining.Web
{
    class clssApriori
    {
        public static ClssItemCollection DoApriori(ClssItemCollection db, double supportThreshold)
        {

            clssItemSet I = db.GetUniqueItems();
            ClssItemCollection L = new ClssItemCollection(); // tập dữ liệu phổ biến
            ClssItemCollection Li = new ClssItemCollection();//tập dữ liệu 
            ClssItemCollection Ci = new ClssItemCollection(); //tập dữ liệu được lược bớt

            //Duyệt sự lặp lại đầu tiên của phần tử trong tập dữ liệu
            foreach (string item in I)
            {
                Ci.Add(new clssItemSet() { item });
            }

            //Sự lặp lại các lần kế tiếp
            int k = 2;
            while (Ci.Count != 0)
            {
                //Lấy Li từ Ci (phần tử được lược bỏ)
                Li.Clear();
                foreach (clssItemSet itemset in Ci)
                {
                    itemset.Support = db.FindSupport(itemset);
                    if (itemset.Support >= supportThreshold)
                    {
                        Li.Add(itemset);
                        L.Add(itemset);
                    }
                }


                Ci.Clear();
                Ci.AddRange(clssBit.FindSubsets(Li.GetUniqueItems(), k));
                k += 1;
            }

            return (L);
        }

        public static List<ClssRules> Mine(ClssItemCollection db, ClssItemCollection L, double confidenceThreshold)
        {
            List<ClssRules> allRules = new List<ClssRules>();

            foreach (clssItemSet itemset in L)
            {
                ClssItemCollection subsets = clssBit.FindSubsets(itemset, 0);
                foreach (clssItemSet subset in subsets)
                {
                    double confidence = (db.FindSupport(itemset) / db.FindSupport(subset)) * 100.0;
                    if (confidence >= confidenceThreshold)
                    {
                        ClssRules rule = new ClssRules();
                        rule.X.AddRange(subset);
                        rule.Y.AddRange(itemset.Remove(subset));
                        rule.Support = db.FindSupport(itemset);
                        rule.Confidence = confidence;
                        if (rule.X.Count > 0 && rule.Y.Count > 0)
                        {
                            allRules.Add(rule);
                        }
                    }
                }
            }

            return (allRules);
        }
    }
}
