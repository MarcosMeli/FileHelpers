#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Reflection;

namespace FileHelpers
{
	internal sealed class DelimitedField : FieldBase
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

		#region "  QuoteOptional  "

		internal QuoteMode mQuoteMode;

		public QuoteMode QuoteMode
		{
			get { return mQuoteMode; }
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

		protected override ExtractedInfo ExtractFieldString(string from)
		{

			if (mIsOptional && from.Length == 0 )
				return ExtractedInfo.Empty;

			ExtractedInfo res;

			if (mQuoteChar == '\0')
			{
				if (mIsLast)
					res = new ExtractedInfo(from);
				else
				{
					int sepPos = from.IndexOf(this.mSeparator);

					if (sepPos == -1)
					{
						if (this.mNextIsOptional == false)
							throw new FileHelperException("The separator '" + this.mSeparator + "' can´t be found after the field '" + this.FieldInfo.Name + "' (the record has less fields, the separator is wrong or the next field must be marked as optional).");
						else
							sepPos = from.Length;
					}

					res = new ExtractedInfo(from.Substring(0, sepPos));
				}
			}
			else
			{
				string quotedStr = mQuoteChar.ToString();
				res = new ExtractedInfo();
				res.CharsRemoved = 0;

				string from2 = from;
				if ((mTrimMode == TrimMode.Both || mTrimMode == TrimMode.Left))
				{
					from2 = from.TrimStart(mTrimChars);
					res.CharsRemoved = from2.Length - from.Length;
				}

				if (from2.StartsWith(quotedStr))
				{
					int index;
					res.ExtractedString = StringHelper.ExtractQuotedString(from2, mQuoteChar, out index);
					res.CharsRemoved += index;

//					if ((mTrimMode == TrimMode.Both || mTrimMode == TrimMode.Right))
//					{
//						string from2 = from.Substring(res.CharsRemoved).TrimStart(mTrimChars);
//						res.CharsRemoved += from2.Length - from.Length;
//					}

				}
				else
				{
					if (mQuoteMode == QuoteMode.OptionalBoth || mQuoteMode == QuoteMode.OptionalRead)
					{
						if (mIsLast)
							res = new ExtractedInfo(from);
						else
						{
							int sepPos = from.IndexOf(this.mSeparator);

							if (sepPos == -1)
							{
								if (this.mNextIsOptional == false)
									throw new FileHelperException("The separator '" + this.mSeparator + "' can´t be found after the field '" + this.FieldInfo.Name + "' (the record has less fields, the separator is wrong or the next field must be marked as optional).");
								else
									sepPos = from.Length;
							}

							res = new ExtractedInfo(from.Substring(0, sepPos));
						}
					}
					else if (from.Trim().StartsWith(quotedStr))
						throw new BadUsageException("The field '" + this.FieldInfo.Name + "' has spaces before the QuotedChar in the data use the TrimAttribute to by pass this error. Field String: " + from);
					else
						throw new BadUsageException("The field '" + this.FieldInfo.Name + "' not begin with the QuotedChar in the data. You can use and FieldQuoted(.., true) to allow optional quote.. Field String: " + from);
				}

			}


			return res;
		}

		protected override string CreateFieldString(object record)
		{
			string res;

			res = base.CreateFieldString(record);

			if (mQuoteChar != '\0')
			{
				// Add Quotes If:
				//     -  optional == false
				//     -  is optional and contains the separator 
				//     -  is optional and contains a new line

				#if ! MINI
					if (mQuoteMode == QuoteMode.Allways || mQuoteMode == QuoteMode.OptionalRead || res.IndexOf(mSeparator) >= 0 || res.IndexOf(Environment.NewLine) >= 0)
						res = StringHelper.CreateQuotedString(res, mQuoteChar);
				#else
					if (mQuoteMode == QuoteMode.Allways || mQuoteMode == QuoteMode.OptionalRead || res.IndexOf(mSeparator) >= 0 || res.IndexOf("\r\n") >= 0)
						res = StringHelper.CreateQuotedString(res, mQuoteChar);
				#endif
			}

			if (mIsLast == false)
				res += mSeparator;

			return res;
		}

		#endregion
	}
}