using System;

namespace FileHelpers.Events
{
    public abstract class FileHelpersEventArgs<T> : EventArgs where T : class
    {
        protected FileHelpersEventArgs(EventEngineBase<T> engine, int lineNumber)
        {
            Engine = engine;
            LineNumber = lineNumber;
        }

        /// <summary> The engine that raise the event </summary>
        public EventEngineBase<T> Engine { get; set; }

        /// <summary>The current line number.</summary>
        public int LineNumber { get; private set; }
    }
}