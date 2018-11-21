using System;
using System.Collections.Generic;
using System.IO;

namespace MachineLearningTests
{
    class Program
    {
        static void Main(string[] args)
        {
            InitiateIrisBase();

            Classifier classifier = new Classifier();
            var tree = classifier.BuildTree(Datas.trainingDatas);
            classifier.PrintTree(tree);

            foreach(var datas in Datas.testingDatas)
            {
                int numbError = 0;
                Console.WriteLine("excepted : " + datas[Datas.labelIndex]);
                string name = classifier.Classify(datas, tree);
                Console.WriteLine(name);

                if (name.Split(' ')[0].ToString() != datas[Datas.labelIndex].ToString())
                {
                    numbError++;
                }
            }

            Console.ReadLine();
        }

        static void InitiateIrisBase()
        {
            var lines = File.ReadAllLines(@"C:\Users\jpougetoux\source\repos\MachineLearningTests\MachineLearningTests\IrisDatas.txt");

            List<object[]> objects = new List<object[]>();

            for (int i = 0; i < 75; i++)
            {
                var splittedLine = lines[i].Split(',');
                objects.Add(new object[] { double.Parse(splittedLine[0].Replace('.', ',')), double.Parse(splittedLine[1].Replace('.', ',')), double.Parse(splittedLine[2].Replace('.', ',')), double.Parse(splittedLine[3].Replace('.', ',')), splittedLine[4] });
            }

            Datas.trainingDatas = objects.ToArray();
            objects = new List<object[]>();

            for (int i = 75; i < 150; i++)
            {
                var splittedLine = lines[i].Split(',');
                objects.Add(new object[] { double.Parse(splittedLine[0].Replace('.', ',')), double.Parse(splittedLine[1].Replace('.', ',')), double.Parse(splittedLine[2].Replace('.', ',')), double.Parse(splittedLine[3].Replace('.', ',')), splittedLine[4] });
            }

            Datas.testingDatas = objects.ToArray();
        }
    }
}
