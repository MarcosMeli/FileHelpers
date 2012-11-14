
using System;
using System.Collections;
using System.Collections.Generic;

namespace ExamplesFx
{
    public class ExampleFile
    {

        public ExampleFile(string filename)
            : this(filename, NetLanguage.CSharp)
        {
        }

        public ExampleFile(string filename, NetLanguage language)
        {
            Filename = filename;
            Language = language;
        }


        public string Filename { get; set; }
        public string Contents { get; set; }
        public enum FileType
        {
            SourceFile,
            InputFile,
            OutputFile,
            HtmlFile,
        }

        /// <summary>
        /// Type of file to list
        /// </summary>
        public FileType Status = FileType.SourceFile;
        public NetLanguage Language { get; set; }
    }
}