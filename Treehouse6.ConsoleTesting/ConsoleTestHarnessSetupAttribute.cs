using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treehouse6.ConsoleTesting
{
    /// <summary>
    /// Designates a method to be called once per test class instance, 
    /// before the collection of test methods of that class are run
    /// (ie:  Once per test CLASS, NOT once-per-test).
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
    public class ConsoleTestHarnessSetupAttribute : Attribute
    {
        public ConsoleTestHarnessSetupAttribute()
            : base()
        { }
    }
}
