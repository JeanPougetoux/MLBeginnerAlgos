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

            LaunchBayesTesting(Datas.bayesTrainingDatas, Datas.bayesTestingDatas, Datas.bayesHeaders, Datas.bayesLabelIndex);
            // LaunchDecisionTreeTesting(Datas.fruitsTrainingDatas, Datas.fruitsTestingDatas, Datas.fruitsHeaders, Datas.fruitsLabelIndex);
            // LaunchKNNTesting(Datas.irisTrainingDatas, Datas.irisTestingDatas, Datas.irisLabelIndex);
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
    }
}
