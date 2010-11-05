

using System;
using System.Diagnostics;

namespace FileHelpers
{

    [DebuggerDisplay("{ExtractedString()} [{ExtractedFrom}-{ExtractedTo}]")]
	internal struct ExtractedInfo
	{

		internal string mCustomExtractedString;

		public string ExtractedString()
		{
			if (mCustomExtractedString == null)
				return mLine.Substring(ExtractedFrom, ExtractedTo - ExtractedFrom + 1);
			else
				return mCustomExtractedString;
		}

		public int Length
		{
			get { return ExtractedTo - ExtractedFrom + 1;}
		}

		public LineInfo mLine;
		public int ExtractedFrom;
		public int ExtractedTo;


		public ExtractedInfo(LineInfo line)
		{
			mLine = line;
			ExtractedFrom = line.mCurrentPos;
			ExtractedTo = line.mLineStr.Length - 1;
            mCustomExtractedString = null;
		}

		public ExtractedInfo(LineInfo line, int extractTo)
		{
			mLine = line;
			ExtractedFrom = line.mCurrentPos;
			ExtractedTo = extractTo - 1;
            mCustomExtractedString = null;
		}
		
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

		public bool HasOnlyThisChars(char[] sortedArray)
		{
			// Check if the chars at pos or right are empty ones
			if (mCustomExtractedString != null)
			{
				int pos = 0;
				while ( pos <  mCustomExtractedString.Length && 
						Array.BinarySearch(sortedArray,  mCustomExtractedString[pos]) >= 0 )
				{
					pos++;
				}
			
				return pos == mCustomExtractedString.Length;
			}
			else
			{

				int pos = ExtractedFrom;
				while(pos <= ExtractedTo && Array.BinarySearch(sortedArray, mLine.mLineStr[pos]) >= 0)
				{
					pos++;
				}
			
				return pos > ExtractedTo;
			}
		}
	}

}
