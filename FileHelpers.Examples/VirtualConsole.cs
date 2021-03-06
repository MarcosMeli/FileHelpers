using System;
using System.Collections;
using System.Collections.Generic;

namespace ExamplesFx
{
    /// <summary>
    ///     Idea is copied from
    ///     <see href="https://github.com/MarcosMeli/ExamplesFx/blob/master/ExamplesFx/ExampleBase.cs" />.
    /// </summary>
    public class VirtualConsole
    {
        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(decimal? value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(object value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
        }
    }
}