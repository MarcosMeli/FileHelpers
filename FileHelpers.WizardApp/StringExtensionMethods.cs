using System.Collections.Generic;

namespace System
{
    public static class StringExtensionMethods
    {
        public static IEnumerable<string> SplitInLines(this string s)
        {
            return s.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
        }
    }
}