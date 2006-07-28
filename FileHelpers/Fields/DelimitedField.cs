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

		internal string mSeparator;
		internal int mSeparatorLength;

		internal char mQuoteChar = '\0';
		internal QuoteMode mQuoteMode;
		internal MultilineMode  mQuoteMultiline = MultilineMode.AllowForBoth;

		#endregion

		#region "  Overrides String Handling  "

		protected override int CharsToDiscard()
		{
			if (mIsLast)
				return 0;
			else
				return mSeparatorLength;
		}

		protected override ExtractedInfo ExtractFieldString(string from, ForwardReader reader)
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
						{
							string msg = null;

							if (mIsFirst && from.Trim() == string.Empty)
								msg = "The line " + reader.LineNumber.ToString() + " is empty. Maybe you need to use the attribute [IgnoreEmptyLines] in your record class.";
							else
								msg = "The delimiter '" + this.mSeparator + "' can´t be found after the field '" + this.mFieldInfo.Name + "' (the record has less fields, the delimiter is wrong or the next field must be marked as optional).";
							
							throw new FileHelperException(msg);

						}
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
				if (mTrimMode == TrimMode.Both || mTrimMode == TrimMode.Left)
				{
					from2 = from.TrimStart(mTrimChars);
					res.CharsRemoved = from2.Length - from.Length;
				}

				if (from2.StartsWith(quotedStr))
				{
					if (mQuoteMultiline == MultilineMode.AllowForBoth || mQuoteMultiline == MultilineMode.AllowForRead)
					{
						ExtractedInfo ei = StringHelper.ExtractQuotedString(from2, reader, mQuoteChar);
						res.ExtractedString = ei.ExtractedString;
						res.CharsRemoved += ei.CharsRemoved;
						res.ExtraLines = ei.ExtraLines;
						res.NewRestOfLine = ei.NewRestOfLine;
					}
					else
					{
						int index = 0;
						
						res.ExtractedString = StringHelper.ExtractQuotedString(from2, mQuoteChar, out index);
						res.CharsRemoved += index;
					}


				}
				else
				{
					if (mQuoteMode == QuoteMode.OptionalForBoth || mQuoteMode == QuoteMode.OptionalForRead)
					{
						if (mIsLast)
							res = new ExtractedInfo(from);
						else
						{
							int sepPos = from.IndexOf(this.mSeparator);

							if (sepPos == -1)
							{
								if (this.mNextIsOptional == false)
									throw new FileHelperException("The separator '" + this.mSeparator + "' can´t be found after the field '" + this.mFieldInfo.Name + "' (the record has less fields, the separator is wrong or the next field must be marked as optional).");
								else
									sepPos = from.Length;
							}

							res = new ExtractedInfo(from.Substring(0, sepPos));
						}
					}
					else if (from.Trim().StartsWith(quotedStr))
						throw new BadUsageException("The field '" + this.mFieldInfo.Name + "' has spaces before the QuotedChar in the data use the TrimAttribute to by pass this error. Field String: " + from);
					else
						throw new BadUsageException("The field '" + this.mFieldInfo.Name + "' not begin with the QuotedChar in the data. You can use FieldQuoted(QuoteMode.OptionalForRead) to allow optional quoted field.. Field String: " + from);
				}

			}


			return res;
		}

		protected override string CreateFieldString(object record)
		{
			string res;

			res = base.CreateFieldString(record);

			bool hasNewLine = res.IndexOf(StringHelper.NewLine) >= 0;

			// If have a new line and this is not allowed throw an exception
			if (hasNewLine &&
				(mQuoteMultiline == MultilineMode.AllowForRead || 
				 mQuoteMultiline == MultilineMode.NotAllow))
				throw new BadUsageException("One value for the field " + this.mFieldInfo.Name + " has a new line inside. To allow write this value you must add a FieldQuoted attribute with the multiline option in true.");

			if (mQuoteChar != '\0')
			{
				// Add Quotes If:
				//     -  optional == false
				//     -  is optional and contains the separator 
				//     -  is optional and contains a new line

				if (mQuoteMode == QuoteMode.AlwaysQuoted || mQuoteMode == QuoteMode.OptionalForRead || ((mQuoteMode == QuoteMode.OptionalForWrite|| mQuoteMode == QuoteMode.OptionalForBoth)  && res.IndexOf(mSeparator) >= 0) || hasNewLine)
						res = StringHelper.CreateQuotedString(res, mQuoteChar);
			}

			if (mIsLast == false)
				res += mSeparator;

			return res;
		}

		#endregion
	}
}