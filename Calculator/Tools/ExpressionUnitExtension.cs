using Calculator.Models;

namespace Calculator.Tools
{
    public static class ExpressionUnitExtension
    {
        public static bool IsOpenBracket(this IExpressionUnit unit)
        {
            return unit is OperationExpressionUnit o && o.Operation == '(';
        }

        public static bool IsCloseBracket(this IExpressionUnit unit)
        {
            return unit is OperationExpressionUnit o && o.Operation == ')';
        }

        public static bool IsNumber(this IExpressionUnit unit)
        {
            return unit is NumberExpressionUnit;
        }
    }
}