using System;
using FileHelpers;

namespace FileHelpers.Benchmarks
{
	class Benchmark
	{

		[STAThread]
		static void Main(string[] args)
		{
		    long start = DateTime.Now.Ticks;

			FileHelperAsyncEngine engine = new FileHelperAsyncEngine (typeof(TestRecord));

			engine.BeginReadFile(@"E:\_SVN\FileHelpers\test2.csv");
			//string s;
			
			while(engine.ReadNext() != null)
			{
				
			}
			
			engine.Close();

            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - start);

            Console.WriteLine("Total Time: " + Math.Round(ts.TotalSeconds, 2));
		    Console.ReadLine();
		}

		
	}
	
	internal interface IRecordAssigner
			{
				object CreateAndAssign(object[] values);
			}
	
	
}
