using System;
using FileHelpers;
using FileHelpers.Converters;


namespace ExamplesFx
{
    //-> Name:Read Fixed File
    //-> Description:Example of how to read a Fixed Length layout file (eg COBOL output)
    //-> AutoRun:true

    public class ReadFixedFile
        : ExampleBase
    {
        //-> Let's start with a simple file:

        //-> FileIn:Input.txt
        /*01010 Alfreds Futterkiste          13122005
12399 Ana Trujillo Emparedados y   23012000
00011 Antonio Moreno Taquería      21042001
51677 Around the Horn              13051998
99999 Berglunds snabbköp           02111999*/
        //-> /FileIn

        //-> We define the record layout:
        //-> File:RecordClass.cs
        [FixedLengthRecord()]
        public class Customer
        {
            [FieldFixedLength(5)]
            public int CustId;

            [FieldFixedLength(30)]
            [FieldTrim(TrimMode.Both)]
            public string Name;

            [FieldFixedLength(8)]
            [DateTimeConverter("ddMMyyyy")]
            public DateTime AddedDate;
        }

        //-> /File

        //-> Read the values and write them to the Console:
        public override void Run()
        {
            //-> File:Example.cs
            var engine = new FixedFileEngine<Customer>();
            Customer[] result = engine.ReadFile("input.txt");

            foreach (var detail in result)
                Console.WriteLine(" Client: {0},  Name: {1}", detail.CustId, detail.Name);

            //-> /File
        }

        //-> Console
    }
}
