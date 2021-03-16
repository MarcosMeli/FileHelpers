using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers
{
    /// <summary>This is a base for examples that do not have input files.</summary>
    public class OutputBase
    {
        /// <summary><see cref="ExampleBase.Console"/></summary>
        protected VirtualConsole Console => new VirtualConsole();
    }
}