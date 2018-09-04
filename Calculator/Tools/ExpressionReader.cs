using System;
using System.Collections.Generic;
using System.Globalization;
using Calculator.Models;

namespace Calculator.Tools
{
    public class ExpressionReader
    {
        private readonly string expression;
        private readonly ExpressionUnitChecker expressionUnitChecker;
        private int cursor;

        public ExpressionReader(ExpressionUnitChecker expressionUnitChecker, string expression)
        {
            this.expressionUnitChecker = expressionUnitChecker;
            this.expression = expression.Replace(" ", "");
        }

        public IEnumerable<IExpressionUnit> Parse()
        {
            while (cursor < expression.Length) yield return ReadNext();
        }

        private IExpressionUnit ReadNext()
        {
            if (cursor < expression.Length && expressionUnitChecker.IsNumber(expression[cursor]))
                return ReadNumber();

            cursor++;
            if (!expressionUnitChecker.IsOperationSing(expression[cursor - 1]))
                throw new InvalidOperationException();
            return new OperationExpressionUnit(expression[cursor - 1]);
        }

        private NumberExpressionUnit ReadNumber()
        {
            var numberLen = 1;
            while (cursor + numberLen < expression.Length &&
                   expressionUnitChecker.IsNumber(expression[cursor + numberLen]))
                numberLen++;
            cursor += numberLen;
            var numberString = expression.Substring(cursor - numberLen, numberLen);
            var number = double.Parse(numberString,
                NumberStyles.Number, CultureInfo.InvariantCulture);
            return new NumberExpressionUnit(number);
        }
    }
}