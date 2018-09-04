namespace Calculator.Models
{
    public class NumberExpressionUnit : IExpressionUnit
    {
        public NumberExpressionUnit(double number)
        {
            Number = number;
        }
        
        public double Number { get; }
        
        public override string ToString()
        {
            return $"Number: {Number}";
        }
    }
}