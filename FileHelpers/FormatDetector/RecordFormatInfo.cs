using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers.Dynamic;

namespace FileHelpers.Detection
{
    /// <summary>
    /// Describes the format of a record
    /// </summary>
    public sealed class RecordFormatInfo
    {
        /// <summary>
        /// A number between 0 to 100 that indicates how acurate is the Record Format
        /// </summary>
        internal int mConfidence = 0;

        /// <summary>
        /// A number between 0 to 100 that indicates how acurate is the Record Format
        /// </summary>
        public int Confidence
        {
            get { return mConfidence; }
        }
        
    }
}