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
        public NetLanguage Language { get; set; }
    }
}