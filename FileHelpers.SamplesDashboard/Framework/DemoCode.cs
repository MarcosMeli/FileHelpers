using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FileHelpers
{
    public sealed class DemoCode
    {
        /// <summary>
        /// Message when new file is added to the list
        /// </summary>
        public class NewFile : EventArgs
        {
            public DemoFile details;

            internal NewFile(DemoFile pfile)
            {
                this.details = pfile;
            }
        }

        /// <summary>
        /// Notify application that a new file has been created
        /// </summary>
        public event EventHandler<NewFile> AddedFile;

        /// <summary>
        /// Create a new demo class
        /// </summary>
        /// <param name="demo">Demo structure from template parse</param>
        /// <param name="codeTitle">Title from TODO:</param>
        /// <param name="category">Category from TODO:</param>
        public DemoCode(DemoParent demo, string codeTitle, string category)
        {
            CodeTitle = codeTitle;
            Demo = demo;
            Category = category;
            Runnable = true;
            Files = new List<DemoFile>();
        }

        /// <summary>
        /// Demo class that runs
        /// </summary>
        public DemoParent Demo { get; private set; }

        /// <summary>
        /// Title set from code
        /// </summary>
        public string CodeTitle { get; private set; }

        /// <summary>
        /// Description set from code
        /// </summary>
        public string CodeDescription { get; set; }

        /// <summary>
        /// Code from file.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Can be of the form "Async/Delimited" with multiple categories
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// List of logical files extracted from the code
        /// </summary>
        public List<DemoFile> Files { get; set; }

        /// <summary>
        /// last file extracted, generally sample data
        /// </summary>
        public DemoFile LastFile
        {
            get
            {
                if (Files.Count == 0)
                    return null;
                else
                    return Files[Files.Count - 1];
            }
        }

        /// <summary>
        /// Whether this test has been run before
        /// </summary>
        public bool TestRun { get { return Demo.TestRun; } }

        /// <summary>
        /// Is this test runnable
        /// </summary>
        public bool Runnable { get; set; }

        /// <summary>
        /// Control???  - Not used
        /// </summary>
        public UserControl Control  { get; set; }

        public void Test()
        {
                        try
            {

            foreach (DemoFile file in this.Files)
            {
                if (file.Status == DemoFile.FileType.InputFile)
                    File.WriteAllText(file.Filename, file.Contents, Encoding.UTF8);
            }
            this.Demo.Test();
            }
                        catch (Exception ex)
                        {
                            this.Demo.Output += ex.ToString();
                        }
                        finally
                        {
                            bool ConsoleFound = false;
                            foreach (DemoFile file in this.Files)
                            {
                                if (file.Filename == "Console")
                                {
                                    file.Contents = this.Demo.Output;
                                    ConsoleFound = true;
                                }
                                if (file.Status == DemoFile.FileType.InputFile)
                                {
                                    File.Delete(file.Filename);
                                }
                                if (file.Status == DemoFile.FileType.OutputFile)
                                {
                                    if (File.Exists(file.Filename))
                                    {
                                        file.Contents = File.ReadAllText(file.Filename);
                                    }
                                }
                            }
                            if ((!ConsoleFound) && !String.IsNullOrEmpty(this.Demo.Output))
                            {
                                DemoFile console = new DemoFile("Console");
                                console.Contents = this.Demo.Output;
                                console.Status = DemoFile.FileType.OutputFile;
                                this.Files.Add(console);
                                if (AddedFile != null)
                                    AddedFile(this,new NewFile(console));
                            }
                        }

        }
    }
}
