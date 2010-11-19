using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FileHelpers.SamplesDashboard
{
    public class ExampleParser
    {
        public static string Parse(string demoText)
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

            var category = "";
            res.AppendLine(@"demo = new DemoCode(""" + name + @""", """ + category + @""");");
            if (!string.IsNullOrEmpty(description))
            {
                res.AppendLine(@"demo.CodeDescription = @""" + description + @""";");
            }
            
            res.AppendLine(@"demos.Add(demo);");


            var filesMatch = Regex.Matches(demoText,
                                           @"\/\/\-\>\s*\{Example\.File\s*\:(?<filename>.*?)\}\s*(?<filecode>.*?)\s*\/\/\-\>\s*\{\/Example\.File\s*\}",
                                           regexOptions | RegexOptions.Singleline);

            foreach (Match fileMatch in filesMatch)
            {
                res.AppendLine(fileMatch.Groups["filename"].Value);
                res.AppendLine(fileMatch.Groups["filecode"].Value);
            }
            

            //demo.Files.Add(new DemoFile());
            //demo.LastFile.Contents = "Bla bla .bla";
            //res.Add(demo);

            return res.ToString();
        }
    }
}
