using System;
using System.Collections;
using System.Collections.Generic;
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
        /// Length of the field
        /// </summary>
        public int Length
		{
			get { return ExtractedTo - ExtractedFrom + 1;}
		}

        /// <summary>
        /// Contains the line of data read
        /// </summary>
        public LineInfo mLine;

        /// <summary>
        /// Position of first character of the field in mLine.mLine
        /// </summary>
        public int ExtractedFrom;

        /// <summary>
        /// Position of last character of the field in mLine.mLine
        /// </summary>
        public int ExtractedTo;

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
		
        //public void TrimStart(char[] sortedToTrim)
        //{
        //    if (mCustomExtractedString != null)
        //        mCustomExtractedString = mCustomExtractedString.TrimStart(sortedToTrim);
        //    else
        //        while (ExtractedFrom <= ExtractedTo &&
        //               Array.BinarySearch(sortedToTrim, mLine.mLine[ExtractedFrom]) >= 0)
        //            ExtractedFrom++;
        //}

        //public void TrimEnd(char[] sortedToTrim)
        //{
        //    if (mCustomExtractedString != null)
        //        mCustomExtractedString = mCustomExtractedString.TrimEnd(sortedToTrim);
        //    else
        //        while (ExtractedTo >= ExtractedFrom && 
        //               Array.BinarySearch(sortedToTrim, mLine.mLine[ExtractedTo]) >= 0)
        //            ExtractedTo--;
        //}

        //public void TrimBoth(char[] sortedToTrim)
        //{
        //    if (mCustomExtractedString != null)
        //        mCustomExtractedString = mCustomExtractedString.Trim(sortedToTrim);
        //    else
        //    {
        //        while(ExtractedFrom <= ExtractedTo && Array.BinarySearch(sortedToTrim, mLine.mLine[ExtractedFrom]) >= 0)
        //        {
        //            ExtractedFrom++;
        //        }
			
        //        while(ExtractedTo >= ExtractedFrom && Array.BinarySearch(sortedToTrim, mLine.mLine[ExtractedTo]) >= 0)
        //        {
        //            ExtractedTo--;
        //        }
        //    }
        //}

		internal static readonly ExtractedInfo Empty = new ExtractedInfo(string.Empty);

        /// <summary>
        /// Check that the value returned only has these specific characters in it
        /// </summary>
        /// <param name="sortedArray">Sorted array of characters to check</param>
        /// <returns>false if any character in value does not match</returns>
        public bool HasOnlyThisChars(char[] sortedArray)
		{
			// Check if the chars at pos or right are empty ones
			if (mCustomExtractedString != null)
			{
				int pos = 0;
				while ( pos <  mCustomExtractedString.Length)
				{
                    if (Array.BinarySearch(sortedArray, mCustomExtractedString[pos]) < 0)
                        return false;
					pos++;
				}
			
				return true;
			}
			else
			{

				int pos = ExtractedFrom;
				while(pos <= ExtractedTo) 
				{
                    if (Array.BinarySearch(sortedArray, mLine.mLineStr[pos]) < 0)
                        return false;
					pos++;
				}
			
				return true;
			}
		}
	}
}
