using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearningTests.DecisionTree
{
    internal class DecisionNode : ITreeElement
    {
        internal Question question;
        internal ITreeElement trueBranch;
        internal ITreeElement falseBranch;

        internal DecisionNode(Question question, ITreeElement trueBranch, ITreeElement falseBranch)
        {
            this.question = question;
            this.trueBranch = trueBranch;
            this.falseBranch = falseBranch;
        }

        public void PrintElement(string spacing)
        {
            Console.WriteLine(spacing + question.ToString());
        }
    }
}
