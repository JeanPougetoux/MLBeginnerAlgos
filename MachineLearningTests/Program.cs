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

            int numbError = 0;


            foreach (var datas in Datas.testingDatas)
            {
                Console.WriteLine("excepted : " + datas[Datas.labelIndex]);
                string name = classifier.Classify(datas, tree);
                Console.WriteLine(name);

                if (name.Split(' ')[0].ToString() != datas[Datas.labelIndex].ToString())
                {
                    Console.WriteLine("" + datas[0] + " " + datas[1] + " " + datas[2] + " " + datas[3] + " " + datas[4] + " ");
                    numbError++;
                }
            }

            Console.WriteLine("" + numbError + " errors.");

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

            Datas.trainingDatas = objects.ToArray();
            objects = new List<object[]>();

            for (int i = 70; i < 150; i++)
            {
                var splittedLine = lines[i].Split(',');
                objects.Add(new object[] { double.Parse(splittedLine[0].Replace('.', ',')), double.Parse(splittedLine[1].Replace('.', ',')), double.Parse(splittedLine[2].Replace('.', ',')), double.Parse(splittedLine[3].Replace('.', ',')), splittedLine[4] });
            }

            Datas.testingDatas = objects.ToArray();
        }
    }
}
