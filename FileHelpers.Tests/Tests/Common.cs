using System.Reflection;
using FileHelpers;
using FileHelpers.MasterDetail;
using System.Collections;
using System.IO;
using MasterDetails = FileHelpers.MasterDetail.MasterDetails<object, object>;

namespace FileHelpersTests
{
	// this class only adds the relative path to the saple files.
	public static class TestCommon
	{
	    //private static string mAssemblyLocation = string.Empty;
		public static string GetPath(params string[] pathElements)
		{
		    var result = Path.GetFullPath(Path.Combine("..", "Data"));

		    foreach (var element in pathElements)
		    {
		        result = Path.Combine(result, element);
		    }

            return result;
		}

        public static string GetTempFile(string fileName)
        {
            return Path.Combine(Path.GetTempPath(), fileName);
        }

        public static object[] ReadTest(FileHelperEngine engine, params string[] pathElements)
		{
			return engine.ReadFile(GetPath(pathElements));
		}

        public static object[] ReadTest(FileHelperEngine engine, int maxRecords, params string[] pathElements)
        {
            return engine.ReadFile(GetPath(pathElements), maxRecords);
        }

        public static object[] ReadAllAsync(FileHelperAsyncEngine engine, params string[] pathElements)
		{
			ArrayList arr = new ArrayList();
			engine.BeginReadFile(GetPath(pathElements));
			while(engine.ReadNext() != null)
				arr.Add(engine.LastRecord);
			engine.Close();

			return arr.ToArray();

		}

        public static MasterDetails[] ReadTest(MasterDetailEngine engine, params string[] pathElements)
        {
            return engine.ReadFile(GetPath(pathElements));
        }

        public static void BeginReadTest(FileHelperAsyncEngine engine, params string[] pathElements)
		{
			engine.BeginReadFile(GetPath(pathElements));
		}
	}
}
