using System;
using System.Text;

namespace FileHelpersTests
{
    public class TestBaseHelper
    {
     	private readonly string mBasePath = @"..\data\";

        protected string BuildPath(string file)
        {
            return BuildPath(string.Empty, file);
            
        }

        protected string BuildPath(string basePath, string file)
        {
            if (basePath == null | basePath.Length == 0)
                return mBasePath + file;
            else
                return mBasePath + basePath + @"\" + file;
        }
    }
}
