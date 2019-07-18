using System;
using System.Linq;

namespace MachineLearningTests.Perceptron
{
    public class Classifier
    {
        private double[] weights;
        private double learningRate;

        public Classifier(int numbWeights, double learningRate)
        {
            Random r = new Random();
            weights = Enumerable.Range(0, numbWeights + 1).Select(i => r.NextDouble()).ToArray();
            this.learningRate = learningRate;
        }

        public void Classify(double[][] inputs, double[] outputs)
        {
            if(inputs.Length != outputs.Length) { throw new Exception("not the same size"); }

            double totalError = 1;

            while(totalError > 0.2)
            {
                totalError = 0;
                for(int i = 0; i < inputs.Length; i++)
                {
                    int output = calculateOuput(inputs[i]);

                    double error = outputs[i] - output;

                    for(int j = 0; j < weights.Length - 1; j++)
                    {
                        weights[j] += (learningRate * error * inputs[i][j]);
                    }

                    weights[weights.Length - 1] += learningRate * error * 1;
                }
            }

            Console.WriteLine("results : ");
            for(int i = 0; i < inputs.Length; i++)
            {
                Console.WriteLine("excepted : " + outputs[i]);
                Console.WriteLine("real : " + calculateOuput(inputs[i]));
            }
            Console.ReadLine();
        }

        public int calculateOuput(double[] input)
        {
            double sum = input.Select((r, i) => r * weights[i]).Sum();
            sum += 1 * weights.Last();
            return (sum >= 0) ? 1 : 0;
        }
    }
}
