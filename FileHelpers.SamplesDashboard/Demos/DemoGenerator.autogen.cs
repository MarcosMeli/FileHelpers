using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;

namespace Demos
{
    public class DemoFactory
    {
        public static List<DemoCode> GetDemos()
        {
		    var demos = new List<DemoCode>();
            DemoCode demo;
demo = new DemoCode(new MultipleDelimiters(), "Multiple Delimiters", "Advanced");
demo.CodeDescription = @"Write a file with different delimiters using the same record";
demos.Add(demo);
demo.Files.Add(new DemoFile("Example.cs"));
demo.LastFile.Contents = @"var customers = CreateCustomers();

DelimitedFileEngine<CustomersVerticalBar> engine = new DelimitedFileEngine<CustomersVerticalBar>();
engine.WriteFile(""Out_Vertical.txt"", customers);

engine.Options.Delimiter = "";"";
engine.WriteFile(""Out_SemiColon.txt"", customers);

engine.Options.Delimiter = ""\t"";
engine.WriteFile(""Out_Tab.txt"", customers);
";
demo.Files.Add(new DemoFile("CustomersVerticalBar.cs"));
demo.LastFile.Contents = @"[DelimitedRecord(""|"")]
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

demo = new DemoCode(new ReadFile(), "Read Delimited File", "Basic");
demo.CodeDescription = @"Example of how to read a Delimited File";
demos.Add(demo);
demo.Files.Add(new DemoFile("Example.cs"));
demo.LastFile.Contents = @"public void Run()
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
demo.Files.Add(new DemoFile("Input.txt"));
demo.LastFile.Contents = @"ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
ANATR|Emparedados y Helados|Ana Trujillo|Owner|Avda. Constitución 2222|México D.F.|Mexico
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico
AROUT|Around the Horn|Thomas Hardy|Sales Representative|120 Hanover Sq.|London|UK
BERGS|Berglunds snabbköp|Christina Berglund|Administrator|Berguvsvägen  8|Luleå|Sweden
BLAUS|Blauer Delikatessen|Hanna Moos|Sales Rep|Forsterstr. 57|Mannheim|Germany
BLONP|Blondesddsl père et fils|Frédérique Citeaux|Manager|24, Kléber|Strasbourg|France
BOLID|Bólido Comidas preparadas|Martín Sommer|Owner|C/ Araquil, 67|Madrid|Spain
";

demo = new DemoCode(new WriteFile(), "Write Delimited File", "Basic");
demo.CodeDescription = @"Example of how to write a Delimited File";
demos.Add(demo);
demo.Files.Add(new DemoFile("Example.cs"));
demo.LastFile.Contents = @"var engine = new FileHelperEngine<Orders>();

var orders = new List<Orders>();

var order1 = new Orders() {OrderID = 1, CustomerID = ""AIRG"", Freight = 82.43M, OrderDate = new DateTime(2009,05,01)};
var order2 = new Orders() {OrderID = 2, CustomerID = ""JSYV"", Freight = 12.22M, OrderDate = new DateTime(2009,05,02)};

orders.Add(order1);
orders.Add(order2);

engine.WriteFile("""", orders);
";
demo.Files.Add(new DemoFile("RecordClass.cs"));
demo.LastFile.Contents = @"[DelimitedRecord(""|"")]
public class Orders
{
    public int OrderID;

    public string CustomerID;
    [FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
    public DateTime OrderDate;

    public decimal Freight;
}
";

		
           return demos;
        }
    }
}


