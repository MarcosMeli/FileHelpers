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

				if (srcString == string.Empty)
				{
					ShowSyntaxs();
					Environment.Exit(-1);
					return;
				}

				if (newString == "-f")
				{
					if (args.Length < 4)
					{
						ShowSyntaxs();
						Environment.Exit(-1);
						return;
					}

					System.IO.StreamReader reader = new StreamReader(args[3]);
					newString = reader.ReadToEnd();
					reader.Close();
				
				}

				if (args[args.Length - 1].ToLower() == "-v")
					mVerbose = false;

				foreach (string	file in Directory.GetFiles(Path.GetDirectoryName(destFile), Path.GetFileName(destFile)))
				{
					ReplaceFile(file, newString, srcString);
				}

			}
		}

		static bool mVerbose = true;
		private static void ReplaceFile(string destFile, string newString, string srcString)
		{
			System.IO.StreamReader reader = new StreamReader(destFile);
			string originalStr = reader.ReadToEnd();
			reader.Close();

			StreamWriter writer = new StreamWriter(destFile);
			writer.Write(originalStr.Replace(srcString, newString));
			writer.Close();

			if (mVerbose) Console.WriteLine("Replaced: '" + FirstChars(srcString, 25) + "' --> '" + FirstChars(newString, 25) + "'.");
		}

		private static string FirstChars(string source, int chars)
		{
			return source.Substring(0, Math.Min(chars, source.Length)).Replace(Environment.NewLine, " ");
			
		}

		private static void ShowSyntaxs()
		{
			Console.WriteLine("Syntaxis:");
			Console.WriteLine("");
			Console.WriteLine("FileReplace.exe destFile srcString destString");
			Console.WriteLine("");
			Console.WriteLine("   or");
			Console.WriteLine("FileReplace.exe destFile srcString -f destSourceFile");
		}
	}
}
