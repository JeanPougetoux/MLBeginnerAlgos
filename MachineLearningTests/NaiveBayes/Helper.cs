using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MachineLearningTests.NaiveBayes
{
    internal static class Helper
    {
        internal static Dictionary<string, int> GetPossibilitiesOfColumn(object[] datas)
        {
            Dictionary<string, int> classCounts = new Dictionary<string, int>();

            string[] singleDatas = datas.Select(td => td.ToString()).Distinct().ToArray();

            Array.ForEach(singleDatas, (element) =>
            {
                classCounts.Add(element, datas.Where(d => d.ToString() == element).Count());
            });

            return classCounts;
        }

        internal static Dictionary<string, int> GetPossibilitiesOfColumn(int column, object[][] datas)
        {
            return GetPossibilitiesOfColumn(datas.Select(d => d[column].ToString()).ToArray());
        }
    }
}
