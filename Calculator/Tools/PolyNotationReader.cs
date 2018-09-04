using System.Collections.Generic;
using System.Linq;
using Calculator.Models;

namespace Calculator.Tools
{
    public class PolyNotationReader
    {
        private readonly ExpressionReader expressionReader;
        private readonly ExpressionUnitChecker expressionUnitChecker;
        private readonly Stack<OperationExpressionUnit> stack = new Stack<OperationExpressionUnit>();

        private IExpressionUnit previousExpressionUnit;

        public PolyNotationReader(ExpressionUnitChecker expressionUnitChecker, string expression)
        {
            this.expressionUnitChecker = expressionUnitChecker;
            expressionReader = new ExpressionReader(expressionUnitChecker, expression.Replace(" ", ""));
        }

        public List<IExpressionUnit> GetPolNotation()
        {
            var result = new List<IExpressionUnit>();
            foreach (var expressionUnit in expressionReader.Parse())
            {
                if (IsGhostedMultiplicationSign(expressionUnit))
                    AddOperation(new OperationExpressionUnit('*'), result);

                switch (expressionUnit)
                {
                        case NumberExpressionUnit n:
                            result.Add(n);
                            break;
                         case OperationExpressionUnit o:
                             if (stack.Count == 0 || o.IsOpenBracket())
                                 stack.Push(o);
                             else if (o.IsCloseBracket())
                                 ReadUnderBrackets(result);
                             else
                                 AddOperation(o, result);
                             break;
                         
                }
                previousExpressionUnit = expressionUnit;
            }

            if (stack.Any(e => e.IsOpenBracket() || e.IsCloseBracket()))
                throw new NotCorrectBracketsCountException();

            result.AddRange(stack);

            return result;
        }

        private void ReadUnderBrackets(List<IExpressionUnit> result)
        {
            OperationExpressionUnit op = null;
            while (stack.Count > 0 && (op = stack.Pop()).Operation != '(')
                result.Add(op);
            if (op != null && !op.IsOpenBracket())
                throw new NotCorrectBracketsCountException();
        }

        private bool IsGhostedMultiplicationSign(IExpressionUnit currentExpressionUnit)
        {
            if (previousExpressionUnit == null) return false;
            
            return previousExpressionUnit.IsCloseBracket() && currentExpressionUnit.IsNumber() ||
                   previousExpressionUnit.IsCloseBracket() && currentExpressionUnit.IsOpenBracket() ||
                   previousExpressionUnit.IsNumber() && currentExpressionUnit.IsOpenBracket();
        }

        private void AddOperation(OperationExpressionUnit expressionUnit, List<IExpressionUnit> result)
        {
            var weight = expressionUnitChecker.GetWeight(expressionUnit.Operation);
            while (stack.Count > 0 && expressionUnitChecker.GetWeight(stack.First().Operation) >= weight)
                result.Add(stack.Pop());
            stack.Push(expressionUnit);
        }
    }
}