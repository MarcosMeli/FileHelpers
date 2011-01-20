using System;
using FileHelpers;

namespace FileHelpersSamples
{
    /// <summary>
    /// Create an easy example of processing
    /// with the Async engine without any code to break up records
    /// or interpret the individual fields
    /// </summary>
	public class EasySample
	{
        /// <summary>
        /// Layout tour data, fields are in file order
        /// </summary>
		[DelimitedRecord("|")]
		public class Orders
		{
            /// <summary>
            /// Field before the first bar, must be numeric
            /// </summary>
			public int OrderID;

            /// <summary>
            /// Field after second bar
            /// </summary>
			public string CustomerID;

            /// <summary>
            /// Field after the second bar must be in format ddMMyyyy
            /// </summary>
			[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime OrderDate;

            /// <summary>
            /// Field after third bar, must be a numeric with optional decimal point
            /// </summary>
			public decimal Freight;
		}


        /// <summary>
        /// Process the delimited file twice,
        /// once with the simple engine, once with Async
        /// </summary>
        /// <remarks>
        /// This is a simple sample of using the Normal engine and the Async engine
        /// </remarks>
		public void ReadWrite()
		{
			
			var engine = new FileHelperEngine<Orders>();
 
			// to Read use:
			Orders[] res = engine.ReadFile(@"C:\TestIn.txt");

			// to Write use:
			engine.WriteFile(@"C:\TestOut.txt", res);

			foreach (Orders order in res)
			{
				Console.WriteLine("Order Info:");
				Console.WriteLine(order.CustomerID + " - " +
					order.OrderDate.ToString("dd/MM/yy"));
			}

			var asyncEngine = new FileHelperAsyncEngine<Orders>();

			asyncEngine.BeginReadFile(@"C:\TestIn.txt");

			Orders ord;

			while (asyncEngine.ReadNext() != null)
			{
				ord = asyncEngine.LastRecord;
				// your code here
				Console.WriteLine(ord.CustomerID);
			}
		}
	}
}