using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treehouse6.ConsoleTesting;

namespace CalculatorConsoleTest.Example
{
    [ConsoleTestClass]
    public class When_The_Calculator_Does_Addition
    {
        public When_The_Calculator_Does_Addition()
        { }

        private Calc _calc { get; set; }


        [ConsoleTestHarnessSetup]
        public void HarnessSetup()
        {
            _calc = new Calc();
        }


        [ConsoleTestMethod]
        public void Then_Two_Positive_Numbers_Shall_Be_Added()
        {
            var numa = 2;
            var numb = 3;
            var result = _calc.Add(numa, numb);
            Assert.AreEqual(result, 5, "Positive numbers did not add properly.");
        }

        [ConsoleTestMethod]
        public void Then_This_method_will_Not_Pass()
        {
            Assert.Fail("This test is not supposed to pass");
        }

        [ConsoleTestMethod]
        public void Then_Division_Shall_Return_A_Decimal()
        {
            var numerator = 13;
            var divisor = 4;
            var result = _calc.divide(numerator, divisor);

            Assert.IsInstanceOfType(result, typeof(double), "result was not a double");
            Assert.AreEqual(3.25, result, "result was incorrect.");
        }

        [ConsoleTestMethod]
        public void Then_This_Test_Errors_Out_Before_It_Asserts()
        {
            var numa = 5;
            var numb = 10;
            var result = _calc.multiply(numa, numb);
            throw new ApplicationException("Some kind of multiply error happened here.");

            Assert.AreEqual(50, result, "Numbers were not multiplied acorrectly.");
        }

    }
}
