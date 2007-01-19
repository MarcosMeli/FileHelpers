#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Globalization;

namespace FileHelpers
{
	
	internal sealed class LineInfo
	{
		#region "  Constructor  "

		public LineInfo(string line)
		{
			mLineStr = line;
			mLine = mLineStr.ToCharArray();
			mCurrentPos = 0;
		}

		#endregion

		#region "  Internal Fields  "

		//internal string  mLine;
		internal char[] mLine;
		internal string mLineStr;
		internal ForwardReader mReader;
		internal int mCurrentPos;
		
		private static char[] WhitespaceChars = new char[] 
			{ 
				'\t', '\n', '\v', '\f', '\r', ' ', '\x00a0', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', 
				'\u2009', '\u200a', '\u200b', '\u3000', '\ufeff'
			};

		#endregion

		public string CurrentString
		{
			get
			{
				return new string(mLine, mCurrentPos, mLine.Length - mCurrentPos);
			}
		}
		
		public bool IsEOL()
		{
			return mCurrentPos == mLine.Length;
		}

		public int CurrentLength
		{
			get
			{
				return mLine.Length - mCurrentPos;
			}
		}

		public bool EmptyFromPos()
		{
			// Chek if the chars at pos or right are empty ones
			int length = mLine.Length;
			int pos = mCurrentPos;
			while(pos < length && Array.BinarySearch(WhitespaceChars, mLine[pos]) >= 0)
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
			int length = mLine.Length;
			
			while(mCurrentPos < length && Array.BinarySearch(toTrim, mLine[mCurrentPos]) >= 0)
			{
				mCurrentPos++;
			}
		}
		
		public bool StartsWith(string str)
		{
			// Returns true if the string begin with str
			return mCompare.Compare(mLineStr, mCurrentPos, str, 0, CompareOptions.IgnoreCase) == 0;
		}

		public bool StartsWithTrim(string str)
		{
			int length = mLine.Length;
			int pos = mCurrentPos;

			while(pos < length && Array.BinarySearch(WhitespaceChars, mLine[pos]) >= 0)
			{
				pos++;
			}
			
			return mCompare.Compare(mLineStr, pos, str, 0, CompareOptions.IgnoreCase) == 0;
		}

		public void ReadNextLine()
		{
			mLineStr = mReader.ReadNextLine();
			mLine = mLineStr.ToCharArray();
			
			mCurrentPos = 0;
		}
		
		private static CompareInfo mCompare = CultureInfo.InvariantCulture.CompareInfo;

		
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
			return mCompare.IndexOf(mLineStr, foundThis, mCurrentPos, CompareOptions.IgnoreCase);
		}

		internal void ReLoad(string line)
		{
			mLine = line.ToCharArray();
			mLineStr = line;
			mCurrentPos = 0;
		}
	}

}
