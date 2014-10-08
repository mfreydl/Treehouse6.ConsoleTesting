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
    public class HarnessCleanupMethodException : ApplicationException
    {
        public HarnessCleanupMethodException()
            : base()
        { }

        public HarnessCleanupMethodException(string message)
            : base(message)
        { }

        public HarnessCleanupMethodException(string message, Exception innerEx)
            : base(message, innerEx)
        { }

        public HarnessCleanupMethodException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
