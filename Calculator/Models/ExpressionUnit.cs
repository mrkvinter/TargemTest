using System;

namespace Calculator.Models
{
    public class ExpressionUnit
    {
        private readonly double number;
        private readonly char operation;
        public TypeStatement Type;

        public ExpressionUnit(char operation)
        {
            this.operation = operation;
            Type = TypeStatement.Operation;
        }

        public ExpressionUnit(double number)
        {
            this.number = number;
            Type = TypeStatement.Number;
        }

        public double Number
        {
            get
            {
                if (Type != TypeStatement.Number)
                    throw new InvalidOperationException();
                return number;
            }
        }

        public char Operation
        {
            get
            {
                if (Type != TypeStatement.Operation)
                    throw new InvalidOperationException();
                return operation;
            }
        }

        public override string ToString()
        {
            return Type == TypeStatement.Number ? $"Number: {number}" : $"Operation: {operation}";
        }
    }
}