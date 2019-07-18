using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MachineLearningTests.NaiveBayes
{
    internal class Feature
    {
        internal Dictionary<string, FeaturePossibility> Possibilities;
        internal string Title;

        internal Feature(string title)
        {
            Title = title;
            Possibilities = new Dictionary<string, FeaturePossibility>();
        }

        internal void BuildDictionary(object[][] arrayPossibilities, int columnIndex, int labelColumnIndex)
        {
            var possibilities = Helper.GetPossibilitiesOfColumn(columnIndex, arrayPossibilities);
            var labelPossibilities = Helper.GetPossibilitiesOfColumn(labelColumnIndex, arrayPossibilities);
            int total = possibilities.Sum(p => p.Value);

            foreach (KeyValuePair<string, int> keyValuePair in possibilities)
            {
                FeaturePossibility possibility = new FeaturePossibility((double)keyValuePair.Value / total, keyValuePair.Key);

                possibility.BuildDictionary(arrayPossibilities.Where(ap => ap[columnIndex].ToString() == keyValuePair.Key).Select(ap => ap[labelColumnIndex]).ToArray(),
                    labelPossibilities);

                Possibilities.Add(keyValuePair.Key, possibility);
            }
        }

        internal void BuildDictionary(object[] arrayPossibilities)
        {
            var possibilities = Helper.GetPossibilitiesOfColumn(arrayPossibilities);
            int total = arrayPossibilities.Count();

            foreach(KeyValuePair<string, int> keyValuePair in possibilities)
            {
                Possibilities.Add(keyValuePair.Key, new FeaturePossibility((double) keyValuePair.Value / total, keyValuePair.Key));
            }
        }
    }
}
