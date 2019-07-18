using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearningTests.NaiveBayes
{
    internal struct Label
    {
        internal string Title;
        internal double Probability;

        public Label(string title, double probability)
        {
            Title = title;
            Probability = probability;
        }
    }
}
