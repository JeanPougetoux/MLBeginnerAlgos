using System;
using System.Collections.Generic;
using System.Linq;

namespace MachineLearningTests.NaiveBayes
{
    class Classifier
    {
        private object[][] trainingDatas;
        private object[] headers;
        private int labelIndex;

        private Dictionary<string, Feature> features;

        public Classifier(object[][] trainingDatas, object[] headers, int labelIndex)
        {
            this.trainingDatas = trainingDatas;
            this.headers = headers;
            this.labelIndex = labelIndex;

            features = new Dictionary<string, Feature>();
        }

        public void BuildProbabilitiesArrays()
        {

            for(int i = 0; i < headers.Count(); i++)
            {
                if(i == labelIndex)
                {
                    Feature label = new Feature("label");
                    label.BuildDictionary(trainingDatas.Select(t => t[labelIndex].ToString()).ToArray());

                    features.Add("label", label);
                }
                else
                {
                    Feature feature = new Feature(headers[i].ToString());
                    feature.BuildDictionary(trainingDatas, i, labelIndex);

                    features.Add(headers[i].ToString(), feature);
                }
            }
        }

        public string Classify(object[] datas)
        {
            Dictionary<string, double> probabilityForEachLabel = new Dictionary<string, double>();

            double pX = 1;

            for (int i = 0; i < datas.Count(); i++)
            {
                if (i == labelIndex) continue;

                pX *= features[headers[i].ToString()].Possibilities[datas[i].ToString()].Probability;
            }

            foreach (KeyValuePair<string, FeaturePossibility> feature in features["label"].Possibilities)
            {
                double probability = 1;

                for (int i = 0; i < datas.Count(); i++)
                {
                    if (i == labelIndex) continue;

                    probability *= features[headers[i].ToString()].Possibilities[datas[i].ToString()].ForLabel[feature.Value.Title].Probability;
                }

                probability *= feature.Value.Probability;

                probabilityForEachLabel[feature.Value.Title] = probability / pX;
            }

            var keyValuePair = probabilityForEachLabel.Where(p => p.Value == (probabilityForEachLabel.Max(q => q.Value))).First();

            return $"Probabilité de { keyValuePair.Value } pour la réponse { keyValuePair.Key }.";
        }
    }
}