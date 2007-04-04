#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.ComponentModel;
using System.Text;

namespace FileHelpers
{

	internal sealed class StringHelper
	{
		private StringHelper()
		{
		}

		#if ! MINI
			internal static readonly string NewLine = Environment.NewLine;
		#else
			internal static readonly string NewLine = "\r\n";
		#endif



		#region "  ExtractQuotedString  "

		internal static ExtractedInfo ExtractQuotedString(LineInfo line, char quoteChar, bool allowMultiline)
		{
			//			if (line.mReader == null)
			//				throw new BadUsageException("The reader can´t be null");

			if (line.IsEOL())
				throw new BadUsageException("An empty String found and can be parsed like a QuotedString try to use SafeExtractQuotedString");

			if (line.mLine[line.mCurrentPos] != quoteChar)
				throw new BadUsageException("The source string not begins with the quote char: " + quoteChar);

			StringBuilder res = new StringBuilder(32);
			//int lines = 0;

			bool firstFound = false;

			int i = line.mCurrentPos + 1;
			//bool mustContinue = true;

			while (line.mLineStr != null)
			{
				while (i < line.mLine.Length)
				{
					if (line.mLine[i] == quoteChar)
					{
						if (firstFound == true)
						{
							// Is an escaped quoted char
							res.Append(quoteChar);
							firstFound = false;
						}
						else
						{
							firstFound = true;
						}
					}
					else
					{
						if (firstFound)
						{
							// This was the end of the string

							line.mCurrentPos = i;
							return new ExtractedInfo(res.ToString());
//							ExtractedInfo ei = ;
//							return ei;

						}
						else
						{
							res.Append(line.mLine[i]);
						}
					}
					i++;
				}


				if (firstFound)
				{
					line.mCurrentPos = i;
					return new ExtractedInfo(res.ToString());
				}
				else
				{
					if (allowMultiline == false)
						throw new BadUsageException("The current field has an UnClosed quoted string. Complete line: " + res.ToString());

					line.ReadNextLine();
					res.Append(StringHelper.NewLine);
					//lines++;
					i = 0;
				}
			}

			throw new BadUsageException("The current field has an unclosed quoted string. Complete Filed String: " + res.ToString());
		}

		#endregion

		#region "  CreateQuotedString  "

		internal static void CreateQuotedString(StringBuilder sb, string source, char quoteChar)
		{
			if (source == null) source = string.Empty;

			string quotedCharStr = quoteChar.ToString();
			string escapedString = source.Replace(quotedCharStr, quotedCharStr + quotedCharStr);

			sb.Append(quoteChar);
			sb.Append(escapedString);
			sb.Append(quoteChar);
		}

		#endregion

		#region "  RemoveBlanks  "

        internal static string RemoveBlanks(string source)
        {
            StringBuilder sb = null;
            int i = 0;

            while (i < source.Length && Char.IsWhiteSpace(source[i]))
            {
                i++;
            }

            if (i < source.Length && (source[i] == '+' || source[i] == '-'))
            {
                i++;
                while (i < source.Length && Char.IsWhiteSpace(source[i]))
                {
                    if (sb == null)
                        sb = new StringBuilder(source[i-1].ToString());

                    i++;
                }
            }

            if (sb == null)
                return source;
            else if (i < source.Length)
                sb.Append(source.Substring(i));

            return sb.ToString();
        }

		#endregion
//
//		#region "  ExtractQuotedString  "
//
//		internal static string ExtractQuotedString(string source, char quoteChar, out int index)
//		{
//			StringBuilder res = new StringBuilder(32);
//			bool beginEscape = false;
//
//
//			if (source == null || source.Length == 0)
//
//
//				throw new BadUsageException("An empty String found and can be parsed like a QuotedString try to use SafeExtractQuotedString");
//
//
//			if (source[0] != quoteChar)
//				throw new BadUsageException("The source string not begins with the quote char: " + quoteChar);
//
//			index = 0;
//			int i = 1;
//			while (i < source.Length)
//			{
//				if (source[i] == quoteChar)
//				{
//					if (beginEscape == true)
//					{
//						beginEscape = false;
//						res.Append(quoteChar);
//					}
//					else
//					{
//						beginEscape = true;
//					}
//				}
//				else
//				{
//					if (beginEscape)
//					{
//						// End of the String
//						index = i;
//						return res.ToString();
//					}
//					else
//					{
//						res.Append(source[i]);
//					}
//				}
//
//				i++;
//			}
//			if (beginEscape)
//			{
//				index = i;
//				return res.ToString();
//			}
//			else
//				throw new BadUsageException("The current field has an UnClosed quoted string. Complete line: " + source);
//		}
//
//		#endregion

	}
}