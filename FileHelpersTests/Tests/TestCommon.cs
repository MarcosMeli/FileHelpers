using FileHelpers;
using FileHelpers.MasterDetail;

namespace FileHelpersTests
{
	// this class only adds the relative path to the saple files.
	public sealed class TestCommon
	{
		public static object[] ReadTest(FileHelperEngine engine, string fileName)
		{
			return engine.ReadFile(@"..\data\" + fileName);
		}

        public static MasterDetails[] ReadTest(MasterDetailEngine engine, string fileName)
        {
            return engine.ReadFile(@"..\data\" + fileName);
        }

        public static bool BeginReadTest(FileHelperAsyncEngine engine, string fileName)
		{
			return engine.BeginReadFile(@"..\data\" + fileName);
		}


		private TestCommon()
		{
		}
	}
}