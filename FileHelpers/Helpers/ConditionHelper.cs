using System;

namespace FileHelpers
{
	internal sealed class ConditionHelper
	{
		private ConditionHelper()
		{}

		public static bool BeginsWith(string line, string selector)
		{
			return line.StartsWith(selector);	
		}

		public static bool EndsWith(string line, string selector)
		{
			return line.EndsWith(selector);	
		}

		public static bool Contains(string line, string selector)
		{
			return line.IndexOf(selector) >= 0;
		}

		public static bool Enclosed(string line, string selector)
		{
			return line.StartsWith(selector) && line.EndsWith(selector);
		}

	}
}
