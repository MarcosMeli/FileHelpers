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
work.Language = NetLanguage.CSharp;
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
work.Language = NetLanguage.CSharp;
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
work.Language = NetLanguage.CSharp;
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
work.Language = NetLanguage.CSharp;
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
work.Language = NetLanguage.CSharp;
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
work.Language = NetLanguage.CSharp;
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
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);

demo = new DemoCode(new EnumConverterDemo(), "Enum Converter Example", "Advanced");
demo.CodeDescription = @"When you have a string field in your files that can be better handled if you map it to an enum.";
demos.Add(demo);
work = new DemoFile("TheEnumerator.cs");
work.Contents = @"/// <summary>
/// Different titles describing position in company
/// </summary>
public enum CustomerTitle
{
    Owner,
    SalesRepresentative,
    MarketingManager
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("RunEngine.cs");
work.Contents = @"/// <summary>
/// Run an example of writing a delimited file and 
/// changing the delimiter to show how it is done.
/// </summary>
public void Run()
{
    var engine = new DelimitedFileEngine<Customer>();

    //  Read input records, enumeration automatically converted
    Customer[] customers =  engine.ReadFile(""Input.txt"");

    foreach (var cust in customers)
    {
        Console.WriteLine(""Customer name {0} is a {1}"", cust.ContactName, cust.ContactTitle);
    }
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Customers with Enum.cs");
work.Contents = @"/// <summary>
/// Sample customer class that is delimited by | default
/// </summary>
/// <remarks>
/// Notice last feild is our enumerator
/// </remarks>
[DelimitedRecord(""|"")]
public class Customer
{
    public string CustomerID;
    public string CompanyName;
    public string ContactName;
    public CustomerTitle ContactTitle;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Input.txt");
work.Contents = @"ALFKI|Alfreds Futterkiste|Maria Anders|SalesRepresentative
ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|Owner
FRANR|France restauration|Carine Schmitt|MarketingManager
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner
";
work.Status = DemoFile.FileType.InputFile;
demo.Files.Add(work);

demo = new DemoCode(new SimpleErrorHandlingDemo(), "Simple Error handling", "ErrorHandling");
demo.CodeDescription = @"Read the file or reject the whole file";
demos.Add(demo);
work = new DemoFile("RunEngine.cs");
work.Contents = @"/// <summary>
/// Run an example of running a file with an error through and
/// using a try catch to collect the error.
/// </summary>
/// <remarks>
/// In the standard mode you can catch the exceptions when something fails.
/// </remarks>
public void Run()
{
    try
    {
        var engine = new DelimitedFileEngine<Customer>();

        //  This fails with not in enumeration error
        Customer[] customers = engine.ReadFile(""Input.txt"");

        // this will not happen because of the exception
        foreach (var cust in customers)
        {
            Console.WriteLine(""Customer name {0} is a {1}"", cust.ContactName, cust.ContactTitle);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Customers with Enum.cs");
work.Contents = @"/// <summary>
/// Sample customer class that is delimited by | default
/// </summary>
/// <remarks>
/// Notice last feild is our enumerator
/// </remarks>
[DelimitedRecord(""|"")]
public class Customer
{
    public string CustomerID;
    public string CompanyName;
    public string ContactName;
    public CustomerTitle ContactTitle;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Input.txt");
work.Contents = @"ALFKI|Alfreds Futterkiste|Maria Anders|SalesRepresentative
ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|NotInEnum
FRANR|France restauration|Carine Schmitt|MarketingManager
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner
";
work.Status = DemoFile.FileType.InputFile;
demo.Files.Add(work);
work = new DemoFile("TheEnumerator.cs");
work.Contents = @"/// <summary>
/// Different titles describing position in company
/// </summary>
public enum CustomerTitle
{
    Owner,
    SalesRepresentative,
    MarketingManager
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);

demo = new DemoCode(new ErrorModeErrorHandlingDemo(), "ErrorMode Error handling", "ErrorHandling");
demo.CodeDescription = @"Read the file rejecting bad records";
demos.Add(demo);
work = new DemoFile("RunEngine.cs");
work.Contents = @"/// <summary>
/// Run an example of running a file with an error using the
/// ErrorMode option to capture bad records
/// </summary>
/// <remarks>
/// In the standard mode you can catch the exceptions when something fails.
/// </remarks>
public void Run()
{
    var engine = new DelimitedFileEngine<Customer>();

    // Switch error mode on
    engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

    //  This fails with not in enumeration error
    Customer[] customers = engine.ReadFile(""Input.txt"");

    // This will display error from line 2 of the file.
    foreach (ErrorInfo err in engine.ErrorManager.Errors)
    {
        Console.WriteLine();
        Console.WriteLine(""Error on Line number: {0}"", err.LineNumber);
        Console.WriteLine(""Record causing the problem: {0}"", err.RecordString);
        Console.WriteLine(""Complete exception information: {0}"", err.ExceptionInfo.ToString());
    }
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Customers with Enum.cs");
work.Contents = @"/// <summary>
/// Sample customer class that is delimited by | default
/// </summary>
/// <remarks>
/// Notice last feild is our enumerator
/// </remarks>
[DelimitedRecord(""|"")]
public class Customer
{
    public string CustomerID;
    public string CompanyName;
    public string ContactName;
    public CustomerTitle ContactTitle;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Input.txt");
work.Contents = @"ALFKI|Alfreds Futterkiste|Maria Anders|SalesRepresentative
ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|NotInEnum
FRANR|France restauration|Carine Schmitt|MarketingManager
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner
";
work.Status = DemoFile.FileType.InputFile;
demo.Files.Add(work);
work = new DemoFile("TheEnumerator.cs");
work.Contents = @"/// <summary>
/// Different titles describing position in company
/// </summary>
public enum CustomerTitle
{
    Owner,
    SalesRepresentative,
    MarketingManager
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);

demo = new DemoCode(new IgnoreModeErrorHandlingDemo(), "Ignore Mode Error handling", "ErrorHandling");
demo.CodeDescription = @"Read the file dropping bad records";
demos.Add(demo);
work = new DemoFile("RunEngine.cs");
work.Contents = @"/// <summary>
/// Run an example of running a file with an error using the
/// IgnoreMode option to silently drop bad records
/// </summary>
public void Run()
{
    var engine = new DelimitedFileEngine<Customer>();

    // Switch error mode on
    engine.ErrorManager.ErrorMode = ErrorMode.IgnoreAndContinue;

    //  This fails with not in enumeration error
    Customer[] customers = engine.ReadFile(""Input.txt"");

    // This wont display anything, we have dropped it
    foreach (ErrorInfo err in engine.ErrorManager.Errors)
    {
        Console.WriteLine();
        Console.WriteLine(""Error on Line number: {0}"", err.LineNumber);
        Console.WriteLine(""Record causing the problem: {0}"", err.RecordString);
        Console.WriteLine(""Complete exception information: {0}"", err.ExceptionInfo.ToString());
    }

    // This will display only 3 of the four records
    foreach (var cust in customers)
    {
        Console.WriteLine(""Customer name {0} is a {1}"", cust.ContactName, cust.ContactTitle);
    }
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Customers with Enum.cs");
work.Contents = @"/// <summary>
/// Sample customer class that is delimited by | default
/// </summary>
/// <remarks>
/// Notice last feild is our enumerator
/// </remarks>
[DelimitedRecord(""|"")]
public class Customer
{
    public string CustomerID;
    public string CompanyName;
    public string ContactName;
    public CustomerTitle ContactTitle;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Input.txt");
work.Contents = @"ALFKI|Alfreds Futterkiste|Maria Anders|SalesRepresentative
ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|NotInEnum
FRANR|France restauration|Carine Schmitt|MarketingManager
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner
";
work.Status = DemoFile.FileType.InputFile;
demo.Files.Add(work);
work = new DemoFile("TheEnumerator.cs");
work.Contents = @"/// <summary>
/// Different titles describing position in company
/// </summary>
public enum CustomerTitle
{
    Owner,
    SalesRepresentative,
    MarketingManager
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);

demo = new DemoCode(new ErrorSaveErrorHandlingDemo(), "ErrorMode saving Errors", "ErrorHandling");
demo.CodeDescription = @"Read the file saving bad records";
demos.Add(demo);
work = new DemoFile("RunEngine.cs");
work.Contents = @"/// <summary>
/// Run an example of running a file with an error using the
/// ErrorMode option to capture bad records and then saving them
/// </summary>
public void Run()
{
    var engine = new DelimitedFileEngine<Customer>();

    // Switch error mode on
    engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

    //  This fails with not in enumeration error
    Customer[] customers = engine.ReadFile(""Input.txt"");

    if (engine.ErrorManager.HasErrors)
        engine.ErrorManager.SaveErrors(""errors.out"");

    // sometime later you can read it back
    ErrorInfo[] errors = ErrorManager.LoadErrors(""errors.out"");

    // This will display error from line 2 of the file.
    foreach (var err in errors)
    {
        Console.WriteLine();
        Console.WriteLine(""Error on Line number: {0}"", err.LineNumber);
        Console.WriteLine(""Record causing the problem: {0}"", err.RecordString);
        Console.WriteLine(""Complete exception information: {0}"", err.ExceptionInfo.ToString());
    }
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Customers with Enum.cs");
work.Contents = @"/// <summary>
/// Sample customer class that is delimited by | default
/// </summary>
/// <remarks>
/// Notice last feild is our enumerator
/// </remarks>
[DelimitedRecord(""|"")]
public class Customer
{
    public string CustomerID;
    public string CompanyName;
    public string ContactName;
    public CustomerTitle ContactTitle;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Input.txt");
work.Contents = @"ALFKI|Alfreds Futterkiste|Maria Anders|SalesRepresentative
ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|NotInEnum
FRANR|France restauration|Carine Schmitt|MarketingManager
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner
";
work.Status = DemoFile.FileType.InputFile;
demo.Files.Add(work);
work = new DemoFile("TheEnumerator.cs");
work.Contents = @"/// <summary>
/// Different titles describing position in company
/// </summary>
public enum CustomerTitle
{
    Owner,
    SalesRepresentative,
    MarketingManager
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Errors.out");
work.Contents = @"";
work.Status = DemoFile.FileType.OutputFile;
demo.Files.Add(work);

demo = new DemoCode(new ReadBeforeEventSample(), "Read Before Event Handling", "Events");
demo.CodeDescription = @"Show how to implement read before event";
demos.Add(demo);
work = new DemoFile("RunEngine.cs");
work.Contents = @"/// <summary>
/// reads report.inp and skips all the records that are not detail records using a simple criteria
/// </summary>
public void Run()
{
    var engine = new FileHelperEngine<OrdersFixed>();
    engine.BeforeReadRecord += new BeforeReadHandler<OrdersFixed>(BeforeEvent);
    var result = engine.ReadFile(""report.inp"");

    foreach (var value in result)
    {
        Console.WriteLine(""Customer: {0} Freight: {1}"", value.CustomerID, value.Freight);
    }
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("report.inp");
work.Contents = @"-----------------------------------------------------
              XXX Enterprise
-----------------------------------------------------
10249   TOMSP  05071996      11.61
10250   HANAR  08071996      65.83
10251   VICTE  08071996      41.34
                                       Page 1
-----------------------------------------------------
              YYY Enterprise
-----------------------------------------------------
10269   TOMSP  05071996      11.61
10230   HANAR  08071996      65.83
10151   VICTE  08071996      41.34

";
work.Status = DemoFile.FileType.InputFile;
demo.Files.Add(work);
work = new DemoFile("Report layout.cs");
work.Contents = @"/// <summary>
/// Layout of the records we want for the report in report.inp
/// </summary>
/// <remarks>
/// This only covers the detail records
/// </remarks>
[FixedLengthRecord(FixedMode.AllowVariableLength)]
[IgnoreEmptyLines]
public class OrdersFixed
{
    [FieldFixedLength(7)]
    public int OrderID;

    [FieldFixedLength(8)]
    public string CustomerID;

    [FieldFixedLength(8)]
    public DateTime OrderDate;

    [FieldFixedLength(11)]
    public decimal Freight;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("EventHandler.cs");
work.Contents = @"private void BeforeEvent(EngineBase engine, BeforeReadEventArgs<OrdersFixed> e)
{
    if (e.RecordLine.StartsWith("" "") || e.RecordLine.StartsWith(""-""))
        e.SkipThisRecord = true;

    //  Sometimes changing the record line can be useful, for example to correct for
    //  a bad data layout.  Here is an example of this, commented out for this example

    //if (e.RecordLine.StartsWith("" ""))
    //   e.RecordLine = ""Be careful!"";
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);

demo = new DemoCode(new ReadAfterEventSample(), "Read After Event Handling", "Events");
demo.CodeDescription = @"Show how to implement read after event";
demos.Add(demo);
work = new DemoFile("RunEngine.cs");
work.Contents = @"/// <summary>
/// Read a simple file and ignore zero value freight using a Read After Event
/// </summary>
public void Run()
{
    var engine = new FileHelperEngine<OrdersFixed>();
    engine.AfterReadRecord += new AfterReadHandler<OrdersFixed>(AfterEvent);

    var result = engine.ReadFile(""Input.txt"");

    foreach (var value in result)
    {
        Console.WriteLine(""Customer: {0} Freight: {1}"", value.CustomerID, value.Freight);
    }
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Input.txt");
work.Contents = @"10249   TOMSP  05071996      11.61
10250   HANAR  08071996       0.00
10251   VICTE  08071996      41.34
10269   TOMSP  05071996      11.61
10230   HANAR  08071996      65.83
10151   VICTE  08071996      41.34

";
work.Status = DemoFile.FileType.InputFile;
demo.Files.Add(work);
work = new DemoFile("Report layout.cs");
work.Contents = @"/// <summary>
/// Layout of the all input records from Input.txt
/// </summary>
[FixedLengthRecord(FixedMode.AllowVariableLength)]
[IgnoreEmptyLines]
public class OrdersFixed
{
    [FieldFixedLength(7)]
    public int OrderID;

    [FieldFixedLength(8)]
    public string CustomerID;

    [FieldFixedLength(8)]
    public DateTime OrderDate;

    [FieldFixedLength(11)]
    public decimal Freight;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("EventHandler.cs");
work.Contents = @"private void AfterEvent(EngineBase engine, AfterReadEventArgs<OrdersFixed> e)
{
    //  we want to drop all records with no freight
    if (e.Record.Freight == 0)
        e.SkipThisRecord = true;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);

demo = new DemoCode(new WriteBeforeEventSample(), "Write Before Event Handling", "Events");
demo.CodeDescription = @"Show how to implement write before event";
demos.Add(demo);
work = new DemoFile("RunEngine.cs");
work.Contents = @"/// <summary>
/// Run a record through engine using the write event to filter out unwanted details
/// </summary>
public void Run()
{
    var engine = new FileHelperEngine<OrdersFixed>();

    var result = engine.ReadFile(""Input.txt"");

    //  add our filter logic.
    engine.BeforeWriteRecord += new BeforeWriteHandler<OrdersFixed>(BeforeWriteEvent);
    engine.WriteFile(""output.txt"", result);
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("EventHandler.cs");
work.Contents = @"private void BeforeWriteEvent(EngineBase engine, BeforeWriteEventArgs<OrdersFixed> e)
{
    //  We only want clients with large frieght values
    if (e.Record.Freight < 40)
        e.SkipThisRecord = true;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Input.txt");
work.Contents = @"10249   TOMSP  05071996      11.61
10250   HANAR  08071996       0.00
10251   VICTE  08071996      41.34
10269   TOMSP  05071996      11.61
10230   HANAR  08071996      65.83
10151   VICTE  08071996      41.34

";
work.Status = DemoFile.FileType.InputFile;
demo.Files.Add(work);
work = new DemoFile("output.txt");
work.Contents = @"";
work.Status = DemoFile.FileType.OutputFile;
demo.Files.Add(work);
work = new DemoFile("Report layout.cs");
work.Contents = @"/// <summary>
/// Layout of the records we want for the report in Input.txt
/// </summary>
[FixedLengthRecord(FixedMode.AllowVariableLength)]
[IgnoreEmptyLines]
public class OrdersFixed
{
    [FieldFixedLength(7)]
    public int OrderID;

    [FieldFixedLength(8)]
    public string CustomerID;

    [FieldFixedLength(8)]
    public DateTime OrderDate;

    [FieldFixedLength(11)]
    public decimal Freight;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);

demo = new DemoCode(new WriteAfterEventSample(), "Write After Event Handling", "Events");
demo.CodeDescription = @"Show how to implement write after event";
demos.Add(demo);
work = new DemoFile("RunEngine.cs");
work.Contents = @"/// <summary>
/// Run a record through engine using the write event to filter out unwanted details
/// </summary>
public void Run()
{
    var engine = new FileHelperEngine<OrdersFixed>();

    var result = engine.ReadFile(""Input.txt"");

    //  add our filter logic.
    engine.AfterWriteRecord += new AfterWriteHandler<OrdersFixed>(AfterWriteEvent);
    engine.WriteFile(""output.txt"", result);
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("EventHandler.cs");
work.Contents = @"private void AfterWriteEvent(EngineBase engine, AfterWriteEventArgs<OrdersFixed> e)
{
   //  We only want clients with large frieght values
    if (e.Record.CustomerID == ""HANAR"" )
        e.RecordLine = ""Insufficient Access"";
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Input.txt");
work.Contents = @"10249   TOMSP  05071996      11.61
10250   HANAR  08071996       0.00
10251   VICTE  08071996      41.34
10269   TOMSP  05071996      11.61
10230   HANAR  08071996      65.83
10151   VICTE  08071996      41.34

";
work.Status = DemoFile.FileType.InputFile;
demo.Files.Add(work);
work = new DemoFile("output.txt");
work.Contents = @"";
work.Status = DemoFile.FileType.OutputFile;
demo.Files.Add(work);
work = new DemoFile("Report layout.cs");
work.Contents = @"/// <summary>
/// Layout of the records we want for the report in Input.txt
/// </summary>
[FixedLengthRecord(FixedMode.AllowVariableLength)]
[IgnoreEmptyLines]
public class OrdersFixed
{
    [FieldFixedLength(7)]
    public int OrderID;

    [FieldFixedLength(8)]
    [FieldTrim(TrimMode.Both)]
    public string CustomerID;

    [FieldFixedLength(8)]
    public DateTime OrderDate;

    [FieldFixedLength(11)]
    public decimal Freight;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);

demo = new DemoCode(new SimpleMasterDetailSample(), "Simple Master Detail sample", "MasterDetail");
demo.CodeDescription = @"Show how to implement Master detail reading using a selection subroutine";
demos.Add(demo);
work = new DemoFile("RunEngine.cs");
work.Contents = @"/// <summary>
/// Run a record through engine using a selector to create a master detail input
/// </summary>
public void Run()
{
    var engine = new MasterDetailEngine<Customers, Orders>(new MasterDetailSelector(ExampleSelector));

    var result = engine.ReadFile(""Input.txt"");

    foreach (var group in result)
    {
        Console.WriteLine(""Customer: {0}"", group.Master.CustomerID);
        foreach (var detail in group.Details)
        {
            Console.WriteLine(""    Freight: {0}"", detail.Freight);
        }
    }
}

/// <summary>
/// Selector to determine whether we have a master or
/// detail record to import
/// </summary>
/// <param name=""record"">Alpha characters coming in</param>
/// <returns>Selector for master or detail record</returns>
FileHelpers.MasterDetail.RecordAction ExampleSelector(string record)
{
    if (record.Length < 2)
        return RecordAction.Skip;

    if (Char.IsLetter(record[0]))
        return FileHelpers.MasterDetail.RecordAction.Master;
    else
        return FileHelpers.MasterDetail.RecordAction.Detail;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Input.txt");
work.Contents = @"ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
10248|ALFKI|5|04071996|01081996|16071996|3|32.38
10249|ALFKI|6|05071996|16081996|10071996|1|11.61
10251|ALFKI|3|08071996|05081996|15071996|1|41.34
ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|Owner|Avda. de la Constitución 2222|México D.F.|Mexico
10252|ANATR|4|09071996|06081996|11071996|2|51.3
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico

";
work.Status = DemoFile.FileType.InputFile;
demo.Files.Add(work);
work = new DemoFile("output.txt");
work.Contents = @"";
work.Status = DemoFile.FileType.OutputFile;
demo.Files.Add(work);
work = new DemoFile("Master layout.cs");
work.Contents = @"/// <summary>
/// Layout of the master records beginning with alpha characters in input
/// </summary>
[DelimitedRecord(""|"")]
public class Customers
{
    public string CustomerID;
    public string CompanyName;
    public string ContactName;
    public string ContactTitle;
    public string Address;
    public string City;
    public string Country;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Detail layout.cs");
work.Contents = @"/// <summary>
/// Layout of the detail records beginning with numerics in input
/// </summary>
[DelimitedRecord(""|"")]
public class Orders
{
    public int OrderID;
    public string CustomerID;
    public int EmployeeID;
    public DateTime OrderDate;
    public DateTime RequiredDate;
    public DateTime ShippedDate;
    public int ShipVia;
    public decimal Freight;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);

demo = new DemoCode(new SecondMasterDetailSample(), "Master Detail sample", "MasterDetail");
demo.CodeDescription = @"Show how to implement Master detail reading where record contains characters";
demos.Add(demo);
work = new DemoFile("RunEngine.cs");
work.Contents = @"/// <summary>
/// Run a record through engine using a Common selector where master contains a characrter to create a master detail input
/// </summary>
public void Run()
{
    var engine = new MasterDetailEngine<Customers, Orders>
                                 (CommonSelector.MasterIfContains, ""@"");
    // to Read use:
    var res = engine.ReadFile(""Input.txt"");

    // to Write use:
    engine.WriteFile(""Output.txt"", res);
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Input.txt");
work.Contents = @"@ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
10248|ALFKI|5|04071996|01081996|16071996|3|32.38
10249|ALFKI|6|05071996|16081996|10071996|1|11.61
10251|ALFKI|3|08071996|05081996|15071996|1|41.34
@ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|Owner|Avda. de la Constitución 2222|México D.F.|Mexico
10252|ANATR|4|09071996|06081996|11071996|2|51.3
@ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico

";
work.Status = DemoFile.FileType.InputFile;
demo.Files.Add(work);
work = new DemoFile("Output.txt");
work.Contents = @"";
work.Status = DemoFile.FileType.OutputFile;
demo.Files.Add(work);
work = new DemoFile("Master layout.cs");
work.Contents = @"/// <summary>
/// Layout of the master records beginning with alpha characters in input
/// </summary>
[DelimitedRecord(""|"")]
[IgnoreEmptyLines]
public class Customers
{
    public string CustomerID;
    public string CompanyName;
    public string ContactName;
    public string ContactTitle;
    public string Address;
    public string City;
    public string Country;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);
work = new DemoFile("Detail layout.cs");
work.Contents = @"/// <summary>
/// Layout of the detail records beginning with numerics in input
/// </summary>
[DelimitedRecord(""|"")]
public class Orders
{
    public int OrderID;
    public string CustomerID;
    public int EmployeeID;
    public DateTime OrderDate;
    public DateTime RequiredDate;
    public DateTime ShippedDate;
    public int ShipVia;
    public decimal Freight;
}
";
work.Language = NetLanguage.CSharp;
demo.Files.Add(work);

		
           return demos;
        }
    }
}


