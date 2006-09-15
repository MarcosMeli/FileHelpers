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
			Delimited();

			FixedLength();

			ErrorHandling();

			FormatTransformation();

			EventHandling();

			RunTimeRecords();

			RunTimeRecordsFixed();

		}

		#region "  Delimited  "

		static void Delimited()
		{

            Console.WriteLine("Reading Delimited File...");
			Console.WriteLine();

			// Estas dos lineas son el uso de la librería
			FileHelperEngine engine = new FileHelperEngine(typeof(Customer));
			Customer[] customers = (Customer[]) engine.ReadFile(@"..\Data\CustomersDelimited.txt");

			// A partir de la versión 1.4.0 se puede
			// inclusive escribir en una sola línea:

			// Cliente[] clientes = (Cliente[]) CommonEngine.ReadFile(typeof(Cliente), @"..\Data\ClientesDelimitados.txt");

			// Aqui es donde ustedes agregan su código
			foreach (Customer cli in customers)
			{
				Console.WriteLine();
				Console.WriteLine("Customer: " + cli.CustId.ToString() + " - " + cli.Name);
				Console.WriteLine("Added Date: " + cli.AddedDate.ToString("d-M-yyyy"));
				Console.WriteLine("Balance: " + cli.Balance.ToString());
				Console.WriteLine();
				Console.WriteLine("-----------------------------");
			}

			Console.ReadLine();
			Console.WriteLine("Writing data to a delimited file...");
			Console.WriteLine();

			// write the data to a file
			engine.WriteFile("temp.txt", customers);

            Console.WriteLine("Data successful written !!!");
			Console.ReadLine();

			if (File.Exists("temp.txt")) File.Delete("temp.txt");
		}

		#endregion

		#region "  FixedLength  "

		static void FixedLength()
		{
			
			Console.WriteLine("Reading Fixed Length file...");
			Console.WriteLine();



			// Estas dos lineas son el uso de la librería
			FileHelperEngine engine = new FileHelperEngine(typeof(Customer2));
			Customer2[] clientes = (Customer2[]) engine.ReadFile(@"..\Data\CustomersFixedLength.txt");


			// A partir de la versión 1.4.0 se puede
			// inclusive escribir en una sola línea:

			// Cliente[] clientes = (Cliente[]) CommonEngine.ReadFile(typeof(Cliente), @"..\Data\ClientesDelimitados.txt");

			// Aqui es donde ustedes agregan su código
			foreach (Customer2 cli in clientes)
			{
				Console.WriteLine();
				Console.WriteLine("Customer: " + cli.CustId.ToString() + " - " + cli.Name);
				Console.WriteLine("Added Date: " + cli.AddedDate.ToString("d-M-yyyy"));
				Console.WriteLine("Balance: " + cli.Balance.ToString());
				Console.WriteLine();
				Console.WriteLine("-----------------------------");
			}

			Console.ReadLine();
			Console.WriteLine("Writing data to a Fixed Length file...");
			Console.WriteLine();

			// Esta línea es para escribir
			engine.WriteFile("temp.txt", clientes);

			Console.WriteLine("Data successful written !!!");
			Console.ReadLine();

			if (File.Exists("temp.txt")) File.Delete("temp.txt");
		}

		#endregion

		#region "  FormatTransformation  "

		static void FormatTransformation()
		{
			Console.WriteLine("Testing Format Transformation");
			Console.WriteLine();

			FileTransformEngine engine = new FileTransformEngine(typeof(Customer), typeof(Customer2));
			engine.TransformFile(@"..\Data\CustomersDelimited.txt", "temp.txt");


			Console.WriteLine("Format Transformation Successful");

			if (File.Exists("temp.txt")) File.Delete("temp.txt");
			Console.ReadLine();

		}

		#endregion

		#region "  ErrorHandling  "

		static void ErrorHandling()
		{

			Console.WriteLine("Testing error handling...");
			Console.WriteLine();

			// Estas dos lineas son el uso de la librería
			FileHelperEngine engine = new FileHelperEngine(typeof(Customer));
			
			engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            
			Customer[] customers = (Customer[]) engine.ReadFile(@"..\Data\CustomersWithErrors.txt");

			if (engine.ErrorManager.ErrorCount > 0)
			{
				Console.Write("Records: ");
				Console.WriteLine(engine.TotalRecords);

				Console.Write("Successful: ");
				Console.WriteLine(customers.Length);
				
				Console.Write("With Error: ");
				Console.WriteLine(engine.ErrorManager.ErrorCount);

				Console.Write("Error: ");
				Console.WriteLine(engine.ErrorManager.Errors[0].ExceptionInfo.Message);

			}

			engine.ErrorManager.SaveErrors("errors.txt");
		
			Console.ReadLine();

			if (File.Exists("errors.txt")) File.Delete("errors.txt");


		}

		#endregion

		#region "  EventHandling  "

		static void EventHandling()
		{

			Console.WriteLine("Reading Using EventHandlers ...");
			Console.WriteLine();

			// Estas dos lineas son el uso de la librería
			FileHelperEngine engine = new FileHelperEngine(typeof(Customer));

			engine.BeforeReadRecord += new BeforeReadRecordHandler(BeforeReadRecord);
			engine.AfterWriteRecord +=new AfterWriteRecordHandler(AfterWriteRecord);

			Customer[] customers = (Customer[]) engine.ReadFile(@"..\Data\CustomersDelimited.txt");

			// A partir de la versión 1.4.0 se puede
			// inclusive escribir en una sola línea:

			// Cliente[] clientes = (Cliente[]) CommonEngine.ReadFile(typeof(Cliente), @"..\Data\ClientesDelimitados.txt");

			// Aqui es donde ustedes agregan su código
			foreach (Customer cli in customers)
			{
				Console.WriteLine("Customer: " + cli.CustId.ToString() + " - " + cli.Name);
			}

			Console.ReadLine();
			Console.WriteLine("Writing data to a delimited file...");
			Console.WriteLine();

			// write the data to a file
			engine.WriteFile("temp.txt", customers);

			Console.WriteLine("Data successful written !!!");
			Console.ReadLine();

			if (File.Exists("temp.txt")) File.Delete("temp.txt");
		}

		private static void BeforeReadRecord(EngineBase engine, BeforeReadRecordEventArgs e)
		{
			Console.WriteLine("--> Before read line: " + e.RecordLine); 

			if (e.LineNumber == 2)
			{
				e.SkipThisRecord = true;
				Console.WriteLine("-->   skiping line 2"); 
			}
		}

		private static void AfterWriteRecord(EngineBase engine, AfterWriteRecordEventArgs e)
		{
			Console.WriteLine("--> After write record: " + e.RecordLine); 
		}
		
		#endregion


		#region "  RunTimeRecords  "

		static void RunTimeRecords()
		{

			Console.WriteLine("Run Time Records now =) ...");
			Console.WriteLine();

			DelimitedClassBuilder cb = new DelimitedClassBuilder("Customer", ",");
			cb.AddField("CustId", typeof(Int32));
			cb.AddField("Name", typeof(string));
			cb.AddField("Balance", typeof(Decimal));
			cb.AddField("AddedDate", typeof(DateTime));
			cb.LastField.Converter.Kind = ConverterKind.Date;
			cb.LastField.Converter.Arg1 = "ddMMyyyy";
			
			
			// Estas dos lineas son el uso de la librería
			FileHelperEngine engine = new FileHelperEngine(cb.CreateRecordClass());

			DataTable dt = engine.ReadFileAsDT(@"..\Data\CustomersDelimited.txt");

			// Aqui es donde ustedes agregan su código
			foreach (DataRow dr in dt.Rows)
			{
				Console.WriteLine("Customer: " + dr[0].ToString() + " - " + dr[1].ToString());
			}

		}

		static void RunTimeRecordsFixed()
		{

			Console.WriteLine("Run Time Records now =) ...");
			Console.WriteLine();

			FixedLengthClassBuilder cb = new FixedLengthClassBuilder("Customer", FixedMode.ExactLength);
			cb.AddField("CustId", 5, typeof(Int32));
			cb.AddField("Name", 20, typeof(string));
			cb.AddField("Balance", 8, typeof(Decimal));
			cb.AddField("AddedDate", 8, typeof(DateTime));
			cb.LastField.Converter.Kind = ConverterKind.Date;
			cb.LastField.Converter.Arg1 = "ddMMyyyy";
			
			
			// Estas dos lineas son el uso de la librería
			FileHelperEngine engine = new FileHelperEngine(cb.CreateRecordClass());

			DataTable dt = engine.ReadFileAsDT(@"..\Data\CustomersFixedLength.txt");

			// Aqui es donde ustedes agregan su código
			foreach (DataRow dr in dt.Rows)
			{
				Console.WriteLine("Customer: " + dr[0].ToString() + " - " + dr[1].ToString());
			}

		}
		#endregion

	}
}
