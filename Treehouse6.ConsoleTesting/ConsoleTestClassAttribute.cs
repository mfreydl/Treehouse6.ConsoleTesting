using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treehouse6.ConsoleTesting
{
    /// <summary>
    /// Designates a class one that contains tests to run from the console window.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class ConsoleTestClassAttribute : Attribute
    {
        public ConsoleTestClassAttribute()
            : base()
        { }

    }
}
