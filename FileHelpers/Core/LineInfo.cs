using System;
using System.Diagnostics;
using System.Globalization;

namespace FileHelpers
{
    /// <summary>
    /// Record read from the file for processing
    /// </summary>
    /// <remarks>
    /// The data inside the LIneInfo may be reset during processing,
    /// for example on read next line.  Do not rely on this class
    /// containing all data from a record for all time.  It is designed
    /// to read data sequentially.
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplayStr()}")]
    internal sealed class LineInfo
    {
        //public static readonly LineInfo Empty = new LineInfo(string.Empty);

        #region "  Constructor  "

        //static readonly char[] mEmptyChars = new char[] {};
        /// <summary>
        /// Create a line info with data from line
        /// </summary>
        /// <param name="line"></param>
        public LineInfo(string line)
        {
            mReader = null;
            mLineStr = line;
            //mLine = line == null ? mEmptyChars : line.ToCharArray();
            mCurrentPos = 0;
        }

        #endregion

        /// <summary>
        /// Return part of line,  Substring
        /// </summary>
        /// <param name="from">Start position (zero offset)</param>
        /// <param name="count">Number of characters to extract</param>
        /// <returns>substring from line</returns>
        public string Substring(int from, int count)
        {
            return mLineStr.Substring(from, count);
        }

        #region "  Internal Fields  "

        //internal string  mLine;
        //internal char[] mLine;

        /// <summary>
        /// Record read from reader
        /// </summary>
        internal string mLineStr;

        /// <summary>
        /// Reader that got the string
        /// </summary>
        internal ForwardReader mReader;

        /// <summary>
        /// Where we are processing records from
        /// </summary>
        internal int mCurrentPos;

        /// <summary>
        /// List of whitespace characters that we want to skip
        /// </summary>
       
        internal static readonly char[] WhitespaceChars = new char[] {
            '\t', '\n', '\v', '\f', '\r', ' ', '\x00a0', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005',
            '\u2006', '\u2007', '\u2008',
            '\u2009', '\u200a', '\u200b', '\u3000', '\ufeff'
        };

        #endregion

        /// <summary>
        /// Debugger display string
        /// </summary>
        /// <returns></returns>
        private string DebuggerDisplayStr()
        {
            if (IsEOL())
                return "<EOL>";
            else
                return CurrentString;
        }

        /// <summary>
        /// Extract a single field from the system
        /// </summary>
        public string CurrentString
        {
            get { return mLineStr.Substring(mCurrentPos, mLineStr.Length - mCurrentPos); }
        }

        /// <summary>
        /// If we have extracted more that the field contains.
        /// </summary>
        /// <returns>True if end of line</returns>
        public bool IsEOL()
        {
            return mCurrentPos >= mLineStr.Length;
        }

        /// <summary>
        /// Amount of data left to process
        /// </summary>
        public int CurrentLength
        {
            get { return mLineStr.Length - mCurrentPos; }
        }

        /// <summary>
        /// Is there only whitespace left in the record?
        /// </summary>
        /// <returns>True if only whitespace</returns>
        public bool EmptyFromPos()
        {
            // Check if the chars at pos or right are empty ones
            int length = mLineStr.Length;
            int pos = mCurrentPos;
            while (pos < length &&
                   Array.BinarySearch(WhitespaceChars, mLineStr[pos]) >= 0)
                pos++;

            return pos >= length;
        }

        /// <summary>
        /// Delete any of these characters from beginning of the record
        /// </summary>
        /// <param name="toTrim"></param>
        public void TrimStart(char[] toTrim)
        {
            Array.Sort(toTrim);
            TrimStartSorted(toTrim);
        }

        /// <summary>
        /// Move the record pointer along skipping these characters
        /// </summary>
        /// <param name="toTrim">Sorted array of character to skip</param>
        private void TrimStartSorted(char[] toTrim)
        {
            // Move the pointer to the first non to Trim char
            int length = mLineStr.Length;

            while (mCurrentPos < length &&
                   Array.BinarySearch(toTrim, mLineStr[mCurrentPos]) >= 0)
                mCurrentPos++;
        }

        public bool StartsWith(string str)
        {
            // Returns true if the string begin with str
            if (mCurrentPos >= mLineStr.Length)
                return false;
            else {
                return
                    mCompare.Compare(mLineStr,
                        mCurrentPos,
                        str.Length,
                        str,
                        0,
                        str.Length,
                        CompareOptions.OrdinalIgnoreCase) == 0;
            }
        }

        /// <summary>
        /// Check that the record begins with a value ignoring whitespace
        /// </summary>
        /// <param name="str">String to check for</param>
        /// <returns>True if record begins with</returns>
        public bool StartsWithTrim(string str)
        {
            int length = mLineStr.Length;
            int pos = mCurrentPos;

            while (pos < length &&
                   Array.BinarySearch(WhitespaceChars, mLineStr[pos]) >= 0)
                pos++;


            return mCompare.Compare(mLineStr, pos, str, 0, CompareOptions.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// get The next line from the system and reset the line pointer to zero
        /// </summary>
        public void ReadNextLine()
        {
            mLineStr = mReader.ReadNextLine();
            //mLine = mLineStr.ToCharArray();

            mCurrentPos = 0;
        }

        private static readonly CompareInfo mCompare = StringHelper.CreateComparer();

        /// <summary>
        /// Find the location of the next string in record
        /// </summary>
        /// <param name="foundThis">String we are looking for</param>
        /// <returns>Position of the next one</returns>
        public int IndexOf(string foundThis)
        {
            // Bad performance with custom IndexOf
            //			if (foundThis.Length == 1)
            //			{
            //				char delimiter = foundThis[0];
            //				int pos = mCurrentPos;
            //				int length = mLine.Length;
            //			
            //				while (pos < length)
            //				{
            //					if (mLine[pos] == delimiter)
            //						return pos;
            //				
            //					pos++;
            //				}
            //				return -1;
            //			}
            //			else
//			if (mLineStr == null)
//				return -1;
//			else
            return mCompare.IndexOf(mLineStr, foundThis, mCurrentPos, CompareOptions.Ordinal);
        }

        /// <summary>
        /// Reset the string back to the original line and reset the line pointer
        /// </summary>
        /// <remarks>If the input is multi line, this will read next record and remove the original data</remarks>
        /// <param name="line">Line to use</param>
        internal void ReLoad(string line)
        {
            //mLine = line == null ? mEmptyChars : line.ToCharArray();
            mLineStr = line;
            mCurrentPos = 0;
        }
    }
}