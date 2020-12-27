using System;
using System.Globalization;
using System.Text;

namespace FileHelpers.Helpers
{
    /// <summary>
    /// Helper classes for strings
    /// </summary>
    internal static class StringHelper
    {
        /// <summary>
        /// New line variable
        /// </summary>
        internal static readonly string NewLine = Environment.NewLine;

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
            if (line.IsEOL())
            {
                throw new BadUsageException(
                    "An empty String found. This cannot be parsed like a QuotedString - try to use SafeExtractQuotedString");
            }

            if (line.mLineStr[line.mCurrentPos] != quoteChar)
                throw new BadUsageException("The source string does not begin with the quote char: " + quoteChar);

            var res = new StringBuilder(32);

            bool firstFound = false;

            int i = line.mCurrentPos + 1;

            while (line.mLineStr != null)
            {
                while (i < line.mLineStr.Length)
                {
                    if (line.mLineStr[i] == quoteChar)
                    {
                        if (firstFound)
                        {
                            // Is an escaped quoted char
                            res.Append(quoteChar);
                            firstFound = false;
                        }
                        else
                            firstFound = true;
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
                            res.Append(line.mLineStr[i]);
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
                    {
                        throw new BadUsageException("The current field has an unclosed quoted string. Complete line: " +
                                                    res);
                    }

                    line.ReadNextLine();
                    res.Append(NewLine);
                    //lines++;
                    i = 0;
                }
            }

            throw new BadUsageException("The current field has an unclosed quoted string. Complete Field String: " +
                                        res);
        }

        /// <summary>
        /// Convert a string to a string with quotes around it,
        /// if the quote appears within the string it is doubled
        /// </summary>
        /// <param name="sb">Where string is added</param>
        /// <param name="source">String to be added</param>
        /// <param name="quoteChar">quote character to use, eg "</param>
        internal static void CreateQuotedString(StringBuilder sb, string source, char quoteChar)
        {
            if (source == null)
                source = string.Empty;

            string quotedCharStr = quoteChar.ToString();
            string escapedString = source.Replace(quotedCharStr, quotedCharStr + quotedCharStr);

            sb.Append(quoteChar);
            sb.Append(escapedString);
            sb.Append(quoteChar);
        }

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
        private static string ReplaceRecursive(string original, string oldValue, string newValue)
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
            } while (ante != res ||
                     i > maxTries);

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

            var res = new StringBuilder(original.Length + 1);
            if (!char.IsLetter(original[0]) &&
                original[0] != '_')
                res.Append('_');

            for (int i = 0; i < original.Length; i++)
            {
                char c = original[i];
                if (char.IsLetterOrDigit(c) ||
                    c == '_')
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
            StringComparison comparisionType = StringComparison.OrdinalIgnoreCase;
            string result = original;

            if (!string.IsNullOrEmpty(oldValue))
            {
                int index = -1;
                int lastIndex = 0;

                var buffer = new StringBuilder(original.Length);

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

        /// <summary>
        /// Determines whether the beginning of this string instance matches the specified string ignoring white spaces at the start.
        /// </summary>
        /// <param name="source">source string.</param>
        /// <param name="value">The string to compare.</param>
        /// <param name="comparisonType">string comparison type.</param>
        /// <returns></returns>
        public static bool StartsWithIgnoringWhiteSpaces(string source, string value, StringComparison comparisonType)
        {
            if (value == null)
            {
                return false;
            }
            // find lower bound
            int i = 0;
            int sz = source.Length;
            while (i < sz && char.IsWhiteSpace(source[i]))
            {
                i++;
            }
            // adjust upper bound
            sz = sz - i;
            if (sz < value.Length)
                return false;
            sz = value.Length;
            // search
            return source.IndexOf(value, i, sz, comparisonType) == i;
        }
    }
}