using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>
    /// Indicates the State of an engine
    /// </summary>
    public enum EngineState
    {
        /// <summary>The Engine is closed</summary>
        Closed = 0,
        /// <summary>The Engine is reading a file, string or stream</summary>
        Reading,
        /// <summary>The Engine is writing a file, string or stream</summary>
        Writing
    }
}