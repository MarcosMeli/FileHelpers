using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Detection
{
    /// <summary>
    /// Find the number of delimiters in a class
    /// </summary>
    internal static class QuoteHelper
    {
        /// <summary>
        /// If we use this quote character and this delimitter how many fields do we get
        /// </summary>
        /// <param name="line">Line we are testing against</param>
        /// <param name="delimiter">delimiter for fields</param>
        /// <param name="quotedChar">quote value</param>
        /// <returns>number of fields</returns>
        public static int CountNumberOfDelimiters(string line, char delimiter, char quotedChar)
        {
            int delimitersInLine = 0;
            var restOfLine = line;
            while (!string.IsNullOrEmpty(restOfLine))
            {
                if (restOfLine.StartsWith(quotedChar.ToString()))
                {
                    restOfLine = DiscardUntilQuotedChar(restOfLine, quotedChar);
                }
                else
                {
                    var index = restOfLine.IndexOf(delimiter);
                    if (index < 0)
                        return delimitersInLine;
                    else
                    {
                        delimitersInLine++;
                        restOfLine = restOfLine.Substring(index + 1);
                    }
                }
            }
            return delimitersInLine;
        }

        /// <summary>
        /// skip data until we have matching quote
        /// </summary>
        /// <param name="line">line to test</param>
        /// <param name="quoteChar">quote char to use</param>
        /// <returns>string after field</returns>
        /// <remarks>
        /// This logic does not handle two quotes in a row.  The
        /// logic that calls this will call the same code again to get
        /// the next chunk between delimitters anyway so it is not needed
        /// </remarks>
        private static string DiscardUntilQuotedChar(string line, char quoteChar)
        {
            if (line.StartsWith(quoteChar.ToString()))
                line = line.Substring(1);
            
            var index = line.IndexOf(quoteChar);
            if (index < 0)
                return string.Empty;
            else
                return line.Substring(index + 1);
        }
    }
}