using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace ExamplesFx
{
    //-> Name: FieldTrim
    //-> Description: Example of how to use [FieldTrim] attribute

    public class DemoFieldLength
        : ExampleBase
    {

        //-> FileIn:Input.txt
        /*01010 Alfreds Futterkiste          13122005
        12399 Ana Trujillo Emparedados y   23012000
        00011 Antonio Moreno Taquería      21042001
        51677 Around the Horn              13051998
        99999 Berglunds snabbköp           02111999*/
        //-> /File


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
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime AddedDate;
        }

        //-> /File


        public override void Run()
        {
            //-> File:Example.cs

            var engine = new FixedFileEngine<Customer>();
            var result = engine.ReadFile("input.txt");

            foreach (var detail in result)
                this.Console.WriteLine(" Client: {0},  Name: '{1}'", detail.CustId, detail.Name);
            //-> /File

        }

    }
}