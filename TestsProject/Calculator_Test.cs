using System;
using System.Collections;
using System.Text;
using Calculator.Tools;
using NUnit.Framework;

namespace TestsProject
{
    [TestFixture]
    public class CalculatorTest
    {
        private readonly Calculator.Tools.Calculator calculator;

        public CalculatorTest()
        {
            calculator = new Calculator.Tools.Calculator();
        }

        [TestCaseSource(nameof(CorrectTestCases))]
        public double CorrectTest(string expression)
        {
            return calculator.Calculate(expression);
        }

        [TestCaseSource(nameof(NotCorrectExpressionTestCases))]
        public void NotCorrectExpression(string expression)
        {
            Assert.Throws<InvalidOperationException>(() => calculator.Calculate(expression));
        }

        [TestCaseSource(nameof(NotCorrectBracketsTestCases))]
        public void NotCorrectBracketsCount(string expression)
        {
            Assert.Throws<NotCorrectBracketsCountException>(() => calculator.Calculate(expression));
        }

        [TestCaseSource(nameof(CorrectBracketsTestCases))]
        public void CorrectBracketsCount(string expression)
        {
            Assert.DoesNotThrow(() => calculator.Calculate(expression));
        }

        public static IEnumerable CorrectTestCases
        {
            get
            {
                yield return new TestCaseData("2+2").Returns(4).SetName("Simple");
                yield return new TestCaseData("2+2*2").Returns(6).SetName("CorrectOrder");
                yield return new TestCaseData("(2+2)*2").Returns(8).SetName("Brackets");
                yield return new TestCaseData("1+2-3*4/2^2").Returns(0).SetName("CorrectOrderAll");
                yield return new TestCaseData("(2+2)(1+3)").Returns(16).SetName("GhostMulty");
                yield return new TestCaseData("  (2 + 2) * ( 1 + 3)  ").Returns(16).SetName("WithSpace");
                yield return new TestCaseData("3^2").Returns(9).SetName("CorrectPower");
                yield return new TestCaseData("2^3").Returns(8).SetName("InvertedCorrectPower");
                yield return new TestCaseData("4-2").Returns(2).SetName("CorrectNegative");
                yield return new TestCaseData("2-4").Returns(-2).SetName("InvertedCorrectNegative");
            }
        }

        public static IEnumerable NotCorrectExpressionTestCases
        {
            get
            {
                yield return new TestCaseData("2++2").SetName("SimpleNotCorrect");
                yield return new TestCaseData("2+*2").SetName("CorrectOrder");
                yield return new TestCaseData("not correct").SetName("Brackets");
                yield return new TestCaseData("a+b").SetName("CorrectOrderAll");
            }
        }

        public static IEnumerable CorrectBracketsTestCases
        {
            get
            {
                yield return new TestCaseData("((0))").SetName("Correct");
                yield return new TestCaseData("0").SetName("Simple");
                yield return new TestCaseData("(0)(0)(0)(0)").SetName("Simple");
                yield return new TestCaseData("((0)((0))(0))").SetName("Simple");
            }
        }

        public static IEnumerable NotCorrectBracketsTestCases
        {
            get
            {
                yield return new TestCaseData("(").SetName("OneOpenBracket");
                yield return new TestCaseData("(1").SetName("OneOpenBracket");
                yield return new TestCaseData(")").SetName("OneCloseBracket");
                yield return new TestCaseData("1)").SetName("OneCloseBracket");
                yield return new TestCaseData(")(").SetName("OpenCloseBracket");
                yield return new TestCaseData(")()(").SetName("ContaintsCorrectSequenceBrackets");
                yield return new TestCaseData("()()(").SetName("StartWithCorrect_EndWithOneOpen_SequenceBrackets");
                yield return new TestCaseData("()())").SetName("StartWithCorrect_EndWithOneClose_SequenceBrackets");
            }
        }

        [Test]
        [Timeout(100)]
        public void ManyBrackets()
        {
            var s = new StringBuilder();
            s.Append('(', 10000);
            s.Append("2+2");
            s.Append(')', 10000);

            var actual = calculator.Calculate(s.ToString());

            Assert.That(actual, Is.EqualTo(4));
        }
    }
}