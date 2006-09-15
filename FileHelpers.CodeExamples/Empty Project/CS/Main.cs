using System;
using System.Data;
using System.IO;
using FileHelpers;
using FileHelpers.RunTime;

namespace Examples
{
	class MainClass
	{
		[STAThread]
		static void Main()
		{
			FileHelperEngine engine = new FileHelperEngine(typeof(Customer));

			// To read use:
			Customer[] custs = (Customer[]) engine.ReadFile("yourfile.txt");

			// To write use:
			engine.WriteFile("yourfile.txt", custs);

			
	        //If you are using .NET 2.0 or greater is 
		    //better if you use the Generics version:

			// FileHelperEngine engine = new FileHelperEngine<Customer>();

			// To read use (no casts =)
			// Customer[] custs = engine.ReadFile("yourfile.txt");

			// To write use:
			// engine.WriteFile("yourfile.txt", custs);

		}
	}

	//------------------------
	//   RECORD CLASS (Example, change at your will)
	//   TIP: Remember to use the wizard to generate this class

	[DelimitedRecord(",")]
	public class Customer
	{
		public int CustId;
	
		public string Name;
		public decimal Balance;
		[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
		public DateTime AddedDate;
	}


}

