using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearningTests.DecisionTree
{
    internal class Question
    {
        private int column;
        private object value;
        private object[] headers;

        internal Question(int column, object value, object[] headers)
        {
            this.column = column;
            this.value = value;
            this.headers = headers;
        }

        internal bool Match(object[] dataLine)
        {
            object lineValue = dataLine[column];
            if (IsNumeric(lineValue, out double lineNumber) && IsNumeric(value, out double valueNumber))
            {
                return lineNumber >= valueNumber;
            }
            else
            {
                return lineValue.ToString() == value.ToString();
            }
        }

        public override string ToString()
        {
            string condition = "==";
            if (IsNumeric(value, out double number))
            {
                condition = ">=";
            }
            return $"Is {headers[column]} {condition} {value.ToString()} ?";
        }

        private bool IsNumeric(object o, out double numb)
        {
            bool isDouble = double.TryParse(o.ToString(), out double number);
            numb = number;
            return isDouble;
        }
    }
}
