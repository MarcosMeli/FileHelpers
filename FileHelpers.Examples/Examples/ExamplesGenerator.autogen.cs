

using System;
using System.Collections.Generic;
using System.Text;
using ExamplesFx;


namespace Examples
{
public class ExamplesFactory
{
static ExampleFile file;

        public static List<ExampleCode> GetExamples()
        {
            var examples = new List<ExampleCode>();
            ExampleCode example;
            example = new ExampleCode(new ReadFileDelimited(), "Read Delimited File", "QuickStart", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\10.QuickStart\10.ReadFileDelimited.cs");
            example.Description = @"How to read a Delimited File";
            example.AutoRun = true;
            examples.Add(example);
            file = new ExampleFile("RecordClass.cs");
            file.Contents = @"     [DelimitedRecord(""|"")]
public class Orders
{
public int OrderID;

public string CustomerID;

[FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
public DateTime OrderDate;

[FieldConverter(ConverterKind.Decimal, ""."")] // The decimal separator is .
public decimal Freight;
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Example.cs");
            file.Contents = @"var engine = new FileHelperEngine<Orders>();
var records = engine.ReadFile(""Input.txt"");

foreach (var record in records)
{
Console.WriteLine(record.CustomerID);
Console.WriteLine(record.OrderDate.ToString(""dd/MM/yyyy""));
Console.WriteLine(record.Freight);
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new WriteFileDelimited(), "Write Delimited File", "QuickStart", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\10.QuickStart\20.WriteFileDelimited.cs");
            example.Description = @"Example of how to write a Delimited File";
            example.AutoRun = true;
            examples.Add(example);
            file = new ExampleFile("RecordClass.cs");
            file.Contents = @"     /// <summary>
/// Layout for a file delimited by |
/// </summary>
[DelimitedRecord(""|"")]
public class Orders
{
public int OrderID;

public string CustomerID;

[FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
public DateTime OrderDate;

[FieldConverter(ConverterKind.Decimal, ""."")] // The decimal separator is .
public decimal Freight;
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Example.cs");
            file.Contents = @"var engine = new FileHelperEngine<Orders>();

var orders = new List<Orders>();

orders.Add(new Orders() {
OrderID = 1,
CustomerID = ""AIRG"",
Freight = 82.43M,
OrderDate = new DateTime(2009, 05, 01)
});

orders.Add(new Orders() {
OrderID = 2,
CustomerID = ""JSYV"",
Freight = 12.22M,
OrderDate = new DateTime(2009, 05, 02)
});

engine.WriteFile(""Output.Txt"", orders);
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new ReadFixedFile(), "Read Fixed File", "QuickStart", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\10.QuickStart\30.ReadFileFixed.cs");
            example.Description = @"Example of how to read a Fixed Length layout file (eg Cobol output)";
            example.AutoRun = true;
            examples.Add(example);
            file = new ExampleFile("RecordClass.cs");
            file.Contents = @"[FixedLengthRecord()]
public class Customer
{
[FieldFixedLength(5)]
public int CustId;

[FieldFixedLength(30)]
[FieldTrim(TrimMode.Both)]
public string Name;

[FieldFixedLength(8)]
[FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
public DateTime AddedDate;
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Example.cs");
            file.Contents = @"var engine = new FixedFileEngine<Customer>();
Customer[] result = engine.ReadFile(""input.txt"");

foreach (var detail in result)
Console.WriteLine("" Client: {0},  Name: {1}"", detail.CustId, detail.Name);
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new WriteFileFixed(), "Write Fixed File", "QuickStart", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\10.QuickStart\40.WriteFileFixed.cs");
            example.Description = @"Example of how to write a Fixed Record File";
            example.AutoRun = true;
            examples.Add(example);
            file = new ExampleFile("RecordClass.cs");
            file.Contents = @"[FixedLengthRecord()]
public class Customer
{
[FieldFixedLength(5)]
public int CustId;

[FieldFixedLength(30)]
[FieldTrim(TrimMode.Both)]
public string Name;

[FieldFixedLength(8)]
[FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
public DateTime AddedDate;
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Example.cs");
            file.Contents = @"var engine = new FileHelperEngine<Customer>();

var customers = new List<Customer>();

var order1 = new Customer() {
CustId = 1,
Name = ""Antonio Moreno Taquería"",
AddedDate = new DateTime(2009, 05, 01)
};
var order2 = new Customer() {
CustId = 2,
Name = ""Berglunds snabbköp"",
AddedDate = new DateTime(2009, 05, 02)
};

customers.Add(order1);
customers.Add(order2);

engine.WriteFile(""Output.Txt"", customers);
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new ReadRecordByRecord(), "Read or Write Record by Record", "QuickStart", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\10.QuickStart\60.ReadWriteRecordByRecord.cs");
            example.Description = @"Using the FileHelperAsynEngine to work record by record";
            example.AutoRun = true;
            examples.Add(example);
            file = new ExampleFile("RecordClass.cs");
            file.Contents = @"[DelimitedRecord("","")]
public class Customer
{
public int CustId;

public string Name;

[FieldConverter(ConverterKind.Decimal, ""."")] // The decimal separator is .
public decimal Balance;

[FieldConverter(ConverterKind.Date, ""dd-MM-yyyy"")]
public DateTime AddedDate;

}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Example.cs");
            file.Contents = @"var engine = new FileHelperAsyncEngine<Customer>();

// Read
using(engine.BeginReadFile(""Input.txt""))
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
new Customer { CustId = 1732, Name = ""Juan Perez"", Balance = 435.00m,
AddedDate = new DateTime (2020, 5, 11) },
new Customer { CustId = 554, Name = ""Pedro Gomez"", Balance = 12342.30m,
AddedDate = new DateTime (2004, 2, 6) },
};

using(engine.BeginWriteFile(""TestOut.txt""))
{
foreach(Customer cust in arrayCustomers)
{
engine.WriteNext(cust);
}
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new AutopropertiesSample(), "Autoproperties", "QuickStart", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\10.QuickStart\70.Autoproperties.cs");
            example.Description = @"You can use autoproperties instead of fields";
            examples.Add(example);
            file = new ExampleFile("RecordClass.cs");
            file.Contents = @"[DelimitedRecord(""|"")]
public class Orders
{
public int OrderID { get; set; }

public string CustomerID { get; set; }

public string OrderDate { get; set; }

public string Freight { get; set; }
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Example.cs");
            file.Contents = @"var engine = new FileHelperEngine<Orders>();
var records = engine.ReadFile(""Input.txt"");

foreach (var record in records)
{
Console.WriteLine(record.CustomerID);
Console.WriteLine(record.OrderDate);
Console.WriteLine(record.Freight);
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new ReadFileMissingValue(), "Handle Missing Values with Nullable", "Missing Values", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\12.Missing Values\05.MissingValuesNullable.cs");
            example.Description = @"Using Nullable<T> for missing values";
            examples.Add(example);
            file = new ExampleFile("RecordClass.cs");
            file.Contents = @"[DelimitedRecord(""|"")]
public class Orders
{
public int OrderID;

public string CustomerID;

[FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
public DateTime? OrderDate;

public decimal? Freight;
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Example.cs");
            file.Contents = @"var engine = new FileHelperEngine<Orders>();
var records = engine.ReadFile(""Input.txt"");

foreach (var record in records) {
Console.WriteLine(record.CustomerID);
Console.WriteLine(record.OrderDate);
Console.WriteLine(record.Freight);
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new ReadFileFieldNullValue(), "Handle Missing Values With FieldNullValue", "Missing Values", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\12.Missing Values\10.MissingValuesFieldNullValue.cs");
            example.Description = @"How to read a file with some missing values and use the <b>FieldNullValue</b> attribute";
            examples.Add(example);
            file = new ExampleFile("RecordClass.cs");
            file.Contents = @"[DelimitedRecord(""|"")]
public class Orders
{
public int OrderID;

public string CustomerID;

[FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
[FieldNullValue(typeof (DateTime), ""1900-01-01"")]
public DateTime OrderDate;

public decimal Freight;
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Example.cs");
            file.Contents = @"var engine = new FileHelperEngine<Orders>();
var records = engine.ReadFile(""Input.txt"");

foreach (var record in records) {
Console.WriteLine(record.CustomerID);
if (record.OrderDate != new DateTime(1900, 01, 01))
Console.WriteLine(record.OrderDate.ToString(""dd/MM/yyyy""));
else
Console.WriteLine(""No Date"");
Console.WriteLine(record.Freight);
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new DemoFieldLength(), "FieldTrim", "Attributes", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\15.Attributes\10.FieldTrim.cs");
            example.Description = @"How to use the [FieldTrim] attribute (useful for fixed length records)";
            examples.Add(example);
            file = new ExampleFile("RecordClass.cs");
            file.Contents = @"[FixedLengthRecord()]
public class Customer
{
[FieldFixedLength(5)]
public int CustId;

[FieldFixedLength(30)]
[FieldTrim(TrimMode.Both)]
public string Name;

[FieldFixedLength(8)]
[FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
public DateTime AddedDate;
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Example.cs");
            file.Contents = @"var engine = new FixedFileEngine<Customer>();
var result = engine.ReadFile(""input.txt"");

foreach (var detail in result)
Console.WriteLine("" Client: {0},  Name: '{1}'"", detail.CustId, detail.Name);
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new DemoFieldOrder(), "FieldOrder", "Attributes", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\15.Attributes\20.FieldOrder.cs");
            example.Description = @"Force field order with [FieldOrder] attribute";
            examples.Add(example);
            file = new ExampleFile("RecordClass.cs");
            file.Contents = @"[DelimitedRecord(""|"")]
public class Orders
{
[FieldOrder(20)]
public string CustomerID;

[FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
[FieldOrder(30)]
public DateTime OrderDate;

[FieldConverter(ConverterKind.Decimal, ""."")] // The decimal separator is .
[FieldOrder(40)]
public decimal Freight;

[FieldOrder(10)]
public int OrderID;

}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Example.cs");
            file.Contents = @"var engine = new FileHelperEngine<Orders>();
var records = engine.ReadFile(""Input.txt"");

foreach (var record in records)
{
Console.WriteLine(record.CustomerID);
Console.WriteLine(record.OrderDate.ToString(""dd/MM/yyyy""));
Console.WriteLine(record.Freight);
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new FixedLengthRecordLastVariableExample(), "FixedLengthRecord FixedMode.AllowLessChars", "Attributes", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\15.Attributes\31.FixedLengthRecordLastVariable.cs");
            example.Description = @"Options when working with fixed files and not all records have same length";
            examples.Add(example);
            file = new ExampleFile("Example.cs");
            file.Contents = @"var engine = new FixedFileEngine<Customer>();
Customer[] result = engine.ReadFile(""input.txt"");

foreach (var detail in result) {
Console.WriteLine("" Client: {0},  Date: {1}"",
detail.CustId,
detail.AddedDate.ToString(""dd-MM-yyyy""));
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("RecordClass.cs");
            file.Contents = @"[FixedLengthRecord(FixedMode.AllowLessChars)]
public class Customer
{
[FieldFixedLength(5)]
public int CustId;

[FieldFixedLength(30)]
[FieldTrim(TrimMode.Both)]
public string Name;

[FieldFixedLength(8)]
[FieldConverter(ConverterKind.DateMultiFormat, ""ddMMyyyy"", ""MMyyyy"")]
public DateTime AddedDate;
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new EnumConverterExample(), "Enum Converter", "Converters", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\18.Converters\50.EnumConverter.cs");
            example.Description = @"When you have a string field in your files that can be better handled if you map it to an enum.";
            examples.Add(example);
            file = new ExampleFile("CustomerTitle.cs");
            file.Contents = @"public enum CustomerTitle
{
Owner,
SalesRepresentative,
MarketingManager
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Customers with Enum.cs");
            file.Contents = @"[DelimitedRecord(""|"")]
public class Customer
{
public string CustomerID;
public string CompanyName;
public string ContactName;
    
// Notice last feild is our enumerator
public CustomerTitle ContactTitle;
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("RunEngine.cs");
            file.Contents = @"public override void Run()
{
var engine = new DelimitedFileEngine<Customer>();

//  Read input records, enumeration automatically converted
Customer[] customers = engine.ReadFile(""Input.txt"");

foreach (var cust in customers)
Console.WriteLine(""Customer name {0} is a {1}"", cust.ContactName, cust.ContactTitle);
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new CustomConverter(), "Custom Converter", "Converters", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\18.Converters\60.CustomConverter.cs");
            example.Description = @"Explains how to extend the library with a new converter";
            examples.Add(example);
            file = new ExampleFile("RecordClass.cs");
            file.Contents = @"[FixedLengthRecord]
public class PriceRecord
{
[FieldFixedLength(6)]
public int ProductId;

[FieldFixedLength(8)]
[FieldConverter(typeof(MoneyConverter))]
public decimal PriceList;

[FieldFixedLength(8)]
[FieldConverter(typeof(MoneyConverter))]
public decimal PriceEach;
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("MoneyConverter.cs");
            file.Contents = @"public class MoneyConverter : ConverterBase
{
public override object StringToField(string from)
{
return Convert.ToDecimal(Decimal.Parse(from) / 100);
}

public override string FieldToString(object fieldValue)
{
return ((decimal)fieldValue).ToString(""#.##"").Replace(""."", """");
}

}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("RunEngine.cs");
            file.Contents = @"var engine = new FileHelperEngine<PriceRecord>();

var res = engine.ReadFile(""Input.txt"");

foreach (var product in res)
Console.WriteLine(""Product {0} price {1}"", product.ProductId, product.PriceList);
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new NotifyReadSample(), "INotifyRead Interface", "Events And Notification", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\20.Events And Notification\05.INotifyRead.cs");
            example.Description = @"Get Before/After Read events with the INotifyRead interface";
            examples.Add(example);
            file = new ExampleFile("Report layout.cs");
            file.Contents = @"      [FixedLengthRecord(FixedMode.AllowVariableLength)]
[IgnoreEmptyLines]
public class OrdersFixed
:INotifyRead
{
[FieldFixedLength(7)]
public int OrderID;

[FieldFixedLength(8)]
public string CustomerID;

[FieldFixedLength(8)]
public DateTime OrderDate;

[FieldFixedLength(11)]
public decimal Freight;


public void BeforeRead(BeforeReadEventArgs e)
{
if (e.RecordLine.StartsWith("" "") ||
e.RecordLine.StartsWith(""-""))
e.SkipThisRecord = true;
}

public void AfterRead(AfterReadEventArgs e)
{   
//  we want to drop all records with no freight
if (Freight == 0)
e.SkipThisRecord = true;

}

}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("RunEngine.cs");
            file.Contents = @"var engine = new FileHelperEngine<OrdersFixed>();
var result = engine.ReadFile(""report.inp"");

foreach (var value in result)
Console.WriteLine(""Customer: {0} Freight: {1}"", value.CustomerID, value.Freight);
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new NotifyWriteSample(), "INotifyWrite Interface", "Events And Notification", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\20.Events And Notification\10.INotifyWrite.cs");
            example.Description = @"Get Before/After Write events with the INotifyWrite interface";
            examples.Add(example);
            file = new ExampleFile("Report layout.cs");
            file.Contents = @"      [FixedLengthRecord]
[IgnoreEmptyLines]
public class OrdersFixed
:INotifyWrite
{
[FieldFixedLength(7)]
public int OrderID;

[FieldFixedLength(8)]
public string CustomerID;

[FieldFixedLength(8)]
public DateTime OrderDate;

[FieldFixedLength(11)]
public decimal Freight;

public void BeforeWrite(BeforeWriteEventArgs e)
{  
//  We only want clients with large frieght values
if (this.Freight < 40)
e.SkipThisRecord = true;
}

public void AfterWrite(AfterWriteEventArgs e)
{
//  Hide a line
if (this.CustomerID == ""HANAR"")
e.RecordLine = ""Insufficient Access"";
}
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("RunEngine.cs");
            file.Contents = @"var engine = new FileHelperEngine<OrdersFixed>();

var result = engine.ReadFile(""Input.txt"");

engine.WriteFile(""output.txt"", result);
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new ReadBeforeEventSample(), "Before/After Read Event Handling", "Events And Notification", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\20.Events And Notification\20.ReadEvents.cs");
            example.Description = @"Show how to implement read events";
            examples.Add(example);
            file = new ExampleFile("Report layout.cs");
            file.Contents = @"      [FixedLengthRecord(FixedMode.AllowVariableLength)]
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
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("RunEngine.cs");
            file.Contents = @"      public override void Run()
{
var engine = new FileHelperEngine<OrdersFixed>();
engine.BeforeReadRecord += BeforeEvent;
engine.AfterReadRecord += AfterEvent;

var result = engine.ReadFile(""report.inp"");

foreach (var value in result)
Console.WriteLine(""Customer: {0} Freight: {1}"", value.CustomerID, value.Freight);

}

private void BeforeEvent(EngineBase engine, BeforeReadEventArgs<OrdersFixed> e)
{
if (e.RecordLine.StartsWith("" "") ||
e.RecordLine.StartsWith(""-""))
e.SkipThisRecord = true;

//  Sometimes changing the record line can be useful, for example to correct for
//  a bad data layout.  Here is an example of this, commented out for this example

//if (e.RecordLine.StartsWith("" ""))
//   e.RecordLine = ""Be careful!"";
}


private void AfterEvent(EngineBase engine, AfterReadEventArgs<OrdersFixed> e)
{
//  we want to drop all records with no freight
if (e.Record.Freight == 0)
e.SkipThisRecord = true;
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("RunEngineLambda.cs");
            file.Contents = @"public void RunLambda()
{
var engine = new FileHelperEngine<OrdersFixed>();
engine.BeforeReadRecord += (eng, e) => {
if (e.RecordLine.StartsWith ("" "") ||
e.RecordLine.StartsWith (""-""))
e.SkipThisRecord = true;
};
engine.AfterReadRecord +=  (eng, e) => {
if (e.Record.Freight == 0)
e.SkipThisRecord = true;
};

var result = engine.ReadFile(""report.inp"");

foreach (var value in result)
Console.WriteLine(""Customer: {0} Freight: {1}"", value.CustomerID, value.Freight);

}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new WriteEvents(), "Before/After Write Event Handling", "Events And Notification", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\20.Events And Notification\30.WriteEvents.cs");
            example.Description = @"Show how to implement write events";
            examples.Add(example);
            file = new ExampleFile("Report layout.cs");
            file.Contents = @"      [FixedLengthRecord]
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
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("RunEngine.cs");
            file.Contents = @"      public override void Run()
{
var engine = new FileHelperEngine<OrdersFixed>();

var result = engine.ReadFile(""Input.txt"");

//  add our filter logic.
engine.BeforeWriteRecord += BeforeWriteEvent;
engine.AfterWriteRecord += AfterWriteEvent;

engine.WriteFile(""output.txt"", result);
}

private void BeforeWriteEvent(EngineBase engine, BeforeWriteEventArgs<OrdersFixed> e)
{
//  We only want clients with large frieght values
if (e.Record.Freight < 40)
e.SkipThisRecord = true;
}

private void AfterWriteEvent(EngineBase engine, AfterWriteEventArgs<OrdersFixed> e)
{
//  Hide a line
if (e.Record.CustomerID == ""HANAR"")
e.RecordLine = ""Insufficient Access"";
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("RunEngineLambda.cs");
            file.Contents = @"public void RunLambda()
{
var engine = new FileHelperEngine<OrdersFixed>();

var result = engine.ReadFile(""Input.txt"");

//  add our filter logic.
engine.BeforeWriteRecord += (eng, e) => {
if (e.Record.Freight < 40)
e.SkipThisRecord = true;
};
engine.AfterWriteRecord += (eng, e) => {
if (e.Record.CustomerID == ""HANAR"")
e.RecordLine = ""Insufficient Access"";
};

engine.WriteFile(""output.txt"", result);
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new ErrorModeThrowException(), "ErrorMode.ThrowException", "ErrorHandling", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\25.ErrorHandling\10.ErrorMode.ThrowException.cs");
            example.Description = @"Default Behavior. Read the file or reject the whole file";
            examples.Add(example);
            file = new ExampleFile("Customers with Enum.cs");
            file.Contents = @"[DelimitedRecord(""|"")]
public class Customer
{
public string CustomerID;
public string CompanyName;
public string ContactName;
public CustomerTitle ContactTitle;
}

public enum CustomerTitle
{
Owner,
SalesRepresentative,
MarketingManager
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Example.cs");
            file.Contents = @"try
{
var engine = new DelimitedFileEngine<Customer>();
    
//  This fails with not in enumeration error
var customers = engine.ReadFile(""Input.txt"");
    
}
catch (Exception ex)
{
Console.WriteLine(ex.ToString()); // with stack trace
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new ErrorModeIgnoreAndContinue(), "ErrorMode.IgnoreAndContinue", "ErrorHandling", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\25.ErrorHandling\30.ErrorMode.IgnoreAndContinue.cs");
            example.Description = @"Read the file dropping bad records";
            examples.Add(example);
            file = new ExampleFile("Customers with Enum.cs");
            file.Contents = @"[DelimitedRecord(""|"")]
public class Customer
{
public string CustomerID;
public string CompanyName;
public string ContactName;
public CustomerTitle ContactTitle;
}

public enum CustomerTitle
{
Owner,
SalesRepresentative,
MarketingManager
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("RunEngine.cs");
            file.Contents = @"var engine = new DelimitedFileEngine<Customer>();

// Switch error mode on
engine.ErrorManager.ErrorMode = ErrorMode.IgnoreAndContinue;

//  This fails with not in enumeration error
var customers = engine.ReadFile(""Input.txt"");

// This wont display anything, we have dropped it
foreach (var err in engine.ErrorManager.Errors) {
Console.WriteLine();
Console.WriteLine(""Error on Line number: {0}"", err.LineNumber);
Console.WriteLine(""Record causing the problem: {0}"", err.RecordString);
Console.WriteLine(""Complete exception information: {0}"", err.ExceptionInfo.ToString());
}

// This will display only 3 of the four records
foreach (var cust in customers)
Console.WriteLine(""Customer name {0} is a {1}"", cust.ContactName, cust.ContactTitle);
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new ErrorModeSaveAndContinue(), "ErrorMode SaveAndContinue", "ErrorHandling", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\25.ErrorHandling\40.ErrorMode.SaveAndContinue.cs");
            example.Description = @"Read the file saving bad records";
            examples.Add(example);
            file = new ExampleFile("Input.txt");
            file.Contents = @"ALFKI|Alfreds Futterkiste|Maria Anders|SalesRepresentative
ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|NotInEnum
FRANR|France restauration|Carine Schmitt|MarketingManager
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner
";
            file.Status = ExampleFile.FileType.InputFile;
            example.Files.Add(file);
            file = new ExampleFile("Customers with Enum.cs");
            file.Contents = @"[DelimitedRecord(""|"")]
public class Customer
{
public string CustomerID;
public string CompanyName;
public string ContactName;
public CustomerTitle ContactTitle;
}

public enum CustomerTitle
{
Owner,
SalesRepresentative,
MarketingManager
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("RunEngine.cs");
            file.Contents = @"public override void Run()
{
var engine = new DelimitedFileEngine<Customer>();

// Switch error mode on
engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

//  This fails with not in enumeration error
var customers = engine.ReadFile(""Input.txt"");

if (engine.ErrorManager.HasErrors)
engine.ErrorManager.SaveErrors(""errors.out"");

LoadErrors();
}

private void LoadErrors()
{
// sometime later you can read it back using:
ErrorInfo[] errors = ErrorManager.LoadErrors(""errors.out"");

// This will display error from line 2 of the file.
foreach (var err in errors) {
Console.WriteLine();
Console.WriteLine(""Error on Line number: {0}"", err.LineNumber);
Console.WriteLine(""Record causing the problem: {0}"", err.RecordString);
Console.WriteLine(""Complete exception information: {0}"", err.ExceptionInfo.ToString());
}
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new EngineOptions(), "Dynamic Engine Options", "Advanced", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\50.Advanced\05.DynamicChangeOptions.cs");
            example.Description = @"Change the options of the engines at run time";
            examples.Add(example);
            file = new ExampleFile("CustomersVerticalBar.cs");
            file.Contents = @"[DelimitedRecord(""|"")]
public class CustomersVerticalBar
{
public string CustomerID;
    
// Will be excluded at run time
public string DummyField;

public string CompanyName;
public string ContactName;
public string ContactTitle;
public string Address;
public string City;
public string Country;
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Example.txt");
            file.Contents = @"var engine = new DelimitedFileEngine<CustomersVerticalBar>();

engine.Options.Fields[2].TrimMode = TrimMode.Both;
engine.Options.RemoveField(""DummyField"");

// City is optional
engine.Options.Fields[engine.Options.Fields.Count - 1].IsOptional = true;

engine.ReadFile(""Input.txt"");
";
            // unknown extension .txt
            example.Files.Add(file);

            example = new ExampleCode(new MultipleDelimiters(), "Multiple Delimiters", "Advanced", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\50.Advanced\10.MultipleDelimiters.cs");
            example.Description = @"Write a file with different delimiters using the same record";
            examples.Add(example);
            file = new ExampleFile("CustomersVerticalBar.cs");
            file.Contents = @"/// <summary> Sample class that is delimited by | default </summary>
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

}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("RunEngine.cs");
            file.Contents = @"var customers = CreateCustomers();

var engine = new DelimitedFileEngine<CustomersVerticalBar>(Encoding.UTF8);
//  write out customers using a vertical bar delimiter (default)
engine.WriteFile(""Out_Vertical.txt"", customers);

// Change the delimiter to semicolon and write that out
engine.Options.Delimiter = "";"";
engine.WriteFile(""Out_SemiColon.txt"", customers);

// Change the delimiter to a tab and write that out
engine.Options.Delimiter = ""\t"";
engine.WriteFile(""Out_Tab.txt"", customers);
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new MultiRecordSample(), "Multi Record Processing", "Advanced", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\50.Advanced\30.MultiRecordSample.cs");
            example.Description = @"Read or write a file with many different layouts";
            examples.Add(example);
            file = new ExampleFile("Input.txt");
            file.Contents = @"        10248|VINET|5|04071996|01081996|16071996|3|32.38  
10249|TOMSP|6|05071996|16081996|10071996|1|11.61
ALFKI;Alfreds Futterkiste;Maria Anders;Sales Representative;Obere Str. 57;Berlin;Germany
ANATR;Ana Trujillo Emparedados y helados;Ana Trujillo;Owner;Avda. de la Constitución 2222;México D.F.;Mexico
10250|HANAR|4|08071996|05081996|12071996|2|65.83
10111314012345
11101314123456
10251|VICTE|3|08071996|05081996|15071996|1|41.34
11121314901234
10101314234567
ANTON;Antonio Moreno Taquería;Antonio Moreno;Owner;Mataderos  2312;México D.F.;Mexico
BERGS;Berglunds snabbköp;Christina Berglund;Order Administrator;Berguvsvägen  8;Luleå;Sweden

";
            file.Status = ExampleFile.FileType.InputFile;
            example.Files.Add(file);
            file = new ExampleFile("Customer.cs");
            file.Contents = @"[DelimitedRecord("";"")]
public class Customer
{
public string CustomerID;
public string CompanyName;
public string ContactName;
public string ContactTitle;
public string Address;
public string City;
public string Country;

public override string ToString()
{
return ""Customer: "" + CustomerID + "" - "" + CompanyName + "", "" + ContactName;
}
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("SampleType.cs");
            file.Contents = @"[FixedLengthRecord]
public class SampleType
{
[FieldFixedLength(8)]
[FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
public DateTime Field1;

[FieldFixedLength(3)]
[FieldAlign(AlignMode.Left, ' ')]
[FieldTrim(TrimMode.Both)]
public string Field2;

[FieldFixedLength(3)]
[FieldAlign(AlignMode.Right, '0')]
[FieldTrim(TrimMode.Both)]
public int Field3;

public override string ToString()
{
return ""SampleType: "" + Field2 + "" - "" + Field3;
}
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Orders.cs");
            file.Contents = @"[DelimitedRecord(""|"")]
public class Orders
{
public int OrderID;

public string CustomerID;

public int EmployeeID;

public DateTime OrderDate;

public DateTime RequiredDate;

[FieldNullValue(typeof(DateTime), ""2005-1-1"")]
public DateTime ShippedDate;

public int ShipVia;

public decimal Freight;

public override string ToString()
{
return ""Orders: "" + OrderID + "" - "" + CustomerID + "" - "" + Freight;
}
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("RunEngine.cs");
            file.Contents = @"var engine = new MultiRecordEngine(typeof (Orders),
typeof (Customer),
typeof (SampleType));

engine.RecordSelector = new RecordTypeSelector(CustomSelector);

var res = engine.ReadFile(""Input.txt"");

foreach (var rec in res)
Console.WriteLine(rec.ToString());
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Selector.cs");
            file.Contents = @"private Type CustomSelector(MultiRecordEngine engine, string recordLine)
{
if (recordLine.Length == 0)
return null;

if (Char.IsLetter(recordLine[0]))
return typeof (Customer);
else if (recordLine.Length == 14)
return typeof (SampleType);
else
return typeof (Orders);
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new AutoFormatDetectorExample(), "Smart Format Detector", "Advanced", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\50.Advanced\50.SmartFormatDetector.cs");
            example.Description = @"Detect the format from a flat file";
            examples.Add(example);
            file = new ExampleFile("Example.cs");
            file.Contents = @"var detector = new FileHelpers.Detection.SmartFormatDetector();
var formats = detector.DetectFileFormat(""input.txt"");

foreach (var format in formats)
{
Console.WriteLine(""Format Detected, confidence:"" + format.Confidence + ""%"");
var delimited = format.ClassBuilderAsDelimited;

Console.WriteLine(""    Delimiter:"" + delimited.Delimiter);
Console.WriteLine(""    Fields:"");

foreach (var field in delimited.Fields)
{
Console.WriteLine(""        "" + field.FieldName + "": "" + field.FieldType);    
}
    
    
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new BigFileSort(), "Sort Big File with Record Class", "Sorting", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\60.Sorting\10.SortBigFiles.cs");
            example.Description = @"Shows how to sort a big file using a record class";
            example.Runnable = false;
            examples.Add(example);
            file = new ExampleFile("SortingWithRecord.cs");
            file.Contents = @"public override void Run()
{
// Implements http://en.wikipedia.org/wiki/External_sorting
// OrdersTab is IComparable<OrdersTab>

var sorter = new BigFileSorter<OrdersTab>(10*1024*1024); // 10 Mb
sorter.DeleteTempFiles = true;
sorter.Sort(""unsorted.txt"", ""sorted.txt"");
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("OrdersTab.cs");
            file.Contents = @"[DelimitedRecord(""\t"")]
public class OrdersTab
: IComparable<OrdersTab>
{
public int OrderID;

public string CustomerID;

public int EmployeeID;

public DateTime OrderDate;

[FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
public DateTime RequiredDate;

[FieldNullValue(typeof (DateTime), ""2005-1-1"")]
public DateTime ShippedDate;

public int ShipVia;

public decimal Freight;

#region IComparable<OrdersTab> Members

public int CompareTo(OrdersTab other)
{
return this.OrderID.CompareTo(other.OrderID);
}

#endregion
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new BigFileSortString1(), "Sort Big File without Record Class 1", "Sorting", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\60.Sorting\20.SortBigFilesString1.cs");
            example.Description = @"Shows how to sort a big file without a record class";
            example.Runnable = false;
            examples.Add(example);
            file = new ExampleFile("SortingWithoutRecord.cs");
            file.Contents = @"     public override void Run()
{
// Implements http://en.wikipedia.org/wiki/External_sorting
// Uses the comparison in the construct

// Sort comparing the raw lines
var sorter = new BigFileSorter((x, y) => x.CompareTo(y));
sorter.DeleteTempFiles = true;
sorter.Sort(""unsorted.txt"", ""sorted.txt"");
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new BigFileSortString2(), "Sort Big File without Record Class 2", "Sorting", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\60.Sorting\30.SortBigFilesString2.cs");
            example.Description = @"Shows how to sort a big file without a record class";
            example.Runnable = false;
            examples.Add(example);
            file = new ExampleFile("SortingWithoutRecord.cs");
            file.Contents = @"public override void Run()
{
// Implements http://en.wikipedia.org/wiki/External_sorting

var sorter = new BigFileSorter(
(x, y) => {
// You can add here any custom function
return x.Length.CompareTo(y.Length);
});
sorter.DeleteTempFiles = true;
sorter.Sort(""unsorted.txt"", ""sorted.txt"");
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new SimpleMasterDetailSample(), "Simple Master Detail", "MasterDetail", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\90.MasterDetail\10.SimpleMasterDetail.cs");
            example.Description = @"Show how to implement Master detail reading using a selection subroutine";
            examples.Add(example);
            file = new ExampleFile("RunEngine.cs");
            file.Contents = @"/// <summary>
/// Run a record through engine using a selector to create a master detail input
/// </summary>
public override void Run()
{
var engine = new MasterDetailEngine<Customers, Orders>(new MasterDetailSelector(ExampleSelector));

var result = engine.ReadFile(""Input.txt"");

foreach (var group in result) {
Console.WriteLine(""Customer: {0}"", group.Master.CustomerID);
foreach (var detail in group.Details)
Console.WriteLine(""    Freight: {0}"", detail.Freight);
}
}

/// <summary>
/// Selector to determine whether we have a master or
/// detail record to import
/// </summary>
/// <param name=""record"">Alpha characters coming in</param>
/// <returns>Selector for master or detail record</returns>
private RecordAction ExampleSelector(string record)
{
if (record.Length < 2)
return RecordAction.Skip;

if (Char.IsLetter(record[0]))
return RecordAction.Master;
else
return RecordAction.Detail;
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Input.txt");
            file.Contents = @"ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
10248|ALFKI|5|04071996|01081996|16071996|3|32.38
10249|ALFKI|6|05071996|16081996|10071996|1|11.61
10251|ALFKI|3|08071996|05081996|15071996|1|41.34
ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|Owner|Avda. de la Constitución 2222|México D.F.|Mexico
10252|ANATR|4|09071996|06081996|11071996|2|51.3
ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico

";
            file.Status = ExampleFile.FileType.InputFile;
            example.Files.Add(file);
            file = new ExampleFile("output.txt");
            file.Contents = @"";
            file.Status = ExampleFile.FileType.OutputFile;
            example.Files.Add(file);
            file = new ExampleFile("Master layout.cs");
            file.Contents = @"/// <summary>
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
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Detail layout.cs");
            file.Contents = @"/// <summary>
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
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);

            example = new ExampleCode(new SecondMasterDetailSample(), "Master Detail sample", "MasterDetail", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\90.MasterDetail\20.MasterDetailSample2.cs");
            example.Description = @"Show how to implement Master detail reading where record contains characters";
            examples.Add(example);
            file = new ExampleFile("RunEngine.cs");
            file.Contents = @"/// <summary>
/// Run a record through engine using a Common selector where master contains a characrter to create a master detail input
/// </summary>
public override void Run()
{
var engine = new MasterDetailEngine<Customers, Orders>
(CommonSelector.MasterIfContains, ""@"");
// to Read use:
var res = engine.ReadFile(""Input.txt"");

// to Write use:
engine.WriteFile(""Output.txt"", res);
}
";
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Input.txt");
            file.Contents = @"@ALFKI|Alfreds Futterkiste|Maria Anders|Sales Representative|Obere Str. 57|Berlin|Germany
10248|ALFKI|5|04071996|01081996|16071996|3|32.38
10249|ALFKI|6|05071996|16081996|10071996|1|11.61
10251|ALFKI|3|08071996|05081996|15071996|1|41.34
@ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|Owner|Avda. de la Constitución 2222|México D.F.|Mexico
10252|ANATR|4|09071996|06081996|11071996|2|51.3
@ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner|Mataderos  2312|México D.F.|Mexico

";
            file.Status = ExampleFile.FileType.InputFile;
            example.Files.Add(file);
            file = new ExampleFile("Output.txt");
            file.Contents = @"";
            file.Status = ExampleFile.FileType.OutputFile;
            example.Files.Add(file);
            file = new ExampleFile("Master layout.cs");
            file.Contents = @"/// <summary>
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
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            file = new ExampleFile("Detail layout.cs");
            file.Contents = @"/// <summary>
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
            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            example = new ExampleCode(new ExcelCreateAndSave(), "Create excel storage and save it", "Excel", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\13.Excel\10.ExcelCreateAndSave.cs");
            //example = new ExampleCode(new ExcelCreateAndSave(), "Create excel storage and save it.", "Excel", @"C:\Users\marko\git\open-source\file-helper\FileHelpers\FileHelpers.Examples\Examples\13.Excel\10.ExcelCreateAndSave.cs");
            example.Description = @"Shows how to create excel storage, fill it with object data and save";
            example.Runnable = true;
            examples.Add(example);
            file = new ExampleFile("ExcelExample.cs");
            file.Language = NetLanguage.CSharp;
            file.Contents = @"
public override void Run()
{
    // Create an excel storage for specific class
    // By default start row/column is 2/B (index 1)
    ExcelNPOIStorage storage = new ExcelNPOIStorage(typeof(Student));

    // Set storage file name -> that will be excel output file name
    // Extension must be .xlsx or .xls
    storage.FileName = ""Students.xlsx"";

    // Sheet name is not required. By default sheet name will be ""Sheet0""
    storage.SheetName = ""Students"";
    storage.ColumnsHeaders.Add(""Student number"");
    storage.ColumnsHeaders.Add(""Student name"");
    storage.ColumnsHeaders.Add(""Course name"");

    // Test data
    int count = 3;
    Student[] students = new Student[count];

    students[0] = CreateStudent(0, ""Chuck Norris"", ""Karate"");
    students[1] = CreateStudent(0, ""Steven Seagal"", ""Aikido"");
    students[2] = CreateStudent(0, ""Dennis Ritchie"", ""Programming"");

    // Insert students to excel storage
    // This method will save out excel file
    storage.InsertRecords(students);
}
";
            example.Files.Add(file);
            file = new ExampleFile("ExcelTest.xlsx");
            file.Contents = @"";
            file.Status = ExampleFile.FileType.OutputFile;
            example.Files.Add(file);

            file = new ExampleFile("Student.cs");
            file.Language = NetLanguage.CSharp;
            file.Contents = @"
[DelimitedRecord("""")]
public class Student
{
    public int StudentNumber { get; set; }

    public string FullName { get; set; }

    public string Course { get; set; }
}
";
            example.Files.Add(file);
            file = new ExampleFile("CreateStudent.cs");
            file.Language = NetLanguage.CSharp;
            file.Contents = @"
/// <summary>
/// Create new student
/// </summary>
/// <returns>Student object</returns>
public static Student CreateStudent(int studentNumber, string fullName, string course)
    => new Student()
    {
        StudentNumber = studentNumber,
        FullName = fullName,
        Course = course
    };"
;


            file.Language = NetLanguage.CSharp;
            example.Files.Add(file);
            example = new ExampleCode(new ExportAndEdit(), "Open excel file, edit and save it", "Excel", @"d:\Desarrollo\Devoo\GitHub\FileHelpers\FileHelpers.Examples\Examples\13.Excel\20.ExportAndEdit.cs");
            //example = new ExampleCode(new ExcelCreateAndSave(), "Create excel storage and save it.", "Excel", @"C:\Users\marko\git\open-source\file-helper\FileHelpers\FileHelpers.Examples\Examples\13.Excel\10.ExcelCreateAndSave.cs");
            example.Description = @"Shows how to load excel file to storage, edit it and save again";
            example.Runnable = true;
            examples.Add(example);
            file = new ExampleFile("ExcelExample.cs");
            file.Language = NetLanguage.CSharp;
            file.Contents = @"
public override void Run()
{
    // Create an excel storage for specific class
    // startRow = 2 & startColumn = 1 -> for skipping column header names
    ExcelNPOIStorage storage = new ExcelNPOIStorage(typeof(Student), 2, 1);

    // Set storage file name -> represents the excel file name we want to read
    storage.FileName = ""Students.xlsx"";

    // Read from excel file
    Student[] students = storage.ExtractRecords() as Student[];

    Console.WriteLine(""\t\tStudents from file:\n"");
    foreach (Student s in students)
    {
        Console.WriteLine(s);
    }

    students[0].StudentNumber = 420;
    Console.WriteLine(""\nStudent {0} edited."", students[0].FullName);
    students[1].Course = ""Jiu-Jitsu"";
    Console.WriteLine(""\nStudent {0} edited."", students[0].FullName);

    Console.WriteLine(""\t\tEdited students:\n"");
    foreach (Student s in students)
    {
        Console.WriteLine(s);
    }

    // Insert students to excel storage
    // This method will save out excel file
    storage.InsertRecords(students);
    Console.WriteLine(""Changes saved."");
}
";
            example.Files.Add(file);
            file = new ExampleFile("Student.cs");
            file.Language = NetLanguage.CSharp;
            file.Contents = @"
[DelimitedRecord("""")]
public class Student
{
    public int StudentNumber { get; set; }

    public string FullName { get; set; }

    public string Course { get; set; }
        
    public override string ToString()
    {
        return $""{ StudentNumber}: { FullName} is on course: { Course }"";
    }
}
";
            example.Files.Add(file);
            return examples;
        }
    }
}


