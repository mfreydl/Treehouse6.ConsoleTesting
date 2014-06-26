using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Treehouse6.ConsoleTesting
{
    /// <summary>
    /// Occurs when exceptions are encountered while executing 
    /// the method on the test class designated as 'ConsoleTestHarnessSetup'.
    /// </summary>
    public class HarnessSetupMethodException : ApplicationException
    {
        public HarnessSetupMethodException()
            : base()
        { }

        public HarnessSetupMethodException(string message)
            : base(message)
        { }

        public HarnessSetupMethodException(string message, Exception innerEx)
            : base(message, innerEx)
        { }

        public HarnessSetupMethodException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
