using FileHelpers;
using System.Collections;
using System.IO;


namespace FileHelpersTests
{
	// this class only adds the relative path to the saple files.
	public sealed class Common
	{
		public static string TestPath(string fileName)
		{
            return @"..\..\data\" + fileName;
		}

//		public static string FullTestPath(string fileName)
//		{
//			return Path.GetDirectoryName(typeof(Common).Assembly.GetModules()[0].FullyQualifiedName) + @"\..\data\" + fileName;
//		}

		public static object[] ReadTest(FileHelperEngine engine, string fileName)
		{
            return engine.ReadFile(@"..\..\data\" + fileName);
		}

        public static object[] ReadTest(FileHelperEngine engine, string fileName, int maxRecords)
        {
            return engine.ReadFile(@"..\..\data\" + fileName, maxRecords);
        }

		public static object[] ReadAllAsync(FileHelperAsyncEngine engine, string fileName)
		{
			ArrayList arr = new ArrayList();
            engine.BeginReadFile(@"..\..\data\" + fileName);
			while(engine.ReadNext() != null)
				arr.Add(engine.LastRecord);
			engine.Close();

			return arr.ToArray();

		}

        public static void BeginReadTest(FileHelperAsyncEngine engine, string fileName)
		{
            engine.BeginReadFile(@"..\..\data\" + fileName);
		}


		private Common()
		{}
	}
}