using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;


namespace ExamplesFx
{
    //-> Name:Read or Write Record by Record
    //-> Description:Using the FileHelperAsynEngine to work record by record
    //-> AutoRun:true

    public class ReadRecordByRecord
        : ExampleBase
    {

		//-> If you have a source file like this, separated by a |:

		//-> FileIn:Input.txt
		/*1732,Juan Perez,435.00,11-05-2002
554,Pedro Gomez,12342.30,06-02-2004
112,Ramiro Politti,0.00,01-02-2000
924,Pablo Ramirez,3321.30,24-11-2002*/
		//-> /FileIn


		//-> You first declare a Record Mapping Class:

		//-> File:RecordClass.cs

		[DelimitedRecord(",")]
		public class Customer
		{
			public int CustId;

			public string Name;

			[FieldConverter(ConverterKind.Decimal, ".")] // The decimal separator is .
			public decimal Balance;

			[FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
			public DateTime AddedDate;

		}
		//-> /File

		//->  Instantiate a FileHelperAsyncEngine and read or write files:

		public override void Run()
		{
			//-> File:Example.cs
			var engine = new FileHelperAsyncEngine<Customer>();

			// Read
			using(engine.BeginReadFile("Input.txt"))
			{
				// The engine is IEnumerable
				foreach(Customer cust in engine)
				{
					// your code here
					Console.WriteLine(cust.Name);
				}
			}


			// Write

			var arrayCustomers = new Customer[] {
				new Customer { CustId = 1732, Name = "Juan Perez", Balance = 435.00m,
					           AddedDate = new DateTime (2020, 5, 11) },
				new Customer { CustId = 554, Name = "Pedro Gomez", Balance = 12342.30m,
					           AddedDate = new DateTime (2004, 2, 6) },
			};

			using(engine.BeginWriteFile("TestOut.txt"))
			{
				foreach(Customer cust in arrayCustomers)
				{
					engine.WriteNext(cust);
				}
			}

			//-> /File
		}

	}
}