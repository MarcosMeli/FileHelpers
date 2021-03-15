using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers
{
    [TestFixture]
    public class OutputBase
    {
        /// <summary><see cref="ExampleBase.Console"/></summary>
        protected VirtualConsole Console => new VirtualConsole();
    }
}