using System;
using System.Collections.Generic;
using System.Linq;

namespace MachineLearningTests.KNN
{
    public class KNNAlgo
    {
        private readonly double[][] datas;
        private Dictionary<string, int> labelsIndexes;

        public KNNAlgo(object[][] dat)
        {
            labelsIndexes = new Dictionary<string, int>();
            datas = GetDoubleArrayFromObjectArray(dat);
        }

        public double[][] GetDoubleArrayFromObjectArray(object[][] datas)
        {
            List<double[]> listDoubles = new List<double[]>();

            foreach(var data in datas)
            {
                List<double> tempList = new List<double>();

                for(int i = 0; i < data.Count(); i++)
                {
                    if(i == Datas.labelIndex)
                    {
                        if (!labelsIndexes.ContainsKey(data[i].ToString())){
                            if (labelsIndexes.Count() == 0)
                            {
                                labelsIndexes[data[i].ToString()] = 0;
                            }
                            else
                            {
                                int maxIndex = labelsIndexes.Max(li => li.Value);
                                labelsIndexes[data[i].ToString()] = maxIndex + 1;
                            }
                        }

                        tempList.Add(double.Parse(labelsIndexes[data[i].ToString()].ToString()));
                        continue;
                    }
                    else if(!double.TryParse(data[i].ToString(), out double parsed))
                    {
                        throw new Exception("Il y a autre choses que des doubles dans les données.");
                    }
                    else
                    {
                        tempList.Add(parsed);
                    }
                }

                listDoubles.Add(tempList.ToArray());
            }

            return listDoubles.ToArray();
        }

        public (string, double)[] Classify(double[] unknown, int k)
        {
            int n = datas.Count();
            IndexAndDistance[] infos = new IndexAndDistance[n];

            for(int i = 0; i < n; i++)
            {
                double distance = Distance(unknown, i);
                IndexAndDistance current = new IndexAndDistance(i, distance);

                infos[i] = current;
            }

            Array.Sort(infos);

            // PrintPredictions(infos, k);

            int[] result = Vote(infos.Take(k).ToArray());
            double perCent = 100.0 / result.Count();

            (string, double)[] toReturn = new (string, double)[result.Count()];

            for (int i = 0; i < result.Count(); i++)
            {
                string label = labelsIndexes.First(li => li.Value == result[i]).Key;
                toReturn[i] = (label, perCent);
            };

            return toReturn;
        }

        private int[] Vote(IndexAndDistance[] infos)
        {
            Dictionary<int, int> votes = new Dictionary<int, int>();

            for(int i = 0; i < infos.Length; i++)
            {
                int index = infos[i].index;
                int c = (int) datas[index][Datas.labelIndex];

                if (!votes.ContainsKey(c))
                {
                    votes[c] = 0;
                }

                votes[c]++;
            }

            return votes.Where(v => v.Value == (votes.Max(vo => vo.Value))).Select(v => v.Key).ToArray();
        }

        private void PrintPredictions(IndexAndDistance[] infos, int k)
        {
            Console.WriteLine("Nearest / Distance / Class");
            Console.WriteLine("==========================");
            for (int i = 0; i < k; ++i)
            {
                int c = (int)datas[infos[i].index][Datas.labelIndex];
                string dist = infos[i].distance.ToString("F3");

                var d = datas[infos[i].index];

                Console.WriteLine("( " + string.Join(", ", d.Where(e => Array.IndexOf(d, e) != Datas.labelIndex).ToArray()) + " )  :  " +
                  dist + "        " + c);
            }
        }

        private double Distance(double[] unknown, int indexData)
        {
            // distance = racine((p1.x - p2.x)² + (p1.y - p2.y)² + (p1.z - p2.z)²)
            double sum = 0.0;

            for(int i = 0; i < unknown.Length; i++)
            {
                if (i == Datas.labelIndex) continue;

                sum += Math.Pow((unknown[i] - datas[indexData][i]), 2);
            }

            return Math.Sqrt(sum);
        }
    }
}