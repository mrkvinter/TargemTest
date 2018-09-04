using System.Collections.Generic;
using System.Linq;
using Calculator.Models;

namespace Calculator.Tools
{
    public class PolyNotationReader
    {
        private readonly ExpressionReader expressionReader;
        private readonly ExpressionUnitChecker expressionUnitChecker;
        private readonly Stack<ExpressionUnit> stack = new Stack<ExpressionUnit>();

        private ExpressionUnit previousExpressionUnit;

        public PolyNotationReader(ExpressionUnitChecker expressionUnitChecker, string expression)
        {
            this.expressionUnitChecker = expressionUnitChecker;
            expressionReader = new ExpressionReader(expressionUnitChecker, expression.Replace(" ", ""));
        }

        public List<ExpressionUnit> GetPolNotation()
        {
            var result = new List<ExpressionUnit>();
            foreach (var expressionUnit in expressionReader.Parse())
            {
                if (IsGhostedMultiplicationSign(expressionUnit))
                    AddOperation(new ExpressionUnit('*'), result);

                if (expressionUnit.Type == TypeStatement.Number)
                {
                    result.Add(expressionUnit);
                }
                else
                {
                    if (stack.Count == 0 || expressionUnit.Operation == '(')
                        stack.Push(expressionUnit);
                    else if (expressionUnit.Operation == ')')
                        ReadUnderBrackets(result);
                    else
                        AddOperation(expressionUnit, result);
                }

                previousExpressionUnit = expressionUnit;
            }

            if (stack.Any(e => e.Operation.Equals('(') || e.Operation.Equals(')')))
                throw new NotCorrectBracketsCountException();

            result.AddRange(stack);

            return result;
        }

        private void ReadUnderBrackets(List<ExpressionUnit> result)
        {
            ExpressionUnit op = null;
            while (stack.Count > 0 && (op = stack.Pop()).Operation != '(')
                result.Add(op);
            if (op != null && (op.Type != TypeStatement.Operation || op.Operation != '('))
                throw new NotCorrectBracketsCountException();
        }

        private bool IsGhostedMultiplicationSign(ExpressionUnit currentExpressionUnit)
        {
            if (previousExpressionUnit == null) return false;
            
            return previousExpressionUnit.IsCloseBracket() && currentExpressionUnit.IsNumber() ||
                   previousExpressionUnit.IsCloseBracket() && currentExpressionUnit.IsOpenBracket() ||
                   previousExpressionUnit.IsNumber() && currentExpressionUnit.IsOpenBracket();
        }

        private void AddOperation(ExpressionUnit expressionUnit, List<ExpressionUnit> result)
        {
            var weight = expressionUnitChecker.GetWeight(expressionUnit.Operation);
            while (stack.Count > 0 && expressionUnitChecker.GetWeight(stack.First().Operation) >= weight)
                result.Add(stack.Pop());
            stack.Push(expressionUnit);
        }
    }
}