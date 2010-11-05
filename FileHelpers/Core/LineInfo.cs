

using System;
using System.Diagnostics;
using System.Globalization;

namespace FileHelpers
{

    [DebuggerDisplay("{DebuggerDisplayStr()}")]
    internal sealed class LineInfo
	{
        //public static readonly LineInfo Empty = new LineInfo(string.Empty);
		#region "  Constructor  "

		//static readonly char[] mEmptyChars = new char[] {};

		public LineInfo(string line)
		{
            mReader = null;
			mLineStr = line;
			//mLine = line == null ? mEmptyChars : line.ToCharArray();
			mCurrentPos = 0;
		}

		#endregion

        public string Substring(int from, int count)
        {
            return mLineStr.Substring(from, count);
        }
		#region "  Internal Fields  "

		//internal string  mLine;
		//internal char[] mLine;
		internal string mLineStr;
		internal ForwardReader mReader;
		internal int mCurrentPos;
		
		private static readonly char[] WhitespaceChars = new char[] 
			{ 
				'\t', '\n', '\v', '\f', '\r', ' ', '\x00a0', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', 
				'\u2009', '\u200a', '\u200b', '\u3000', '\ufeff'
			};

		#endregion

        private string DebuggerDisplayStr()
        {
            if (IsEOL())
                return "<EOL>";
            else
                return CurrentString;
        }

		public string CurrentString
		{
			get
			{
				return mLineStr.Substring(mCurrentPos, mLineStr.Length - mCurrentPos);
			}
		}
		
		public bool IsEOL()
		{
			return mCurrentPos >= mLineStr.Length;
		}

		public int CurrentLength
		{
			get
			{
				return mLineStr.Length - mCurrentPos;
			}
		}

		public bool EmptyFromPos()
		{
			// Chek if the chars at pos or right are empty ones
			int length = mLineStr.Length;
			int pos = mCurrentPos;
			while(pos < length && Array.BinarySearch(WhitespaceChars, mLineStr[pos]) >= 0)
			{
				pos++;
			}
			
			return pos >= length;
		}

		public void TrimStart()
		{
			TrimStartSorted(WhitespaceChars);
		}

		public void TrimStart(char[] toTrim)
		{
			Array.Sort(toTrim);
			TrimStartSorted(toTrim);
		}

		private void TrimStartSorted(char[] toTrim)
		{
			// Move the pointer to the first non to Trim char
			int length = mLineStr.Length;
			
			while(mCurrentPos < length && Array.BinarySearch(toTrim, mLineStr[mCurrentPos]) >= 0)
			{
				mCurrentPos++;
			}
		}
		
		public bool StartsWith(string str)
		{
			// Returns true if the string begin with str
			if (mCurrentPos >= mLineStr.Length)
				return false;
			else

				return mCompare.Compare(mLineStr, mCurrentPos, str.Length, str, 0, str.Length, CompareOptions.OrdinalIgnoreCase) == 0;
		}

		public bool StartsWithTrim(string str)
		{
			int length = mLineStr.Length;
			int pos = mCurrentPos;

			while(pos < length && Array.BinarySearch(WhitespaceChars, mLineStr[pos]) >= 0)
			{
				pos++;
			}
			

			return mCompare.Compare(mLineStr, pos, str, 0, CompareOptions.OrdinalIgnoreCase) == 0;
		}

		public void ReadNextLine()
		{
			mLineStr = mReader.ReadNextLine();
            //mLine = mLineStr.ToCharArray();
			
			mCurrentPos = 0;
		}
		
		private static readonly CompareInfo mCompare = StringHelper.CreateComparer();

		
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

		internal void ReLoad(string line)
		{
			//mLine = line == null ? mEmptyChars : line.ToCharArray();
			mLineStr = line;
			mCurrentPos = 0;
		}
	}

}
