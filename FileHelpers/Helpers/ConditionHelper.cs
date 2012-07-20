using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>
    /// Helpers that work with conditions to make them easier to write
    /// </summary>
	internal static class ConditionHelper
	{
        /// <summary>
        /// Test whether string begins with another string
        /// </summary>
        /// <param name="line">string to test</param>
        /// <param name="selector">value we want to check for</param>
        /// <returns>true if string begins with the selector</returns>
		public static bool BeginsWith(string line, string selector)
		{
			return line.StartsWith(selector);
		}

        /// <summary>
        /// Test whether string ends with another string
        /// </summary>
        /// <param name="line">string to test</param>
        /// <param name="selector">value we want to check for</param>
        /// <returns>true if string ends with the selector</returns>
        public static bool EndsWith(string line, string selector)
		{
			return line.EndsWith(selector);
		}

        /// <summary>
        /// Test whether string contains with another string
        /// </summary>
        /// <param name="line">string to test</param>
        /// <param name="selector">value we want to check for</param>
        /// <returns>true if string contains the selector</returns>
        public static bool Contains(string line, string selector)
		{
			return line.IndexOf(selector) >= 0;
		}

        /// <summary>
        /// Test whether string begins and ends with another string
        /// </summary>
        /// <param name="line">string to test</param>
        /// <param name="selector">value we want to check for</param>
        /// <returns>true if string begins and ends with the selector</returns>
        public static bool Enclosed(string line, string selector)
		{
			return line.StartsWith(selector) && line.EndsWith(selector);
		}
	}
}
