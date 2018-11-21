using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MachineLearningTests
{
    internal class Leaf : ITreeElement
    {
        internal readonly Dictionary<string, int> Predictions;

        internal Leaf(object[][] datas)
        {
            Predictions = ClassCounts(datas);
        }

        public void PrintElement(string spacing)
        {
            Console.Write(spacing + "Predict : ");
            foreach (KeyValuePair<string, int> kvp in Predictions)
            {
                Console.Write(kvp.Key + " : " + kvp.Value + ",");
            }
            Console.WriteLine();
        }

        private Dictionary<string, int> ClassCounts(object[][] datas)
        {
            var dictionary = new Dictionary<string, int>();

            foreach (var entry in datas)
            {
                string label = entry[Datas.labelIndex].ToString();

                if (!dictionary.ContainsKey(label))
                {
                    dictionary[label] = 0;
                }

                dictionary[label]++;
            }

            return dictionary;
        }
    }
}
