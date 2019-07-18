using MachineLearningTests.DecisionTree;
using MachineLearningTests.KNN;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MachineLearningTests
{
    class Program
    {
        static void Main(string[] args)
        {
            InitiateIrisBase();

            // Pour essayer on lance le même dataset avec trois algos différents :

            /*LaunchBayesTesting(Datas.bayesTrainingDatas, Datas.bayesTestingDatas, Datas.bayesHeaders, Datas.bayesLabelIndex);
            LaunchDecisionTreeTesting(Datas.bayesTrainingDatas, Datas.bayesTestingDatas, Datas.bayesHeaders, Datas.bayesLabelIndex);
            LaunchKNNTesting(ConvertDataSetToNumeric(Datas.bayesTrainingDatas, Datas.bayesLabelIndex), ConvertDataSetToNumeric(Datas.bayesTestingDatas, Datas.bayesLabelIndex), Datas.bayesLabelIndex);*/

            // LaunchDecisionTreeTesting(Datas.fruitsTrainingDatas, Datas.fruitsTestingDatas, Datas.fruitsHeaders, Datas.fruitsLabelIndex);
            // LaunchKNNTesting(Datas.irisTrainingDatas, Datas.irisTestingDatas, Datas.irisLabelIndex);

            Perceptron.Classifier per = new Perceptron.Classifier(2, 1);
            per.Classify(new double[][] { new double[] { 1, 0 }, new double[] { 1, 1 }, new double[] { 0, 1 }, new double[] { 0, 0 } }, new double[]{ 0, 1, 0, 0 });
        }

        static void LaunchKNNTesting(object[][] trainingDatas, object[][] testingDatas, int labelIndex)
        {
            KNNAlgo algo = new KNNAlgo(trainingDatas, labelIndex);
            double[][] temp = algo.GetDoubleArrayFromObjectArray(testingDatas);

            int error = 0;

            for (int i = 0; i < temp.Length; i++)
            {
                (string, double)[] predictions = algo.Classify(temp[i], 2);
                string actual = testingDatas[i][labelIndex].ToString();

                Console.WriteLine($"Actual : {actual}, Prediction : { string.Join(" ", predictions.ToList().Select(p => "" + p.Item1 + " : " + p.Item2 + "% ").ToArray()) }");

                if (predictions.Count() != 1 || (predictions.Count() == 1 && predictions[0].Item1 != actual)) error++;
            }

            Console.WriteLine(Environment.NewLine + $"Passed : { temp.Length }, Errors : { error }.");
            Console.ReadLine();
        }

        static void LaunchDecisionTreeTesting(object[][] trainingDatas, object[][] testingDatas, object[] headers, int labelIndex)
        {
            Console.WriteLine("--- TRAINING DATAS ---" + Environment.NewLine);

            Classifier classifier = new Classifier(labelIndex, headers);
            var tree = classifier.BuildTree(trainingDatas);
            classifier.PrintTree(tree);

            Console.WriteLine(Environment.NewLine + "--- TESTING DATAS ---" + Environment.NewLine);

            int numbError = 0;

            foreach (var datas in testingDatas)
            {
                Console.WriteLine("excepted : " + datas[labelIndex]);

                string name = classifier.Classify(datas, tree);
                name = name.Remove(name.Length - 2);

                Console.WriteLine(name);


                if (name.Split(',').Count() > 1 || name.Split(' ')[0].ToString() != datas[labelIndex].ToString())
                {
                    numbError++;
                }
            }

            Console.WriteLine(Environment.NewLine + "" + numbError + " errors.");

            Console.ReadLine();
        }

        static void LaunchBayesTesting(object[][] trainingDatas, object[][] testingDatas, object[] headers, int labelIndex)
        {
            NaiveBayes.Classifier classifier = new NaiveBayes.Classifier(trainingDatas, headers, labelIndex);
            classifier.BuildProbabilitiesArrays();
            Console.WriteLine(classifier.Classify(testingDatas[0]));
            Console.ReadLine();
        }

        static void InitiateIrisBase()
        {
            var lines = File.ReadAllLines(@"C:\Users\jpougetoux\source\repos\MachineLearningTests\MachineLearningTests\IrisDatas.txt");

            List<object[]> objects = new List<object[]>();

            for (int i = 0; i < 70; i++)
            {
                var splittedLine = lines[i].Split(',');
                objects.Add(new object[] { double.Parse(splittedLine[0].Replace('.', ',')), double.Parse(splittedLine[1].Replace('.', ',')), double.Parse(splittedLine[2].Replace('.', ',')), double.Parse(splittedLine[3].Replace('.', ',')), splittedLine[4] });
            }

            Datas.irisTrainingDatas = objects.ToArray();
            objects = new List<object[]>();

            for (int i = 70; i < 150; i++)
            {
                var splittedLine = lines[i].Split(',');
                objects.Add(new object[] { double.Parse(splittedLine[0].Replace('.', ',')), double.Parse(splittedLine[1].Replace('.', ',')), double.Parse(splittedLine[2].Replace('.', ',')), double.Parse(splittedLine[3].Replace('.', ',')), splittedLine[4] });
            }

            Datas.irisTestingDatas = objects.ToArray();
        }

        static object[][] ConvertDataSetToNumeric(object[][] datas, int labelIndex)
        {
            object[][] newDataSet = new object[datas.Length][];
            Dictionary<string, double> dictio = new Dictionary<string, double>();

            for (int i = 0; i < datas.Length; i++)
            {
                newDataSet[i] = new object[datas[0].Length];
                for (int j = 0; j < datas[0].Length; j++)
                {
                    if (j == labelIndex)
                    {
                        newDataSet[i][j] = datas[i][j].ToString();
                        continue;
                    }

                    if (double.TryParse(datas[i][j].ToString(), out double result))
                    {
                        newDataSet[i][j] = result;
                    }
                    else
                    {
                        if (!dictio.ContainsKey(datas[i][j].ToString()))
                        {
                            if (dictio.Count() == 0)
                            {
                                dictio[datas[i][j].ToString()] = 1;
                            }
                            else
                            {
                                dictio[datas[i][j].ToString()] = dictio.Max(d => d.Value) + 1;
                            }
                        }

                        newDataSet[i][j] = dictio[datas[i][j].ToString()];
                    }
                }
            }

            return newDataSet;
        }
    }
}
