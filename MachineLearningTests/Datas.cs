using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearningTests
{
    internal static class Datas
    {

        /// BAYES
        
        internal static object[][] bayesTrainingDatas = new object[][]
        {
            new object[] { "sunny", "hot", "high", "false", "no" },
            new object[] { "sunny", "hot", "high", "true", "no" },
            new object[] { "overcast", "hot", "high", "false", "yes" },
            new object[] { "rainy", "mild", "high", "false", "yes" },
            new object[] { "rainy", "cool", "normal", "false", "yes" },
            new object[] { "rainy", "cool", "normal", "true", "no" },
            new object[] { "overcast", "cool", "normal", "true", "yes" },
            new object[] { "sunny", "mild", "high", "false", "no" },
            new object[] { "sunny", "cool", "normal", "false", "yes" },
            new object[] { "rainy", "mild", "normal", "false", "yes" },
            new object[] { "sunny", "mild", "normal", "true", "yes" },
            new object[] { "overcast", "mild", "high", "true", "yes" },
            new object[] { "overcast", "hot", "normal", "false", "yes" },
            new object[] { "rainy", "mild", "high", "true", "no" },
        };

        internal static object[][] bayesTestingDatas = new object[][]
        {
            new object[] { "sunny", "cool", "high", "true", "no" }
        };

        internal static readonly string[] bayesHeaders = new string[] { "outlook", "temperature", "humidity", "windy", "label" };

        internal static int bayesLabelIndex = 4;

        /// FRUITS
        
        internal static object[][] fruitsTrainingDatas = new object[][]
        {
            new object [] {"green", 3, "Apple"},
            new object [] {"yellow", 3, "Apple"},
            new object [] {"red", 1, "Grape"},
            new object [] {"red", 1, "Grape"},
            new object [] {"yellow", 3, "Lemon"}
        };

        internal static readonly object[][] fruitsTestingDatas = new object[][]
        {
            new object [] {"green", 3, "Apple"},
            new object [] {"yellow", 4, "Apple"},
            new object [] {"red", 2, "Grape"},
            new object [] {"red", 1, "Grape"},
            new object [] {"yellow", 3, "Lemon"},
        };

        internal static readonly string[] fruitsHeaders = new string[] { "color", "diameter", "label" };

        internal static readonly int fruitsLabelIndex = 2;

        /// IRIS

        internal static object[][] irisTrainingDatas = new object[][]
        {
            
        };

        internal static object[][] irisTestingDatas = new object[][]
        {
            
        };

        internal static readonly int irisLabelIndex = 4;
    }
}
