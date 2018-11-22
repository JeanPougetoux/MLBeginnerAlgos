using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearningTests
{
    internal static class Datas
    {
        internal static object[][] trainingDatas = new object[][]
        {
            new object [] {0, 3, "Apple"},
            new object [] {1, 3, "Apple"},
            new object [] {2, 1, "Grape"},
            new object [] {2, 1, "Grape"},
            new object [] {1, 3, "Lemon"}
        };

        internal static readonly object[][] testingDatas = new object[][]
        {
            new object [] {0, 3, "Apple"},
            new object [] {1, 4, "Apple"},
            new object [] {2, 2, "Grape"},
            new object [] {2, 1, "Grape"},
            new object [] {1, 3, "Lemon"},
        };

        internal static object[][] irisTrainingDatas = new object[][]
        {
            
        };

        internal static object[][] irisTestingDatas = new object[][]
        {
            
        };

        internal static readonly string[] headers = new string[] { "color", "diameter", "label" };

        internal static readonly int labelIndex = 2;
    }
}
