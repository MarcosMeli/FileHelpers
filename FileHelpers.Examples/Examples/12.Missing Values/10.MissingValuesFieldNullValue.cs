using System;
using FileHelpers;
using FileHelpers.Converters;

namespace ExamplesFx
{
    //-> Name: Handle Missing Values With FieldNullValue
    //-> Description: How to read a file with some missing values and use the <b>FieldNullValue</b> attribute

    public class ReadFileFieldNullValue
        : ExampleBase
    {
		//-> If your files contain a field that can be empty 
		//-> FileIn:Input.txt
		/*10248|VINET|04071996|32.38
10249|TOMSP||11.61
10250|HANAR|08071996|65.83
10251|VICTE|03041991|41.34*/
		//-> /File

		// And the field type needs a value (int, DateTime, etc), and you dont want to use Nullable, the FieldNullValueAttribute comes to the rescue

		//-> File:RecordClass.cs
		[DelimitedRecord("|")]
		public class Orders
		{
			public int OrderID;

			public string CustomerID;

			[DateTimeConverter("ddMMyyyy")]
			[FieldNullValue(typeof (DateTime), "1900-01-01")]
			public DateTime OrderDate;

			public decimal Freight;
		}

        //-> /File

        // Now read as usual
        public override void Run()
        {
            //-> File:Example.cs
            var engine = new FileHelperEngine<Orders>();
            var records = engine.ReadFile("Input.txt");

            foreach (var record in records) {
                Console.WriteLine(record.CustomerID);
                if (record.OrderDate != new DateTime(1900, 01, 01))
                    Console.WriteLine(record.OrderDate.ToString("dd/MM/yyyy"));
                else
                    Console.WriteLine("No Date");
                Console.WriteLine(record.Freight);
            }
            //-> /File
        }


        //-> Console
   }
}