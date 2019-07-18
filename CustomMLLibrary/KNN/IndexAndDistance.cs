using System;

namespace MachineLearningTests.KNN
{
    internal class IndexAndDistance : IComparable<IndexAndDistance>
    {
        internal int index;
        internal double distance;

        internal IndexAndDistance(int index, double distance)
        {
            this.index = index;
            this.distance = distance;
        }

        public int CompareTo(IndexAndDistance other)
        {
            if (distance < other.distance) return -1;
            else if (distance > other.distance) return +1;
            else return 0;
        }
    }
}