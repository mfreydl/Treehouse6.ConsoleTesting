using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treehouse6.ConsoleTesting
{
    /// <summary>
    /// Designates a method as a unit test to be run from the console window.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=true)]
    public class ConsoleTestMethodAttribute : Attribute
    {
        public ConsoleTestMethodAttribute()
            : base()
        { }
    }

}
