using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;

namespace Demos
{
    public class DemoFactory
    {
	    static DemoFile work;

        public static List<DemoCode> GetDemos()
        {
		    var demos = new List<DemoCode>();
            DemoCode demo;
demo = new DemoCode(new ReadFile(), "Read Delimited File", "Basic");
demo.CodeDescription = @"Example of how to read a Delimited File";
demos.Add(demo);
work = new DemoFile("Example.cs");
work.Contents = @"/// <summary>
/// Execute the engine and get some results
/// </summary>
public void Run()
{
    var engine = new FileHelperEngine<Orders>();
    var records = engine.ReadFile(""Input.txt"");

    foreach (var record in records)
    {
        Console.WriteLine(record.CustomerID);
        Console.WriteLine(record.OrderDate.ToString(""dd/MM/yyyy""));
        Console.WriteLine(record.Freight);
    }
}
";
demo.Files.Add(work);
work = new DemoFile("RecordClass.cs");
work.Contents = @"/// <summary>
/// Our class we are reading using FileHelpers,  the record breakdown
/// </summary>
[DelimitedRecord(""|"")]
public class Orders
{
    public int OrderID;

    public string CustomerID;

    [FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
    public DateTime OrderDate;

    public decimal Freight;
}
";
demo.Files.Add(work);
work = new DemoFile("Input.txt");
work.Contents = @"10248|VINET|04071996|32.38
10249|TOMSP|05071996|11.61
10250|HANAR|08071996|65.83
10251|VICTE|08071996|41.34
";
work.Status = DemoFile.FileType.InputFile;
demo.Files.Add(work);

demo = new DemoCode(new WriteFile(), "Write Delimited File", "Basic");
demo.CodeDescription = @"Example of how to write a Delimited File";
demos.Add(demo);
work = new DemoFile("Example.cs");
work.Contents = @"var engine = new FileHelperEngine<Orders>();

var orders = new List<Orders>();

var order1 = new Orders() {OrderID = 1, CustomerID = ""AIRG"", Freight = 82.43M, OrderDate = new DateTime(2009,05,01)};
var order2 = new Orders() {OrderID = 2, CustomerID = ""JSYV"", Freight = 12.22M, OrderDate = new DateTime(2009,05,02)};

orders.Add(order1);
orders.Add(order2);

engine.WriteFile(""Output.Txt"", orders);
";
demo.Files.Add(work);
work = new DemoFile("RecordClass.cs");
work.Contents = @"/// <summary>
/// Layout for a file delimited by |
/// </summary>
[DelimitedRecord(""|"")]
public class Orders
{
    public int OrderID;

    public string CustomerID;

    [FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
    public DateTime OrderDate;

    public decimal Freight;
}
";
demo.Files.Add(work);
work = new DemoFile("Output.Txt");
work.Contents = @"";
work.Status = DemoFile.FileType.OutputFile;
demo.Files.Add(work);

demo = new DemoCode(new MultipleDelimiters(), "Multiple Delimiters", "Advanced");
demo.CodeDescription = @"Write a file with different delimiters using the same record";
demos.Add(demo);
work = new DemoFile("RunEngine.cs");
work.Contents = @"/// <summary>
/// Run an example of writing a delimited file and 
/// changing the delimiter to show how it is done.
/// </summary>
public void Run()
{
    var customers = CreateCustomers();

    var engine = new DelimitedFileEngine<CustomersVerticalBar>();
    //  write out customers using a vertical bar delimiter (default)
    engine.WriteFile(""Out_Vertical.txt"", customers);

    // Change the delimiter to semicolon and write that out
    engine.Options.Delimiter = "";"";
    engine.WriteFile(""Out_SemiColon.txt"", customers);

    // Change the delimiter to a tab and write that out
    engine.Options.Delimiter = ""\t"";
    engine.WriteFile(""Out_Tab.txt"", customers);

}
";
demo.Files.Add(work);
work = new DemoFile("CreateCustomers.cs");
work.Contents = @"        /// <summary>
        /// This routine reads the data and creates an array of Customers for our samples
        /// </summary>
        /// <returns>Array of customers</returns>
        private CustomersVerticalBar[] CreateCustomers()
        {
            //  6 records of sample data to parse
            string tempCustomers = @""ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
ANATR|Emparedados y Helados|Ana Trujillo|Owner|Avda. Constitución 2222|México D.F.|Mexico
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico
BERGS|Berglunds snabbköp|Christina Berglund|Administrator|Berguvsvägen  8|Luleå|Sweden
BLAUS|Blauer Delikatessen|Hanna Moos|Sales Rep|Forsterstr. 57|Mannheim|Germany
BOLID|Bólido Comidas preparadas|Martín Sommer|Owner|C/ Araquil, 67|Madrid|Spain"";

            // use the common engine to break down the records above
            return CommonEngine.ReadString<CustomersVerticalBar>(tempCustomers);
        }
";
demo.Files.Add(work);
work = new DemoFile("CustomersVerticalBar.cs");
work.Contents = @"/// <summary>
/// Sample class that is delimited by | default
/// </summary>
/// <remarks>
/// Order of fields in the class is the same as the order in the file
/// </remarks>
[DelimitedRecord(""|"")]
public class CustomersVerticalBar
{
    public string CustomerID;
    public string CompanyName;
    public string ContactName;
    public string ContactTitle;
    public string Address;
    public string City;
    public string Country;

    //-> To display in the PropertyGrid.
    public override string ToString()
    {
        return CustomerID + "" - "" + CompanyName + "", "" + ContactName;
    }
}
";
demo.Files.Add(work);

		
           return demos;
        }
    }
}


