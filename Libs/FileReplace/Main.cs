using System;
using System.IO;

namespace FileReplace
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Replacer
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (args.Length < 3)
				ShowSyntaxs();
			else
			{
				string destFile, srcString, newString;

				destFile = args[0];
				srcString = args[1];
				newString = args[2];

				System.IO.StreamReader reader = new StreamReader(destFile);
				string originalStr = reader.ReadToEnd();
				reader.Close();

				StreamWriter writer = new StreamWriter(destFile);
				writer.Write(originalStr.Replace(srcString, newString));
				writer.Close();

				Console.WriteLine("Finish to Replace '" + srcString + "' with '" + newString + "'.");
			}
		}

		private static void ShowSyntaxs()
		{
			Console.WriteLine("Syntaxis:");
			Console.WriteLine("");
			Console.WriteLine("FileReplace.exe destFile srcString destString");
		}
	}
}
