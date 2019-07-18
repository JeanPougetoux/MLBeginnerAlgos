using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MachineLearningTests.NaiveBayes
{
    internal class FeaturePossibility
    {
        internal double Probability;
        internal string Title;

        internal Dictionary<string, Label> ForLabel;

        internal FeaturePossibility(double probability, string title)
        {
            Probability = probability;
            Title = title;

            ForLabel = new Dictionary<string, Label>();
        }

        internal void BuildDictionary(object[] arrayPossibilities, Dictionary<string, int> labelPossibilities)
        {
            var possibilities = Helper.GetPossibilitiesOfColumn(arrayPossibilities);

            foreach (KeyValuePair<string, int> keyValuePair in possibilities)
            {
                ForLabel.Add(keyValuePair.Key, new Label(keyValuePair.Key, (double)keyValuePair.Value / labelPossibilities[keyValuePair.Key]));
            }
        }
    }
}
