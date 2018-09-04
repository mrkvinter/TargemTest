namespace Calculator.Models
{
    public class OperationExpressionUnit : IExpressionUnit
    {
        public OperationExpressionUnit(char number)
        {
            Operation = number;
        }
        
        public char Operation { get; }
        
        public override string ToString()
        {
            return $"Operation: {Operation}";
        }

    }
}