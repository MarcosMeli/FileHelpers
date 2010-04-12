

using System;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace FileHelpers
{
	internal sealed class DelimitedField 
        : FieldBase
	{

		#region "  Constructor  "

        private static readonly CompareInfo mCompare = StringHelper.CreateComparer();

        private DelimitedField()
            : base()
        {
        }

	    internal DelimitedField(FieldInfo fi, string sep)
            : base(fi)
	    {
	        QuoteChar = '\0';
	        QuoteMultiline = MultilineMode.AllowForBoth;
	        Separator = sep; // string.Intern(sep);
	    }

	    #endregion

		#region "  Properties  "

		private string mSeparator;

		internal string Separator
		{
			get { return mSeparator; }
			set
			{
				mSeparator = value;

				if (IsLast && IsArray == false)
					CharsToDiscard = 0;
				else
					CharsToDiscard = mSeparator.Length;
			}
		}

        internal MultilineMode QuoteMultiline { get; set; }
        internal QuoteMode QuoteMode { get; set; }
        internal char QuoteChar { get; set; }

	    #endregion

		#region "  Overrides String Handling  "
         

		internal override ExtractedInfo ExtractFieldString(LineInfo line)
		{
			if (IsOptional && line.IsEOL() )
				return ExtractedInfo.Empty;
            
			if (QuoteChar == '\0')
				return BasicExtractString(line);
			else
			{
				if (TrimMode == TrimMode.Both || TrimMode == TrimMode.Left)
				{
					line.TrimStart(TrimChars);
				}

				string quotedStr = QuoteChar.ToString();
				if (line.StartsWith(quotedStr))
				{
					return StringHelper.ExtractQuotedString(line, QuoteChar, QuoteMultiline == MultilineMode.AllowForBoth || QuoteMultiline == MultilineMode.AllowForRead);
				}
				else
				{
					if (QuoteMode == QuoteMode.OptionalForBoth || QuoteMode == QuoteMode.OptionalForRead)
						return BasicExtractString(line);
					else if (line.StartsWithTrim(quotedStr))
						throw new BadUsageException(string.Format("The field '{0}' has spaces before the QuotedChar at line {1}. Use the TrimAttribute to by pass this error. Field String: {2}", FieldInfo.Name, line.mReader.LineNumber, line.CurrentString));
					else
						throw new BadUsageException(string.Format("The field '{0}' not begin with the QuotedChar at line {1}. You can use FieldQuoted(QuoteMode.OptionalForRead) to allow optional quoted field.. Field String: {2}", FieldInfo.Name, line.mReader.LineNumber, line.CurrentString));
				}

			}


		}

		private ExtractedInfo BasicExtractString(LineInfo line)
		{
		
			if (IsLast && ! IsArray)
				return new ExtractedInfo(line);
			else
			{
				int sepPos;

				sepPos = line.IndexOf(mSeparator);

				if (sepPos == -1)
				{
                    if (IsLast && IsArray)
                        return new ExtractedInfo(line);

					if (NextIsOptional == false)
					{
						string msg;

						if (IsFirst && line.EmptyFromPos())
							msg = string.Format("The line {0} is empty. Maybe you need to use the attribute [IgnoreEmptyLines] in your record class.", line.mReader.LineNumber);
						else
							msg = string.Format("Delimiter '{0}' not found after field '{1}' (the record has less fields, the delimiter is wrong or the next field must be marked as optional).", mSeparator, this.FieldInfo.Name, line.mReader.LineNumber);

						throw new FileHelpersException(line.mReader.LineNumber, line.mCurrentPos, msg);

					}
					else
						sepPos = line.mLine.Length;
				}

				return new ExtractedInfo(line, sepPos);
			}
		}

        internal override void CreateFieldString(StringBuilder sb, object fieldValue)
		{
			string field = base.CreateFieldString(fieldValue);

            bool hasNewLine = mCompare.IndexOf(field, StringHelper.NewLine, CompareOptions.Ordinal) >= 0;

			// If have a new line and this is not allowed throw an exception
			if (hasNewLine &&
				(QuoteMultiline == MultilineMode.AllowForRead || 
				 QuoteMultiline == MultilineMode.NotAllow))
				throw new BadUsageException("One value for the field " + this.FieldInfo.Name + " has a new line inside. To allow write this value you must add a FieldQuoted attribute with the multiline option in true.");

			// Add Quotes If:
			//     -  optional == false
			//     -  is optional and contains the separator 
			//     -  is optional and contains a new line

			if ((QuoteChar != '\0') && 
				(QuoteMode == QuoteMode.AlwaysQuoted || 
					QuoteMode == QuoteMode.OptionalForRead || 
					( (QuoteMode == QuoteMode.OptionalForWrite || QuoteMode == QuoteMode.OptionalForBoth)  
					&& mCompare.IndexOf(field, mSeparator, CompareOptions.Ordinal) >= 0) || hasNewLine))
				StringHelper.CreateQuotedString(sb, field, QuoteChar);
			else
				sb.Append(field);

			if (IsLast == false)
				sb.Append(mSeparator);
		}

	    protected override FieldBase CreateClone()
	    {
	        var res = new DelimitedField();
	        res.mSeparator = mSeparator;
	        res.QuoteChar = QuoteChar;
            res.QuoteMode = QuoteMode;
	        res.QuoteMultiline = QuoteMultiline;
	        return res;
	    }

	    #endregion
	}
}