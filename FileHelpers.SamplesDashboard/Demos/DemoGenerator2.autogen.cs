using FileHelpers;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers.SamplesDashboard
{
    public class DemoFactory
    {
        public static List<DemoCode> GetDemos()
        {
		    var demos = new List<DemoCode>();
            DemoCode demo;
demo = new DemoCode("Read Delimited File", "Basic");
demo.CodeDescription = @"Example of how to read a Delimited File";
demos.Add(demo);
demo.Files.Add(new DemoFile("Example.cs"));
demo.LastFile.Contents = @"var engine = new FileHelperEngine<Orders>();
var records = engine.ReadFile("""");

foreach (var record in records)
{
    Console.WriteLine(record.CustomerID);
    Console.WriteLine(record.OrderDate.ToString(""dd/MM/yyyy""));
    Console.WriteLine(record.Freight);
}
";
demo.Files.Add(new DemoFile("OrdersRecord.cs"));
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
demo.Files.Add(new DemoFile("SampleFile.txt"));
demo.LastFile.Contents = @"ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
ANATR|Emparedados y Helados|Ana Trujillo|Owner|Avda. Constitución 2222|México D.F.|Mexico
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico
AROUT|Around the Horn|Thomas Hardy|Sales Representative|120 Hanover Sq.|London|UK
BERGS|Berglunds snabbköp|Christina Berglund|Administrator|Berguvsvägen  8|Luleå|Sweden
BLAUS|Blauer Delikatessen|Hanna Moos|Sales Rep|Forsterstr. 57|Mannheim|Germany
BLONP|Blondesddsl père et fils|Frédérique Citeaux|Manager|24, Kléber|Strasbourg|France
BOLID|Bólido Comidas preparadas|Martín Sommer|Owner|C/ Araquil, 67|Madrid|Spain
";

demo = new DemoCode("Write Delimited File", "Basic");
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


