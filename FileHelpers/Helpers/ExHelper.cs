#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	internal sealed class ExHelper
	{
		private ExHelper()
		{
		}

		public static void CheckNullOrEmpty(string val)
		{
			if (val == null || val.Length == 0)
				throw new ArgumentNullException("Value can´t be null or empty");
		}

		public static void CheckNullOrEmpty(string val, string paramName)
		{
			if (val == null || val.Length == 0)
				throw new ArgumentNullException(paramName, "Value can´t be null or empty");
		}

		public static void CheckNullParam(string param, string paramName)
		{
			if (param == null || param.Length == 0)
				throw new ArgumentNullException(paramName + " can´t be neither null nor empty", paramName);
		}

		public static void CheckNullParam(object param, string paramName)
		{
			if (param == null)
				throw new ArgumentNullException(paramName + " can´t be null", paramName);
		}

		public static void CheckDifferentsParams(object param1, string param1Name, object param2, string param2Name)
		{
			if (param1 == param2)
				throw new ArgumentException(param1Name + " can´t be the same that " + param2Name, param1Name + " and " + param2Name);
		}

		public static void PositiveValue(int val)
		{
			if (val < 0 )
				throw new ArgumentException("The value must be greater or equal than 0.");
		}
	}
}