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
            return "";
        }
    }
}