using System;
using System.Collections;
using System.Collections.Generic;

namespace ExamplesFx
{
    public abstract class ExampleBase
    {
        /// <summary>This property allows inheritors to call Console.Method() just like the static Console class.
        /// This fake console captures the output. The output is used for the documentation generation.
        /// </summary>
        protected VirtualConsole Console => new VirtualConsole();

        public abstract void Run();
    }
}