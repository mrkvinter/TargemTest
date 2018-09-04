using System.Collections.Generic;

namespace Calculator.Tools
{
    public class ExpressionUnitChecker
    {
        private readonly HashSet<char> availableChars = new HashSet<char>
            {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', ','};

        private readonly Dictionary<char, int> weights = new Dictionary<char, int>
        {
            {'(', 0},
            {')', 1},
            {'+', 2},
            {'-', 2},
            {'*', 3},
            {'/', 3},
            {'^', 4}
        };

        public int GetWeight(char operationSign)
        {
            return weights[operationSign];
        }

        public bool IsOperationSing(char sign)
        {
            return weights.ContainsKey(sign);
        }

        public bool IsNumber(char number)
        {
            return availableChars.Contains(number);
        }
    }
}