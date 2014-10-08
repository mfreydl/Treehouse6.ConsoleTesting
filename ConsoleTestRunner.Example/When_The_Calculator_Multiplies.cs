using CalculatorConsoleTest.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treehouse6.ConsoleTesting;

namespace ConsoleTestRunner.Example
{
    [ConsoleTestClass]
    public class When_The_Calculator_Multiplies
    {
        public When_The_Calculator_Multiplies()
        { }

        private Calc _calc;

        [ConsoleTestHarnessSetup]
        public void HarnessSetup()
        {
            _calc = new Calc();
        }


        [ConsoleTestMethod]
        public void Then_Integers_Shall_Multiply_Accurately()
        {
            int a = 3;
            int b = 5;

            var result = _calc.multiply(a, b);

            Assert.AreEqual(15, result, "The product of two integers was incorrect.");
            Assert.AreEqual(15, result, "The product of {0} and {1} was incorrect.", a, b);
        }
    }
}
