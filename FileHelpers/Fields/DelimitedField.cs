#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Globalization;
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

		private static CompareInfo mCompare = CultureInfo.InvariantCulture.CompareInfo;
		
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

		protected override ExtractedInfo ExtractFieldString(LineInfo line)
		{

			if (mIsOptional && line.mLine.Length == 0 )
				return ExtractedInfo.Empty;

			ExtractedInfo res = null;

			if (mQuoteChar == '\0')
			{
				//TODO: Check this, dont allow random data at the end of the file.
				if (mIsLast)
					res = new ExtractedInfo(line);
				else
				{
					int sepPos ;
					
					sepPos = line.IndexOf(mSeparator);

					if (sepPos == -1)
					{
						if (this.mNextIsOptional == false)
						{
							string msg = null;

							if (mIsFirst && line.EmptyFromPos())
								msg = "The line " + line.mReader.LineNumber.ToString() + " is empty. Maybe you need to use the attribute [IgnoreEmptyLines] in your record class.";
							else
								msg = "The delimiter '" + this.mSeparator + "' can´t be found after the field '" + this.mFieldInfo.Name + "' at line "+ line.mReader.LineNumber.ToString() + " (the record has less fields, the delimiter is wrong or the next field must be marked as optional).";
							
							throw new FileHelperException(msg);

						}
						else
							sepPos = line.mLine.Length - 1;
					}

					res = new ExtractedInfo(line, sepPos);
				}
				
				return res; 
			}
			else
			{
				#region "  UNIMPLEMENTED QUOTED  "
				
				// TODO: UnComment and Fix

//				string quotedStr = mQuoteChar.ToString();
//				res = new ExtractedInfo();
//				res.CharsRemoved = 0;
//
//			//	string from2 = from;
//				if (mTrimMode == TrimMode.Both || mTrimMode == TrimMode.Left)
//				{
//					int pos = line.mCurrentPos;
//					line.TrimStart(mTrimChars);
////					from2 = from.TrimStart(mTrimChars);
//					res.CharsRemoved = line.mCurrentPos - pos;
//				}
//
//				if (line.StartsWith(quotedStr))
//				{
//					if (mQuoteMultiline == MultilineMode.AllowForBoth || mQuoteMultiline == MultilineMode.AllowForRead)
//					{
//						ExtractedInfo ei = StringHelper.ExtractQuotedString(line, mQuoteChar);
//						res.ExtractedString = ei.ExtractedString;
//						res.CharsRemoved += ei.CharsRemoved;
//						res.ExtraLines = ei.ExtraLines;
//						res.NewRestOfLine = ei.NewRestOfLine;
//					}
//					else
//					{
//						int index = 0;
//						
//						res.ExtractedString = StringHelper.ExtractQuotedString(from2, mQuoteChar, out index);
//						res.CharsRemoved += index;
//					}
//
//
//				}
//				else
//				{
//					if (mQuoteMode == QuoteMode.OptionalForBoth || mQuoteMode == QuoteMode.OptionalForRead)
//					{
//						// TODO: Validate this, try to validate the last field too.
//						if (mIsLast)
//							res = new ExtractedInfo(line);
//						else
//						{
//							int sepPos = line.IndexOf(mSeparator);
//
//							if (sepPos == -1)
//							{
//								if (this.mNextIsOptional == false)
//									throw new FileHelperException("The separator '" + this.mSeparator + "' can´t be found after the field '" + this.mFieldInfo.Name + "' at line "+ line.mReader.LineNumber.ToString() + " (the record has less fields, the separator is wrong or the next field must be marked as optional).");
//								else
//									sepPos = line.CurrentLength - 1;
//							}
//
//							res = new ExtractedInfo(line, sepPos);
//						}
//					}
//					else if (line.StartsWithTrim(quotedStr))
//						throw new BadUsageException("The field '" + this.mFieldInfo.Name + "' has spaces before the QuotedChar at line "+ line.mReader.LineNumber.ToString() + ". Use the TrimAttribute to by pass this error. Field String: " + line.CurrentString);
//					else
//						throw new BadUsageException("The field '" + this.mFieldInfo.Name + "' not begin with the QuotedChar at line "+ line.mReader.LineNumber.ToString() + ". You can use FieldQuoted(QuoteMode.OptionalForRead) to allow optional quoted field.. Field String: " + line.CurrentString);
//				}
//
				#endregion
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