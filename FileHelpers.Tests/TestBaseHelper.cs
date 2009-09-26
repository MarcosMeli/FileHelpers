using System.IO;

namespace FileHelpersTests
{
    public class TestBaseHelper
    {
        private readonly string mBasePath = Path.Combine("..", "Data");

        protected string BuildPath(string file)
        {
            return BuildPath(string.Empty, file);
            
        }

        protected string BuildPath(string basePath, string file)
        {
            if (basePath == null | basePath.Length == 0)
                return Path.Combine(mBasePath, file);
            else
                return Path.Combine(Path.Combine(mBasePath, basePath), file);
        }
    }
}
