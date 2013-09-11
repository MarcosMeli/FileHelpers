using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace FileHelpers
{
    /// <summary>
    /// Define a field that is delimited, eg CSV and may be quoted
    /// </summary>
	public sealed class DelimitedField
        : FieldBase
	{

		#region "  Constructor  "

        private static readonly CompareInfo mCompare = StringHelper.CreateComparer();

        /// <summary>
        /// Create an empty delimited field structure
        /// </summary>
        private DelimitedField()
        {
        }

        /// <summary>
        /// Create a delimited field with defined separator
        /// </summary>
        /// <param name="fi">field info structure</param>
        /// <param name="sep">field separator</param>
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

        /// <summary>
        /// Set the separator string
        /// </summary>
        /// <remarks>Also sets the discard count</remarks>
		public string Separator
		{
			get { return mSeparator; }
			internal set
			{
				mSeparator = value;

				if (IsLast && IsArray == false)
					CharsToDiscard = 0;
				else
					CharsToDiscard = mSeparator.Length;
			}
		}

        /// <summary>
        /// allow a quoted multiline format
        /// </summary>
        public MultilineMode QuoteMultiline { get; internal set; }

        /// <summary>
        /// whether quotes are optional for read and / or write
        /// </summary>
        public QuoteMode QuoteMode { get; internal set; }

        /// <summary>
        /// quote character around field (and repeated within it)
        /// </summary>
        public char QuoteChar { get; internal set; }

	    #endregion

		#region "  Overrides String Handling  "

        /// <summary>
        /// Extract the field from the delimited file, removing separators and quotes
        /// and any duplicate quotes within the record
        /// </summary>
        /// <param name="line">line containing record input</param>
        /// <returns>Extract information</returns>
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
					var res = StringHelper.ExtractQuotedString(line, QuoteChar, QuoteMultiline == MultilineMode.AllowForBoth || QuoteMultiline == MultilineMode.AllowForRead);

                    if (TrimMode == TrimMode.Both || TrimMode == TrimMode.Right)
                    {
                        line.TrimStart(TrimChars);
                    }

                    if (!IsLast && !line.StartsWith(Separator) && !line.IsEOL())
                        throw new BadUsageException(line, "The field "+ this.FieldInfo.Name + " is quoted but the quoted char: " + quotedStr + " not is just before the separator (You can use [FieldTrim] to avoid this error)");
				    return res;
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

			if (IsLast && !IsArray)
            {
                var sepPos = line.IndexOf(mSeparator);

                if (sepPos == -1)
                    return new ExtractedInfo(line);

                // Now check for one extra separator
                var msg = string.Format("Delimiter '{0}' found after the last field '{1}' (the file is wrong or you need to add a field to the record class)", mSeparator, this.FieldInfo.Name, line.mReader.LineNumber);

                throw new BadUsageException(line.mReader.LineNumber, line.mCurrentPos, msg);
			}
			else
			{
			    int sepPos = line.IndexOf(mSeparator);

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
						sepPos = line.mLineStr.Length;
				}

				return new ExtractedInfo(line, sepPos);
			}
		}

        /// <summary>
        /// Output the field string adding delimiters and any required quotes
        /// </summary>
        /// <param name="sb">buffer to add field to</param>
        /// <param name="fieldValue">value object to add</param>
        /// <param name="isLast">Indicates if we are processing last field</param>
        internal override void CreateFieldString(StringBuilder sb, object fieldValue, bool isLast)
		{
			string field = base.CreateFieldString(fieldValue);

            bool hasNewLine = mCompare.IndexOf(field, StringHelper.NewLine, CompareOptions.Ordinal) >= 0;

			// If have a new line and this is not allowed.  We throw an exception
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

            if (isLast == false)
				sb.Append(mSeparator);
		}

        /// <summary>
        /// create a field base class and populate the delimited values
        /// base class will add its own values
        /// </summary>
        /// <returns>fieldbase ready to be populated with extra info</returns>
	    protected override FieldBase CreateClone()
	    {
	        var res = new DelimitedField
	            {
	                mSeparator = mSeparator,
	                QuoteChar = QuoteChar,
	                QuoteMode = QuoteMode,
	                QuoteMultiline = QuoteMultiline
	            };
            return res;
	    }

	    #endregion
	}
}