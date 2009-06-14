using System.Reflection;
using FileHelpers;
using FileHelpers.MasterDetail;
using System.Collections;
using System.IO;
using MasterDetails = FileHelpers.MasterDetail.MasterDetails<object, object>;

namespace FileHelpersTests
{
	// this class only adds the relative path to the saple files.
	public sealed class TestCommon
	{
	    //private static string mAssemblyLocation = string.Empty;
		public static string GetPath(string fileName)
		{
            //if (string.IsNullOrEmpty(mAssemblyLocation))
            //{
            //    mAssemblyLocation = Path.GetDirectoryName(Assembly.GetAssembly(typeof (Common)).Location);
            //}

            return Path.Combine(Path.GetFullPath(@"..\data"), fileName);
		}

//		public static string FullTestPath(string fileName)
//		{
//			return Path.GetDirectoryName(typeof(Common).Assembly.GetModules()[0].FullyQualifiedName) + @"\..\data\" + fileName;
//		}

		public static object[] ReadTest(FileHelperEngine engine, string fileName)
		{
			return engine.ReadFile(GetPath(fileName));
		}

        public static object[] ReadTest(FileHelperEngine engine, string fileName, int maxRecords)
        {
            return engine.ReadFile(GetPath(fileName), maxRecords);
        }

		public static object[] ReadAllAsync(FileHelperAsyncEngine engine, string fileName)
		{
			ArrayList arr = new ArrayList();
			engine.BeginReadFile(GetPath(fileName));
			while(engine.ReadNext() != null)
				arr.Add(engine.LastRecord);
			engine.Close();

			return arr.ToArray();

		}

		public static MasterDetails[] ReadTest(MasterDetailEngine engine, string fileName)
        {
            return engine.ReadFile(GetPath(fileName));
        }

        public static void BeginReadTest(FileHelperAsyncEngine engine, string fileName)
		{
			engine.BeginReadFile(GetPath(fileName));
		}


		private TestCommon()
		{}
	}
}