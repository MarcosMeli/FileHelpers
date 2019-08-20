using System;
using FileHelpers;

namespace ExamplesFx
{
    public class CustomConverter : ExampleBase
    {
        //-> Name: Custom Converter
        //-> Description: Explains how to extend the library with a new converter

        //-> For fixed length records where you need to read decimal data and you don't have an explicit decimal dot (because this isn't required or a Cobol layout). You read it with a customer converter.
        //-> If you want to parse a file with a field that has different parsing rules you can define a CustomConverter.For example, if you have the following source file:

        //-> FileIn:Input.txt
        /*0001250001458800013025
0001260012345600234567
0001270123456702345678
0001280087654300123456
*/
        //-> /File
        
            // -> This is a fixed length record file with 6 characters for the ProductId and 8 places for PriceList and PriceEach. The record class would look as follows:

        //-> File:RecordClass.cs
        [FixedLengthRecord]
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

        //-> /File

        //->The last step is to define the converter, you must inherit from ConverterBase:
        //-> File:MoneyConverter.cs
        public class MoneyConverter : ConverterBase
        {
            public override object StringToField(string from)
            {
                return Convert.ToDecimal(decimal.Parse(from) / 100);
            }

            public override string FieldToString(object fieldValue)
            {
                return ((decimal)fieldValue).ToString("#.##").Replace(".", "");
            }

        }

        //-> /File


        //-> Done !! You actually parse the file with:

        public override void Run()
        {
            //-> File:RunEngine.cs
            var engine = new FileHelperEngine<PriceRecord>();

            var res = engine.ReadFile("Input.txt");

            foreach (var product in res)
                Console.WriteLine("Product {0} price {1}", product.ProductId, product.PriceList);
            //-> /File
        }


    }
}