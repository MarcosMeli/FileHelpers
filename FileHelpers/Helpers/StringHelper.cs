using System;
using System.ComponentModel;
using System.Text;
using System.Globalization;

namespace FileHelpers
{
    /// <summary>
    /// Helper classes for strings
    /// </summary>
	internal static class StringHelper
	{
        /// <summary>
        /// New line variable
        /// </summary>
		#if ! MINI
			internal static readonly string NewLine = Environment.NewLine;
		#else
			internal static readonly string NewLine = "\r\n";
		#endif


		#region "  ExtractQuotedString  "

        /// <summary>
        /// Extract a string from a quoted string, allows for doubling the quotes
        /// for example 'o''clock'
        /// </summary>
        /// <param name="line">Line to extract from (with extra info)</param>
        /// <param name="quoteChar">Quote char to remove</param>
        /// <param name="allowMultiline">can we have a new line in middle of string</param>
        /// <returns>Extracted information</returns>
		internal static ExtractedInfo ExtractQuotedString(LineInfo line, char quoteChar, bool allowMultiline)
		{
			//			if (line.mReader == null)
			//				throw new BadUsageException("The reader can´t be null");

			if (line.IsEOL())
				throw new BadUsageException("An empty String found. This can not be parsed like a QuotedString try to use SafeExtractQuotedString");

			if (line.mLineStr[line.mCurrentPos] != quoteChar)
				throw new BadUsageException("The source string does not begin with the quote char: " + quoteChar);

			StringBuilder res = new StringBuilder(32);

			bool firstFound = false;

			int i = line.mCurrentPos + 1;
			//bool mustContinue = true;

			while (line.mLineStr != null)
			{
				while (i < line.mLineStr.Length)
				{
					if (line.mLineStr[i] == quoteChar)
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
						}
						else
						{
							res.Append(line.mLineStr[i]);
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
						throw new BadUsageException("The current field has an unclosed quoted string. Complete line: " + res.ToString());

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

        /// <summary>
        /// Convert a string to a string with quotes around it,
        /// if the quote appears within the string it is doubled
        /// </summary>
        /// <param name="sb">Where string is added</param>
        /// <param name="source">String to be added</param>
        /// <param name="quoteChar">quote character to use, eg "</param>
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

        // TODO:  is this correct or even necessary...
        /// <summary>
        /// remove leading blanks and blanks after the plus or minus sign from a string,
        /// </summary>
        /// <param name="source">source to trim</param>
        /// <returns>String without blanks</returns>
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

        /// <summary>
        /// Create an invariant culture comparison operator
        /// </summary>
        /// <returns>Comparison operations</returns>
        internal static CompareInfo CreateComparer()
        {
            if (mCulture == null)
                mCulture = CultureInfo.InvariantCulture; // new CultureInfo("en-us");

            return mCulture.CompareInfo;
        }

        /// <summary>
        /// replace the one string with another, and keep doing it
        /// </summary>
        /// <param name="original">Original string</param>
        /// <param name="oldValue">Value to replace</param>
        /// <param name="newValue">value to replace with</param>
        /// <returns>String with all multiple occurrences replaced</returns>
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

        /// <summary>
        /// convert a string into a valid identifier
        /// </summary>
        /// <param name="original">Original string</param>
        /// <returns>valid identifier  Original_string</returns>
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

        /// <summary>
        /// Replace string with another ignoring the case of the string
        /// </summary>
        /// <param name="original">Original string</param>
        /// <param name="oldValue">string to replace</param>
        /// <param name="newValue">string to insert</param>
        /// <returns>string with values replaced</returns>
        public static string ReplaceIgnoringCase(string original, string oldValue, string newValue)
        {
            return Replace(original, oldValue, newValue, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// String replace with a comparison function, eg OridinalIgnoreCase
        /// </summary>
        /// <param name="original">Original string</param>
        /// <param name="oldValue">Value to be replaced</param>
        /// <param name="newValue">value to replace with</param>
        /// <param name="comparisionType">Comparison type (enum)</param>
        /// <returns>String with values replaced</returns>
        public static string Replace(string original, string oldValue, string newValue, StringComparison comparisionType)
        {
            string result = original;

            if (!string.IsNullOrEmpty(oldValue))
            {
                int index = -1;
                int lastIndex = 0;

                StringBuilder buffer = new StringBuilder(original.Length);

                while ((index = original.IndexOf(oldValue, index + 1, comparisionType)) >= 0)
                {
                    buffer.Append(original, lastIndex, index - lastIndex);
                    buffer.Append(newValue);

                    lastIndex = index + oldValue.Length;
                }
                buffer.Append(original, lastIndex, original.Length - lastIndex);

                result = buffer.ToString();
            }

            return result;
        }
    }
}