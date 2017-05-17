using System;

namespace FileHelpers.Events
{
    /// <summary>
    /// Event args to signal engine failures
    /// </summary>
    public abstract class FileHelpersEventArgs : EventArgs
    {
        /// <summary>
        /// Define an event message for an engine
        /// </summary>
        /// <param name="engine">Engine type</param>
        /// <param name="lineNumber">Line number error occurred</param>
        protected FileHelpersEventArgs(EngineBase engine, int lineNumber)
        {
            Engine = engine;
            LineNumber = lineNumber;
        }

        /// <summary> The engine that raise the event </summary>
        public EngineBase Engine { get; set; }

        /// <summary>The current line number.</summary>
        public int LineNumber { get; private set; }
    }
}