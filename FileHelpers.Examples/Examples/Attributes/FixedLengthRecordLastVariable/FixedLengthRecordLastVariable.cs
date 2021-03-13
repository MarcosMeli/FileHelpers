using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Examples.Attributes.FixedLengthRecordLastVariable
{
    //-> Name: FixedLengthRecord FixedMode.AllowLessChars
    //-> Description: Options when working with fixed files and not all records have same length

    public class FixedLengthRecordLastVariableExample
        : ExampleBase
    {
        public override void Run()
        {
            //-> File:Example.cs
            var engine = new FixedFileEngine<Customer>();
            Customer[] result = engine.ReadFile("input.txt");

            foreach (var detail in result) {
                Console.WriteLine(" Client: {0},  Date: {1}",
                    detail.CustId,
                    detail.AddedDate.ToString("dd-MM-yyyy"));
            }

            //-> /File
        }

        //-> File:RecordClass.cs
        [FixedLengthRecord(FixedMode.AllowLessChars)]
        public class Customer
        {
            [FieldFixedLength(5)]
            public int CustId;

            [FieldFixedLength(30)]
            [FieldTrim(TrimMode.Both)]
            public string Name;

            [FieldFixedLength(8)]
            [FieldConverter(ConverterKind.DateMultiFormat, "ddMMyyyy", "MMyyyy")]
            public DateTime AddedDate;
        }

        //-> /File

        //-> FileIn:Input.txt
/*01010 Alfreds Futterkiste          13122005
12399 Ana Trujillo Emparedados y   23012000
00011 Antonio Moreno Taquería      042001
51677 Around the Horn              13051998
99999 Berglunds snabbköp           111999*/
        //-> /File
    }
}