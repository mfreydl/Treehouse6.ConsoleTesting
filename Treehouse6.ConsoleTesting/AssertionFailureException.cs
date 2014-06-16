using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treehouse6.ConsoleTesting
{
    /// <summary>
    /// Thrown by test runner when assert clauses fail.
    /// </summary>
    public class AssertionFailureException : ApplicationException
    {
        public AssertionFailureException()
            : base()
        { }
        public AssertionFailureException(string message)
            : base(message)
        { }
        public AssertionFailureException(string message, Exception ex)
            : base(message, ex)
        { }

        public object Actual { get; set; }

        public object Expected { get; set; }

    }
}
