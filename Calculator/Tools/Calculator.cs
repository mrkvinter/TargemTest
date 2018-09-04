using System;
using System.Collections.Generic;
using Calculator.Models;

namespace Calculator.Tools
{
    public class Calculator
    {
        public double Calculate(string expression)
        {
            var expressionUnitChecker = new ExpressionUnitChecker();
            var helper = new PolyNotationReader(expressionUnitChecker, expression);
            var result = helper.GetPolNotation();
            var stackExpression = new Stack<double>();

            foreach (var unit in result)
            {
                switch (unit)
                {
                    case NumberExpressionUnit n:
                        stackExpression.Push(n.Number);
                        break;
                    case OperationExpressionUnit o:
                        stackExpression.Push(Calck(stackExpression.Pop(), stackExpression.Pop(), o));
                        break;
                }
            }

            return stackExpression.Peek();
        }

        private double Calck(double a, double b, OperationExpressionUnit operation)
        {
            switch (operation.Operation)
            {
                case '*': return b * a;
                case '/': return b / a;
                case '+': return b + a;
                case '-': return b - a;
                case '^': return Math.Pow(b, a);
                default: throw new NotSupportedException();
            }
        }
    }
}