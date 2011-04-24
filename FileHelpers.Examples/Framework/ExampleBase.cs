using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ExamplesFramework
{
    
    public abstract class ExampleBase
    {
        #region Virtual Console

        public class VirtualConsole
        {
            private StringBuilder mStream;
            internal event EventHandler Changed;

            public void Clear()
            {
                mStream.Clear();
            }

            private void OnChanged()
            {
                EventHandler handler = Changed;
                if (handler != null) handler(this, EventArgs.Empty);
            }

            public VirtualConsole()
            {
                mStream = new StringBuilder();
            }

            internal String Output
            {
                get { return mStream.ToString(); }
            }

            public void Write(string value)
            {
                mStream.Append(value);
                OnChanged();
            }

            public void Write(object value)
            {
                mStream.Append(value);
                OnChanged();
            }

            public void WriteLine()
            {
                mStream.AppendLine();
                OnChanged();
            }

            public void Write(string format, params object[] arg)
            {
                mStream.AppendFormat(format, arg);
                OnChanged();
            }

            public void WriteLine(string value)
            {
                mStream.AppendLine(value);
                OnChanged();
            }

            public void WriteLine(object value)
            {
                mStream.Append(value);
                mStream.AppendLine();
                OnChanged();
            }

            public void WriteLine(string format, params object[] arg)
            {
                mStream.AppendFormat(format, arg);
                mStream.AppendLine();
                OnChanged();
            }
        }

        #endregion

        protected ExampleBase()
        {
            Console = new VirtualConsole();
        }

        /// <summary>
        /// Used to capture the Console output
        /// Samples look like they are simple writes to console
        /// but this internal member captures output and
        /// sends to a DemoFile.
        /// </summary>
        public VirtualConsole Console { get; private set; }

        
        private Exception mException;

        public Exception Exception
        {
            get
            {
                return mException;
            }
            set
            {
                mException = value;

                //if (mException != null)
                //    Output += mException.ToString();
            }
        }


        /// <summary>
        /// Execute the test run
        /// </summary>
        public void RunExample()
        {
            Console.Clear();
            Run();
        }

        public abstract void Run();
    }
}
