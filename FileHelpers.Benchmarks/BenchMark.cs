using System;
using FileHelpers;

namespace FileHelpers.Benchmarks
{
	class Benchmark
	{

		[STAThread]
		static void Main(string[] args)
		{
			FileHelperAsyncEngine engine = new FileHelperAsyncEngine (typeof(TestRecord));

			engine.BeginReadFile(@"E:\_SVN\FileHelpers\test2.csv");
			//string s;
			
			while(engine.ReadNext() != null)
			{
				
			}
			
			engine.Close();
		}

		
	}
	
	internal interface IRecordAssigner
			{
				object CreateAndAssign(object[] values);
			}
	
	
}
