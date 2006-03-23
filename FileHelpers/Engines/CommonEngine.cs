#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;
using System.IO;
using System.Text;


namespace FileHelpers
{
	/// <summary>This class only have <b>static methods</b> to work with files and strings in the common way.</summary>
	public sealed class CommonEngine
	{
		// No instanciate
		private CommonEngine()
		{}

		#region "  Static Members  "

		/// <summary>
		/// Used to read a file without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="fileName">The file name</param>
		/// <returns>The read records.</returns>
		public static object[] ReadFile(Type recordClass, string fileName)
		{
			FileHelperEngine engine = new FileHelperEngine(recordClass);
			return engine.ReadFile(fileName);
		}

		/// <summary>
		/// Used to read a string without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="input">The input string.</param>
		/// <returns>The read records.</returns>
		public static object[] ReadString(Type recordClass, string input)
		{
			FileHelperEngine engine = new FileHelperEngine(recordClass);
			return engine.ReadString(input);
		}

		/// <summary>
		/// Used to write a file without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="fileName">The file name</param>
		/// <param name="records">The records to write</param>
		public static void WriteFile(Type recordClass, string fileName, object[] records)
		{
			FileHelperEngine engine = new FileHelperEngine(recordClass);
			engine.WriteFile(fileName, records);
		}

		/// <summary>
		/// Used to write a string without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="records">The records to write</param>
		/// <returns>The string with the writen records.</returns>
		public static string WriteString(Type recordClass, object[] records)
		{
			FileHelperEngine engine = new FileHelperEngine(recordClass);
			return engine.WriteString(records);
		}

		#endregion
	}
}