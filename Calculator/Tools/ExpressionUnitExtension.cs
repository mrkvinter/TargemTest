using Calculator.Models;

namespace Calculator.Tools
{
    public static class ExpressionUnitExtension
    {
        public static bool IsOpenBracket(this ExpressionUnit unit)
        {
            return unit.Type == TypeStatement.Operation && unit.Operation == '(';
        }

        public static bool IsCloseBracket(this ExpressionUnit unit)
        {
            return unit.Type == TypeStatement.Operation && unit.Operation == ')';
        }

        public static bool IsNumber(this ExpressionUnit unit)
        {
            return unit.Type == TypeStatement.Number;
        }
    }
}