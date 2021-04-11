using System;
using System.Text;

namespace FileHelpers.Dynamic
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
    }
}