using System;
using System.IO;
using System.Text;

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

                    newString = File.ReadAllText(args[3], Encoding.UTF8);

                }

                if (args[args.Length - 1].ToLower() == "-e")
                    mEscape = true;

                foreach (string file in Directory.GetFiles(Path.GetDirectoryName(destFile), Path.GetFileName(destFile)))
                {
                    ReplaceFile(file, newString, srcString);
                }

            }
        }
        static bool mEscape = false;
        private static void ReplaceFile(string destFile, string newString, string srcString)
        {
            string originalStr = File.ReadAllText(destFile, Encoding.UTF8);

            if (mEscape)
            {
                srcString = srcString
                    .Replace("\\n", "\n")
                    .Replace("\\r", "\r")
                    .Replace("\\t", "\t")
                    .Replace("\\r\\n", "\r\n");

                newString = newString
                    .Replace("\\n", "\n")
                    .Replace("\\r", "\r")
                    .Replace("\\t", "\t")
                    .Replace("\\r\\n", "\r\n");
                

            }

            File.WriteAllText(destFile, ReplaceIgnoringCase(originalStr, srcString, newString), Encoding.UTF8);
            Console.WriteLine("Replaced: '" + FirstChars(srcString, 25) + "' --> '" + FirstChars(newString, 25) + "'.");
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
            Console.WriteLine("   with escaped string");
            Console.WriteLine("FileReplace.exe destFile srcString destString -e");
        }

        public static string ReplaceIgnoringCase(string original, string oldValue, string newValue)
        {
            return Replace(original, oldValue, newValue, StringComparison.OrdinalIgnoreCase);
        }

        public static string Replace(string original, string oldValue, string newValue, StringComparison comparisionType)
        {
            string result = original;

            if (!string.IsNullOrEmpty(newValue))
            {
                int index = -1;
                int lastIndex = 0;

                StringBuilder buffer = new StringBuilder(original.Length);

                while ((index = original.IndexOf(oldValue, index + 1, comparisionType)) >= 0)
                {
                    buffer.Append(original, lastIndex, index - lastIndex);
                    buffer.Append(newValue);

                    lastIndex = index + oldValue.Length;
                }
                buffer.Append(original, lastIndex, original.Length - lastIndex);

                result = buffer.ToString();
            }

            return result;
        }
    }
}
