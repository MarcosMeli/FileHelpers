#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System.Reflection;

namespace FileHelpers.Fields
{
	internal class DelimitedField : FieldBase
	{
		#region "  Constructor  "

		internal DelimitedField(FieldInfo fi, string sep) : base(fi)
		{
			this.mSeparator = sep;
			this.mSeparatorLength = mSeparator.Length;
		}

		#endregion

		#region "  Properties  "

		private string mSeparator;
		private int mSeparatorLength;

		public int SeparatorLength
		{
			get { return mSeparatorLength; }
		}

		public string Separator
		{
			get { return mSeparator; }
		}

		#endregion

		#region "  QuoteChar  "

		internal char mQuoteChar = '\0';

		public char QuoteChar
		{
			get { return mQuoteChar; }
		}

		#endregion

		#region "  Overrides String Handling  "

		protected override int CharsToDiscard()
		{
			if (mIsLast)
				return 0;
			else
				return mSeparatorLength;
		}

		protected override ExtractInfo ExtractFieldString(string from)
		{
			ExtractInfo res;

			//from.StartsWith(mQuoteChar.ToString()) == false)

			if (mQuoteChar == '\0')
			{
				if (mIsLast)
					res = new ExtractInfo(from);
				else
				{
					int sepPos = from.IndexOf(this.mSeparator);
					
					if (sepPos == -1)
						throw new FileHelperException("The separator '" + this.mSeparator + "' can´t be found after the field '" + this.FieldInfo.Name + "' (the record has less fields or the separator is wrong).");

					res = new ExtractInfo(from.Substring(0, sepPos));
				}
			}
			else
			{
				string quotedStr = mQuoteChar.ToString();
				res = new ExtractInfo();
				res.CharsRemoved = 0;

				if ((mTrimMode == TrimMode.Both || mTrimMode == TrimMode.Left))
				{
					string from2 = from.TrimStart(mTrimChars);
					res.CharsRemoved = from2.Length - from.Length;
					from = from2;
				}

				if (from.StartsWith(quotedStr))
				{
					int index;
					res.ExtractedString = StringHelper.ExtractQuotedString(from, mQuoteChar, out index);
					res.CharsRemoved += index;

//					if ((mTrimMode == TrimMode.Both || mTrimMode == TrimMode.Right))
//					{
//						string from2 = from.Substring(res.CharsRemoved).TrimStart(mTrimChars);
//						res.CharsRemoved += from2.Length - from.Length;
//					}

				}
				else
				{
					if (from.Trim().StartsWith(quotedStr))
						throw new QuotedStringException("The current field has spaces before the QuotedChar use the TrimAttribute to by pass this error.", from);
					else
						throw new QuotedStringException("The current field not begin with the QuotedChar.", from);
				}

			}

//			if (mQuoteChar != '\0')
//			{
//			}

			return res;
		}

		protected override string CreateFieldString(object record)
		{
			string res;

			if (mQuoteChar == '\0')
				res = base.CreateFieldString(record);
			else
				res = StringHelper.CreateQuotedString(base.CreateFieldString(record), mQuoteChar);

			if (mIsLast == false)
				res += mSeparator;

			return res;
		}

		#endregion
	}
}