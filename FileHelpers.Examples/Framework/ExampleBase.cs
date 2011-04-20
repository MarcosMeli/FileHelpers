using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ExamplesFramework
{
    public abstract class ExampleBase
    {
        /// <summary>
        /// Used to capture the Console output
        /// Samples look like they are simple writes to console
        /// but this internal member captures output and
        /// sends to a DemoFile.
        /// </summary>
        protected StringWriter Console;

        /// <summary>
        /// Captured console text
        /// </summary>
        public String Output;

        /// <summary>
        /// Whether the test has run yet or not
        /// </summary>
        public bool TestRun { get; set; }

        /// <summary>
        /// Create a Demo class an initialise variables
        /// </summary>
        public ExampleBase()
        {
            TestRun = false;
        }

        /// <summary>
        /// Execute the test run
        /// </summary>
        public void RunExample()
        {
            TestRun = true;

            using (this.Console = new StringWriter())
            {
                Run();
                this.Output = this.Console.ToString();
            }
        }

        public abstract void Run();
    }
}
