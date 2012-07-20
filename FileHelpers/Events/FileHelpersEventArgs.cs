using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Events
{
    /// <summary>
    /// Event args to signal engine failures
    /// </summary>
    /// <typeparam name="T">Type of object we are parsing</typeparam>
    public abstract class FileHelpersEventArgs<T> : EventArgs where T : class
    {
        /// <summary>
        /// Define an event message for an engine
        /// </summary>
        /// <param name="engine">Engine type</param>
        /// <param name="lineNumber">Line number error occurred</param>
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