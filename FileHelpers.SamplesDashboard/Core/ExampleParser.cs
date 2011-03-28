using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FileHelpers.SamplesDashboard
{
    public class ExampleParser
    {
        public static string ParseExample(string demoText, string category)
        {
            var res = new StringBuilder();
            var regexOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;

            var name = "Demo";
            var match = Regex.Match(demoText, @"\/\/\-\>\s*\{Example\.Name\s*:(?<name>.*)\}", regexOptions);
            if (match.Success)
            {
                name = match.Groups["name"].Value.Trim();
            }

            var description = "";

            match = Regex.Match(demoText, @"\/\/\-\>\s*\{Example\.Description\s*:(?<description>.*)\}", regexOptions | RegexOptions.Multiline);
            if (match.Success)
            {
                description = match.Groups["description"].Value.Trim();
            }

            res.AppendLine(@"demo = new DemoCode(""" + name + @""", """ + category + @""");");
            if (!string.IsNullOrEmpty(description))
            {
                res.AppendLine(@"demo.CodeDescription = @""" + description + @""";");
            }

            res.AppendLine(@"demos.Add(demo);");


            var filesMatch = Regex.Matches(demoText,
                                           @"\/\/\-\>\s*\{Example\.File\s*\:(?<filename>.*?)\}\s*\r\n(?<filecode>.*?)\s*\/\/\-\>\s*\{\/Example\.File\s*\}",
                                           regexOptions | RegexOptions.Singleline);

            foreach (Match fileMatch in filesMatch)
            {
                res.AppendLine(@"demo.Files.Add(new DemoFile(""" + fileMatch.Groups["filename"].Value + @"""));");
                res.AppendLine(@"demo.LastFile.Contents = @""" + GetFileCode(fileMatch.Groups["filecode"].Value) + @""";");
            }


            //demo.Files.Add(new DemoFile());
            //demo.LastFile.Contents = "Bla bla .bla";
            //res.Add(demo);

            return res.ToString();
        }

        public static string GetFileCode(string contents)
        {
            contents = contents.Replace("\"", "\"\"")
                .Replace("*/", "")
                .Replace("/*", "");

            int? ident = null;

            foreach (var line in contents.Split(new string[] {Environment.NewLine}, StringSplitOptions.None))
	        {
                if (line.Trim().Length == 0)
                    continue;

                var spaces = line.Length - line.TrimStart().Length;

                if (ident == null)
                    ident = spaces;
                else
                    ident = Math.Min(ident.Value, spaces);
	        }

            var res = new StringBuilder();
            
            foreach (var line in contents.Split(new string[] {Environment.NewLine}, StringSplitOptions.None))
	        {
                if (line.Length < ident)
                    res.AppendLine(line);
                else
                    res.AppendLine(line.Substring(ident.Value - 1));
	        }

            return res.ToString();
        }
    }
}
