

using System;
using System.ComponentModel;
using System.Text;
using System.Globalization;

namespace FileHelpers
{

	internal static class StringHelper
	{
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

	    private static CultureInfo mCulture;
        internal static CompareInfo CreateComparer()
        {
            if (mCulture == null)
                mCulture = CultureInfo.InvariantCulture; // new CultureInfo("en-us");

            return mCulture.CompareInfo;
        }


        internal static string ReplaceRecursive(string original, string oldValue, string newValue)
        {
            const int maxTries = 1000;

            string ante, res;

            res = original.Replace(oldValue, newValue);

            var i = 0;
            do
            {
                i++;
                ante = res;
                res = ante.Replace(oldValue, newValue);

            } while (ante != res || i > maxTries);

            return res;
        }

        internal static string ToValidIdentifier(string original)
        {
            if (original.Length == 0)
                return string.Empty;

            StringBuilder res = new StringBuilder(original.Length + 1);
            if (!char.IsLetter(original[0]) && original[0] != '_')
                res.Append('_');

            for (int i = 0; i < original.Length; i++)
            {
                char c = original[i];
                if (char.IsLetterOrDigit(c) || c == '_')
                    res.Append(c);
                else
                    res.Append('_');
            }

            var identifier = ReplaceRecursive(res.ToString(), "__", "_").Trim('_');
            if (identifier.Length == 0)
                return "_";
            else if (char.IsDigit(identifier[0]))
                identifier = "_" + identifier;

            return identifier;

        }


    }
}