using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ExamplesFx
{
    public sealed class ExampleCode
    {
        public class ExampleEventArgs : EventArgs
        {
            public ExampleCode Example { get; private set; }

            internal ExampleEventArgs(ExampleCode example)
            {
                this.Example = example;
            }
        }

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
        /// Create a new example class
        /// </summary>
        /// <param name="example">Example structure from template parse</param>
        /// <param name="name">Title from TODO:</param>
        /// <param name="category">Category from TODO:</param>
        /// <param name="solutionFile">The solutionfilename</param>
        public ExampleCode(ExampleBase example, string name, string category, string solutionFile)
        {
            Example = example;
            Example.Console.Changed += new EventHandler(Console_Changed); 
            Example.InputFileChanged += new EventHandler(Input_Changed); 
            Name = name;
            OriginalFileName = solutionFile;
            Category = category;
            Runnable = true;
            AutoRun = false;
            Files = new List<ExampleFile>();
        }

        internal event EventHandler ConsoleChanged;

        private void OnConsoleChanged()
        {
            EventHandler handler = ConsoleChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        void Console_Changed(object sender, EventArgs e)
        {
            OnConsoleChanged();
        }


        internal event EventHandler InputChanged;

        private void OnInputChanged()
        {
            EventHandler handler = InputChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        void Input_Changed(object sender, EventArgs e)
        {
            OnInputChanged();
        }

        /// <summary>
        /// Example class that runs
        /// </summary>
        public ExampleBase Example { get; private set; }

        /// <summary>
        /// Title set from code
        /// </summary>
        public string Name { get; private set; }
        
        public string OriginalFileName { get; private set; }

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
        /// Is this test runnable
        /// </summary>
        public bool Runnable { get; set; }

        /// <summary>
        /// Is this test runnable
        /// </summary>
        public bool AutoRun { get; set; }

        ///// <summary>
        ///// Indicates if the Example has Console Output
        ///// </summary>
        //public bool HasOutput { get; set; }

        
        public void RunExample()
        {
            try
            {
                ExamplesEnvironment.InitEnvironment(this);

                this.Example.RunExample();
            }
            catch (Exception ex)
            {
                this.Example.Exception = ex;
            }
            finally
            {
                foreach (var file in this.Files)
                {
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
             
            }

        }

        public void OnNewFileCreated(string fullPath)
        {
            var name = Path.GetFileName(fullPath);

        }
    }
}
