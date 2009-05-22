using System.Reflection;
using FileHelpers;
using FileHelpers.MasterDetail;
using System.Collections;
using System.IO;


namespace FileHelpersTests
{
	// this class only adds the relative path to the saple files.
	public sealed class Common
	{
	    private static string mAssemblyLocation = string.Empty;
		public static string TestPath(string fileName)
		{
            if (string.IsNullOrEmpty(mAssemblyLocation))
            {
                mAssemblyLocation = Assembly.GetAssembly(typeof (Common)).Location;
            }

            return Path.Combine(Path.Combine(mAssemblyLocation, @"..\data"), fileName);
		}

//		public static string FullTestPath(string fileName)
//		{
//			return Path.GetDirectoryName(typeof(Common).Assembly.GetModules()[0].FullyQualifiedName) + @"\..\data\" + fileName;
//		}

		public static object[] ReadTest(FileHelperEngine engine, string fileName)
		{
			return engine.ReadFile(TestPath(fileName));
		}

        public static object[] ReadTest(FileHelperEngine engine, string fileName, int maxRecords)
        {
            return engine.ReadFile(TestPath(fileName), maxRecords);
        }

		public static object[] ReadAllAsync(FileHelperAsyncEngine engine, string fileName)
		{
			ArrayList arr = new ArrayList();
			engine.BeginReadFile(TestPath(fileName));
			while(engine.ReadNext() != null)
				arr.Add(engine.LastRecord);
			engine.Close();

			return arr.ToArray();

		}

		public static MasterDetails[] ReadTest(MasterDetailEngine engine, string fileName)
        {
            return engine.ReadFile(TestPath(fileName));
        }

        public static void BeginReadTest(FileHelperAsyncEngine engine, string fileName)
		{
			engine.BeginReadFile(TestPath(fileName));
		}


		private Common()
		{}
	}
}