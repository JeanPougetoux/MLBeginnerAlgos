using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearningTests
{
    internal static class Datas
    {
        /*internal static object[][] trainingDatas = new object[][]
        {
            new object [] { "Black", 1, 13, "a" },
            new object [] { "Black", 2, 13, "a" },
            new object [] { "Black", 4, 14, "b" },
            new object [] { "Red", 7, 13, "b" },
            new object [] { "White", 15, 12, "c" },
            new object [] { "White", 1, 20, "c" },
            new object [] { "White", 15, 20, "c" }
        };

        internal static readonly object[][] testingDatas = new object[][]
        {
            new object [] { "White", 2, 20, "a" },
            new object [] { "Black", 1, 14, "b" },
            new object [] { "Black", 5, 10, "b" },
        };*/

        internal static object[][] trainingDatas = new object[][]
        {
            
        };

        internal static object[][] testingDatas = new object[][]
        {
            
        };

        internal static readonly string[] headers = new string[] { "sepal l", "sepal w", "petal l", "petal w", "label" };

        internal static readonly int labelIndex = 4;
    }
}
