using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class Performance
	{
		static FileHelperEngine engine;

		[Test]
		public void TestTime()
		{
			long a, b;
			engine = new FileHelperEngine(typeof (OrdersVerticalBar));

			a = DateTime.Now.Ticks;

			TestCommon.ReadTest(engine, @"Good\OrdersVerticalBar.txt");

			b = DateTime.Now.Ticks;

			TimeSpan ts = new TimeSpan(b - a);
//			Console.WriteLine("Records: " + engine.TotalRecords.ToString());
//			Console.WriteLine("Time:    " + ts.TotalMilliseconds.ToString() + " ms");
		}

	}
}