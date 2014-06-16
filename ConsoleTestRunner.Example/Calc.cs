using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorConsoleTest.Example
{
    /// <summary>
    /// Simple business class for demonstrating math actions.
    /// </summary>
    public class Calc
    {
        /// <summary>
        /// Simple business class for demonstrating math actions.
        /// </summary>
        public Calc()
        { }

        public int Add(int a, int b)
        {
            return a + b;
        }

        public int subtract(int a, int b)
        {
            return a - b;
        }

        public int multiply(int a, int b)
        {
            return a * b;
        }

        public double divide(int a, int b)
        {
            return a / b;
        }
    }
}
