using System;
using System.Text;

namespace FileHelpers.Helpers
{
    /// <summary>
    /// Helper classes for strings
    /// </summary>
    internal static class StringHelper
    {
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