using System;
using System.IO;
using System.Text;

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
                if (args[2].ToUpper() == "MAX" || args[2].ToUpper() == "END")
                    pos = int.MaxValue;
                else
                    pos = int.Parse(args[2]);

                bool inCmd = args[args.Length -1].ToUpper() == "-INLINE";

			    string writeStr;
                if (inCmd)
                {
                    writeStr = srcFile;
                    writeStr = writeStr
                    .Replace("\\n", "\n")
                    .Replace("\\r", "\r")
                    .Replace("\\t", "\t")
                    .Replace("\\r\\n", "\r\n");
                }
			    
                else
                    writeStr = File.ReadAllText(srcFile, Encoding.Default);

			    string originalStr = File.ReadAllText(destFile, Encoding.Default);
                if (pos < 0)
                    pos = originalStr.Length + pos;

                StringBuilder res = new StringBuilder(originalStr.Length + writeStr.Length);

                if (pos == int.MaxValue)
                {
                    res.Append(originalStr);
                    res.Append(writeStr);
                }
                else
                {
                    res.Append(originalStr.Substring(0, pos));
                    res.Append(writeStr);
                    res.Append(originalStr.Substring(pos));
                }

			    File.WriteAllText(destFile, res.ToString(), Encoding.Default);

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
