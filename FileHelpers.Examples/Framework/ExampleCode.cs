using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ExamplesFramework
{
    public sealed class ExampleCode
    {
        /// <summary>
        /// Message when new file is added to the list
        /// </summary>
        public class NewFileEventArgs : EventArgs
        {
            public ExampleFile File { get; private set; }

            internal NewFileEventArgs(ExampleFile file)
            {
                this.File = file;
            }
        }

        /// <summary>
        /// Notify application that a new file has been created
        /// </summary>
        public event EventHandler<NewFileEventArgs> AddedFile;

        /// <summary>
        /// Create a new demo class
        /// </summary>
        /// <param name="demo">Demo structure from template parse</param>
        /// <param name="codeTitle">Title from TODO:</param>
        /// <param name="category">Category from TODO:</param>
        public ExampleCode(ExampleBase demo, string codeTitle, string category)
        {
            Name = codeTitle;
            Demo = demo;
            Category = category;
            Runnable = true;
            Files = new List<ExampleFile>();
        }

        /// <summary>
        /// Demo class that runs
        /// </summary>
        public ExampleBase Demo { get; private set; }

        /// <summary>
        /// Title set from code
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Description set from code
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Code from file.
        /// </summary>
        public string SourceCode { get; set; }

        /// <summary>
        /// Can be of the form "Async/Delimited" with multiple categories
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// List of logical files extracted from the code
        /// </summary>
        public List<ExampleFile> Files { get; set; }

        /// <summary>
        /// last file extracted, generally sample data
        /// </summary>
        public ExampleFile LastFile
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
        /// Indicates if the Example has Console Output
        /// </summary>
        public bool HasOutput { get; set; }

        /// <summary>
        /// Control???  - Not used
        /// </summary>
        public UserControl Control  { get; set; }

        public void Test()
        {
            try
            {

                foreach (ExampleFile file in this.Files)
                {
                    if (file.Status == ExampleFile.FileType.InputFile)
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
                foreach (ExampleFile file in this.Files)
                {
                    if (file.Filename == "Console")
                    {
                        file.Contents = this.Demo.Output;
                        ConsoleFound = true;
                    }
                    if (file.Status == ExampleFile.FileType.InputFile)
                    {
                        File.Delete(file.Filename);
                    }
                    if (file.Status == ExampleFile.FileType.OutputFile)
                    {
                        if (File.Exists(file.Filename))
                        {
                            file.Contents = File.ReadAllText(file.Filename);
                        }
                    }
                }
                if ((!ConsoleFound) && !String.IsNullOrEmpty(this.Demo.Output))
                {
                    ExampleFile console = new ExampleFile("Console");
                    console.Contents = this.Demo.Output;
                    console.Status = ExampleFile.FileType.OutputFile;
                    this.Files.Add(console);
                    if (AddedFile != null)
                        AddedFile(this, new NewFileEventArgs(console));
                }
            }

        }
    }
}
