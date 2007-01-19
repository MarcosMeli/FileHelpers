using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace FileHelpers
{
	internal sealed class ForwardReader
	{
		TextReader mReader;

		private string[] mFowardStrings;
		private int mForwardIndex = 0;
		
		internal char[] mEOF = StringHelper.NewLine.ToCharArray();
		internal int mCapacityHint = 64;

		private int mRemaingLines = 0;

		public int RemainingLines
		{
			get { return mRemaingLines; }
		}
		
		private int mLineNumber = 0;

		public int LineNumber
		{
			get {return mLineNumber - 1;}
		}
		
		
		int mPos = 0;
		int MaxRecordSize = 1024 * 8;
		char[] mBuffer;
		
		internal ForwardReader(TextReader reader)
			: this(reader, 0, 0)
		{
		}

		internal ForwardReader(TextReader reader, int forwardLines): 
			this(reader, forwardLines, 0)
		{
		}

		internal ForwardReader(TextReader reader, int forwardLines, int startLine)
		{
			mReader = reader;
			
			mFowardLines = forwardLines;
			mLineNumber = startLine;

			mFowardStrings = new string[mFowardLines + 1];
			mRemaingLines = mFowardLines + 1;

			for (int i = 0; i < mFowardLines + 1; i++)
			{
				mFowardStrings[i] = mReader.ReadLine();
				mLineNumber++;
				if (mFowardStrings[i] == null)
				{
					mRemaingLines = i;
					break;
				}
			}

		}

		
//		public string ReadToDelimiter(string del)
//		{
//			//StringBuilder builder = new StringBuilder(mCapacityHint);
//
//			int right = mPos;
//			while (true)
//			{
//				mReader.
//				
//				//mReader.Read()
//				
//			}
//			
//			
//			
//			if (builder.Length > 0)
//			{
//				return builder.ToString();
//			}
//			return null;
//		}

		private bool mDiscardForward = false;

		public bool DiscardForward
		{
			get { return mDiscardForward; }
			set { mDiscardForward = value; }
		}

		private int mFowardLines = 0;

		public int FowardLines
		{
			get { return mFowardLines; }
		}

		public string ReadNextLine()
		{
			if (mRemaingLines <= 0)
				return null;
			else
			{
				string res = mFowardStrings[mForwardIndex];

				if (mRemaingLines == (mFowardLines + 1))
				{
					mFowardStrings[mForwardIndex] = mReader.ReadLine();
					mLineNumber++;

					if (mFowardStrings[mForwardIndex] == null)
					{
						mRemaingLines--;
					}

				}
				else
				{
					mRemaingLines--;
					if (mDiscardForward)
						return null;

				}

				mForwardIndex = (mForwardIndex + 1)%(mFowardLines + 1);

				return res;
			}

		}

		public string RemainingText
		{
			get
			{
				StringBuilder sb = new StringBuilder(100);

				for (int i = 0; i < mRemaingLines + 1; i++)
				{
					sb.Append(mFowardStrings[(mForwardIndex + i)%(mFowardLines + 1)] + StringHelper.NewLine);
				}

				return sb.ToString();
			}
		}


		public void Close()
		{
			mReader.Close();
		}

	}

	internal sealed class LineInfo
	{
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
		
		public string CurrentString
		{
			get
			{
				return new string(mLine, mCurrentPos, mLine.Length - mCurrentPos);
			}
		}
		
		public LineInfo(string line)
		{
			mLineStr = line;
			mLine = mLineStr.ToCharArray();
			mCurrentPos = 0;
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