using System.Diagnostics;

namespace FileHelpers
{
    /// <summary>
    /// A single field extracted from the 'record'
    /// </summary>
    /// <remarks>
    /// Record is defined by the way the data is input
    /// </remarks>
    [DebuggerDisplay("{ExtractedString()} [{ExtractedFrom}-{ExtractedTo}]")]
    internal struct ExtractedInfo
    {
        /// <summary>
        /// Allows for the actual string to be overridden 
        /// </summary>
        internal string mCustomExtractedString;

        /// <summary>
        /// The string value of the field extracted from the record
        /// </summary>
        /// <returns></returns>
        public string ExtractedString()
        {
            if (mCustomExtractedString == null)
                return mLine.Substring(ExtractedFrom, ExtractedTo - ExtractedFrom + 1);
            else
                return mCustomExtractedString;
        }

        /// <summary>
        /// Contains the line of data read
        /// </summary>
        private readonly LineInfo mLine;

        /// <summary>
        /// Position of first character of the field in mLine.mLine
        /// </summary>
        public readonly int ExtractedFrom;

        /// <summary>
        /// Position of last character of the field in mLine.mLine
        /// </summary>
        public readonly int ExtractedTo;

        /// <summary>
        /// Extract the rest of the line into my variable
        /// </summary>
        /// <param name="line"></param>
        public ExtractedInfo(LineInfo line)
        {
            mLine = line;
            ExtractedFrom = line.mCurrentPos;
            ExtractedTo = line.mLineStr.Length - 1;
            mCustomExtractedString = null;
        }

        /// <summary>
        /// Extract field from current position to specified position
        /// </summary>
        /// <param name="line">Record information</param>
        /// <param name="extractTo">Position to extract to</param>
        public ExtractedInfo(LineInfo line, int extractTo)
        {
            mLine = line;
            ExtractedFrom = line.mCurrentPos;
            ExtractedTo = extractTo - 1;
            mCustomExtractedString = null;
        }

        /// <summary>
        /// Allow a default string or a specific string for this
        /// variable to be applied
        /// </summary>
        /// <param name="customExtract"></param>
        public ExtractedInfo(string customExtract)
        {
            mLine = null;
            ExtractedFrom = 0;
            ExtractedTo = 0;
            mCustomExtractedString = customExtract;
        }

        internal static readonly ExtractedInfo Empty = new ExtractedInfo(string.Empty);
    }
}