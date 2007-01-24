using System;
using FileHelpers;

namespace FileHelpersSamples
{
	public class EasySample
	{
		[DelimitedRecord("|")]
		public class Orders
		{
			public int OrderID;
			public string CustomerID;
			[FieldConverter(ConverterKind.Date, "ddMMyyyy")] public DateTime OrderDate;
			public decimal Freight;
		}

		public void ReadWrite()
		{
			
			FileHelperEngine engine = new FileHelperEngine(typeof (Orders));
 
			// to Read use:
			Orders[] res = (Orders[]) engine.ReadFile(@"C:\TestIn.txt");

			// to Write use:
			engine.WriteFile(@"C:\TestOut.txt", res);

			foreach (Orders order in res)
			{
				Console.WriteLine("Order Info:");
				Console.WriteLine(order.CustomerID + " - " +
					order.OrderDate.ToString("dd/MM/yy"));
			}

			FileHelperAsyncEngine asyncEngine = new FileHelperAsyncEngine(typeof (Orders));

			asyncEngine.BeginReadFile(@"C:\TestIn.txt");

			Orders ord;

			while (asyncEngine.ReadNext() != null)
			{
				ord = (Orders) asyncEngine.LastRecord;
				// your code here
				Console.WriteLine(ord.CustomerID);

			}
		}

	}
}