using System;
using Calculator.Tools;

namespace Calculator
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 1) Console.WriteLine("Not correct. Please, pass expression for calculate.");
            var expression = args[0];
            var calc = new Tools.Calculator();
            try
            {
                var result = calc.Calculate(expression);
                Console.WriteLine($"{expression} = {result}");
            }
            catch (NotCorrectBracketsCountException)
            {
                Console.WriteLine("Expression have not correct brackets.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Invalid expression.");
            }
            catch (Exception)
            {
                Console.WriteLine("Other exception");
            }
        }
    }
}