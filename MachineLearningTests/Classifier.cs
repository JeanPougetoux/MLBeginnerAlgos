using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MachineLearningTests
{
    public class Classifier
    {
        private object[] UniqueValues(object[][] datas, int columnIndex)
        {
            return datas.Select(d => d[columnIndex]).Distinct().ToArray();
        }

        private Dictionary<string, int> ClassCounts(object[][] datas)
        {
            var dictionary = new Dictionary<string, int>();

            foreach(var entry in datas)
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

        private (List<object[]> trueRows, List<object[]> falseRows) Partition(object[][] datas, Question question)
        {
            List<object[]> trueRows = new List<object[]>();
            List<object[]> falseRows = new List<object[]>();

            foreach (var row in datas)
            {
                if (question.Match(row))
                {
                    trueRows.Add(row);
                }
                else
                {
                    falseRows.Add(row);
                }
            }

            return (trueRows, falseRows);
        }

        private double CalculateGini(object[][] datas)
        {
            var dictionaryClassesCounts = ClassCounts(datas);
            double impurity = 1;

            foreach (KeyValuePair<string, int> pair in dictionaryClassesCounts)
            {
                var val = dictionaryClassesCounts[pair.Key];
                double probOfLbs = val / (double)datas.Count();
                impurity -= Math.Pow(probOfLbs, 2);
            }

            return impurity;
        }

        private double CalculateGain(List<object[]> leftRows, List<object[]> rightRows, double currentUncertainty)
        {
            (int lenLeft, int lenRight) = (leftRows.Count(), rightRows.Count());
            double p = lenLeft / (double)(lenLeft + lenRight);

            return currentUncertainty - p * CalculateGini(leftRows.ToArray()) - (1 - p) * CalculateGini(rightRows.ToArray());
        }

        private (double, Question) FindBestSplit(object[][] datas)
        {
            double bestGain = 0;
            Question bestQuestion = null;
            double currentUncertainty = CalculateGini(datas);
            int numbColumn = datas[0].Count();

            for(int i = 0; i < numbColumn; i++)
            {
                if (i == Datas.labelIndex)
                {
                    continue;
                }
                object[] values = UniqueValues(datas, i);

                foreach (var value in values)
                {
                    Question question = new Question(i, value);

                    (List<object[]> trueRows, List<object[]> falseRows) = Partition(datas, question);

                    if(trueRows.Count() == 0 || falseRows.Count() == 0)
                    {
                        continue;
                    }

                    double gain = CalculateGain(trueRows, falseRows, currentUncertainty);

                    if(gain >= bestGain)
                    {
                        (bestGain, bestQuestion) = (gain, question);
                    }
                }
            }

            return (bestGain, bestQuestion);
        }

        public ITreeElement BuildTree(object[][] datas)
        {
            (double gain, Question question) = FindBestSplit(datas);

            if(gain == 0)
            {
                return new Leaf(datas);
            }

            (List<object[]> trueRows, List<object[]> falseRows) = Partition(datas, question);

            var trueBranch = BuildTree(trueRows.ToArray());
            var falseBranch = BuildTree(falseRows.ToArray());

            return new DecisionNode(question, trueBranch, falseBranch);
        }

        public void PrintTree(ITreeElement node, string spacing = "")
        {
            if(node is Leaf)
            {
                node.PrintElement(spacing);
                return;
            }

            node.PrintElement(spacing);

            Console.WriteLine(spacing + "---> True : ");

            PrintTree(((DecisionNode)node).trueBranch, spacing + "  ");

            Console.WriteLine(spacing + "---> False : ");

            PrintTree(((DecisionNode)node).falseBranch, spacing + "  ");
        }

        public string Classify(object[] data, ITreeElement baseNode)
        {
            if(baseNode is Leaf)
            {
                var predictions = ((Leaf)baseNode).Predictions;
                double total = predictions.Sum(c => c.Value);

                string prediction = "";

                foreach(string key in predictions.Keys)
                {
                    string percent = key + " : " + (predictions[key] / total * 100).ToString() + "%";
                    prediction += percent;
                    prediction += ", ";
                }

                return prediction;
            }

            if (((DecisionNode)baseNode).question.Match(data))
            {
                return Classify(data, ((DecisionNode)baseNode).trueBranch);
            }
            else
            {
                return Classify(data, ((DecisionNode)baseNode).falseBranch);
            }
        }

        private void PrintTrueFalse(List<object[]> trueRows, List<object[]> falseRows)
        {
            Console.Write("True : ");
            foreach (var v in trueRows)
            {
                PrintArray(v);
            }

            Console.WriteLine("");
            Console.Write("False : ");
            foreach (var v in falseRows)
            {
                PrintArray(v);
            }

            Console.ReadLine();
        }

        private void PrintArray(object[] array)
        {
            Console.Write("[");
            foreach (var value in array)
            {
                Console.Write(value.ToString() + ",");
                
            }
            Console.Write("]");
        }
    }
}
