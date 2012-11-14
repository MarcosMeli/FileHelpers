using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ExamplesFramework
{
    /// <summary>
    /// Wrapper class for handling the HTML output
    /// </summary>
    public class HtmlWrapper
    {
        /// <summary>
        /// Navigate to filehelpers Trunk directory
        /// </summary>
        public const string Docs = "../../Doc";

        /// <summary>
        /// Navigate to filehelpers Trunk directory
        /// </summary>
        public const string DocsOutput = "../../Doc/Include";
        /// <summary>
        /// Template file, contains ${BODY} that we inject into the code
        /// </summary>
        private const string Template = "example_template.html";

        /// <summary>
        /// From template,  contains the top of the file
        /// </summary>
        private static string Heading = null;

        /// <summary>
        /// from Template contains the bottom of the file
        /// </summary>
        private static string Footing = null;

        /// <summary>
        /// Body text before expansion
        /// </summary>
        public String Body;

        /// <summary>
        /// Whether the blockquote commands are embedded, bad to inline viewing, better for webpages
        /// In DEBUG it will relfect the working directory of your Doc/Include directory
        /// </summary>
        public bool UseBlockQuotes = false;

        public string URLprefix = "http://filehelpers.com/";

        /// <summary>
        /// Example Files attached to this sample
        /// </summary>
        public List<ExampleFile> Files;

        static Regex LeadingAster = new Regex(@"^\s\* ", RegexOptions.Compiled | RegexOptions.Multiline);

        
        static readonly Regex FileNames = new Regex(@"(\${.*?})", RegexOptions.Compiled);
        static readonly Regex Blockquote = new Regex(@"\</?blockquote\>", RegexOptions.Compiled|RegexOptions.IgnoreCase);
        static Regex TrailingLines = new Regex("[\r\n]*$", RegexOptions.Compiled);

        /// <summary>
        /// Create a HTML wrapper with a template (before expansion)
        /// </summary>
        /// <param name="pBody">Html content that appears between body tags</param>
        public HtmlWrapper(  String pBody, List<ExampleFile> pFiles)
        {
            this.Body = pBody;
            this.Files = pFiles;

#if DEBUG
            var url = new Uri(Path.GetFullPath(DocsOutput), UriKind.Absolute);
            this.URLprefix = url.ToString() + "/";
#endif
        }

        /// <summary>
        /// Very simple HTML file with very little overheads
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var html = new StringBuilder();
            html.Append("<html>");
            html.Append("<body>");
            ProcessBody(html);
            html.Append("</body>");
            html.Append("</html>");
            return html.ToString();
        }

        private void ProcessBody(StringBuilder html)
        {
            foreach (var part in FileNames.Split(Body))
            {
                if (part.StartsWith("${"))
                {
                    String filename = part.Substring(2, part.Length - 3);
                    // Special case
                    if( filename == "URL" )
                    {
                        html.Append(URLprefix);
                        continue;
                    }
                    var details = this.Files.Where(x => x.Filename == filename).FirstOrDefault();
                    if (details == null)
                    {
                        html.Append("<p><b>File ");
                        html.Append(part);
                        html.Append(" is missing, check case because I am very dumb about that!</b></p>");
                    }
                    else
                    {

                        String[] lines = details.Contents.Split('\n');
                        int max = lines.Max(x => x.Length);
                        int count = lines.Count();

                        String cssClass = "data";
                        switch (details.Status)
                        {
                            case ExampleFile.FileType.InputFile:
                                cssClass = "data";
                                break;
                            case ExampleFile.FileType.OutputFile:
                                cssClass = "data";
                                break;
                            case ExampleFile.FileType.SourceFile:
                                switch (details.Language)
                                {
                                    case NetLanguage.CSharp:
                                        cssClass = "c#";
                                        break;
                                    case NetLanguage.VbNet:
                                        cssClass = "vb"; // TODO:  Check this
                                        break;
                                }
                                max = 80;
                                break;
                        }
                        html.AppendLine();
                        html.Append(@"<textarea name=""code"" rows=""");
                        html.Append(count); // text area is dumb
                        html.Append(@""" cols=""");
                        html.Append(max);
                        html.Append(@""" class=""");
                        html.Append(cssClass);
                        html.Append(@""">");

                        //  Encode the <>& etc to html elements.
                        html.Append(HttpUtility.HtmlEncode(details.Contents));
                        html.AppendLine("</textarea>");
                    }
                }
                else if (UseBlockQuotes)
                    html.Append(part.Trim());
                else
                    html.Append(Blockquote.Replace(part, "").Trim());
            }
        }

        /// <summary>
        /// Export example file to the filesystem for website
        /// </summary>
        /// <param name="filename">HTML output filename</param>
        public void Export(string filename)
        {
            bool storeUseBlockQuotes = this.UseBlockQuotes;
            string storePrefix = this.URLprefix;
            this.URLprefix = string.Empty;
            try
            {
                this.UseBlockQuotes = true;
                GetHeadAndFoot();
                string output = Path.Combine(DocsOutput, filename);
                using (var writer = new StreamWriter(output))
                {
                    writer.Write(Heading);
                    var body = new StringBuilder();
                    ProcessBody( body);
                    writer.Write(body);
                    writer.Write(Footing);
                    writer.Close();
                }
            }
            finally
            {
                this.UseBlockQuotes = storeUseBlockQuotes ;
                this.URLprefix = storePrefix;
            }
        }

        private static void GetHeadAndFoot()
        {
            if (Heading == null)
            {
                string path = Path.Combine(Docs, Template);
                using (var reader = new StreamReader(path))
                {
                    string[] temp = { "${BODY}" };
                    string[] parts = reader.ReadToEnd().Split(temp, StringSplitOptions.None);
                    reader.Close();

                    if (parts.Length != 2)
                    {
                        throw new Exception("There must be one, and only one ${BODY} in " + path + " found " + parts.Length);
                    }
                    Heading = parts[0];
                    Footing = parts[1];
                }
            }
        }
    }
}
