using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers.SamplesDashboard
{
    public class DemoFactory
    {
        public static IEnumerable<DemoCode> GetDemos()
        {
            var demos = new List<DemoCode>();
            DemoCode demo;

            demo = new DemoCode("Read Delimited File", "");
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
            }";
            demo.Files.Add(new DemoFile("OrdersRecord.cs"));
            demo.LastFile.Contents = @"[DelimitedRecord(""|"")]
        public class Orders
        {
            public int OrderID;

            public string CustomerID;
            [FieldConverter(ConverterKind.Date, ""ddMMyyyy"")]
            public DateTime OrderDate;

            public decimal Freight;
        }";



            return null;
        }
    }
}
