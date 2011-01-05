namespace FileHelpers
{
    public class DemoFile
    {

        public DemoFile()
            :this("demo.cs", NetLanguage.CSharp)
        {
        }

        public DemoFile(string filename)
            : this(filename, NetLanguage.CSharp)
        {
        }

        public DemoFile(string filename, NetLanguage language)
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
        }

        /// <summary>
        /// Type of file to list
        /// </summary>
        public FileType Status = FileType.SourceFile;
        public NetLanguage Language { get; set; }
    }
}