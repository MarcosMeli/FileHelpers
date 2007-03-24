using System;
using System.IO;

namespace FileMerger
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Merger
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
				string destFile, srcFile;
				int pos;

				destFile = args[0];
				srcFile = args[1];
				pos = int.Parse(args[2]);

				System.IO.StreamReader reader = new StreamReader(srcFile);
				string writeStr = reader.ReadToEnd();
				reader.Close();

				reader = new StreamReader(destFile);
				string originalStr = reader.ReadToEnd();
				reader.Close();

				StreamWriter writer = new StreamWriter(destFile);
                
				writer.Write(originalStr.Substring(0, pos));
				writer.Write(writeStr);
				writer.Write(originalStr.Substring(pos));
				writer.Close();

				Console.WriteLine("Finish to Merge files at position " + pos.ToString());
			}

			//
			// TODO: Add code to start application here
			//
		}

		private static void ShowSyntaxs()
		{
			Console.WriteLine("Syntaxis:");
			Console.WriteLine("");
			Console.WriteLine("FileMerger.exe destFile srcFile destPos");

		}
	}
}
