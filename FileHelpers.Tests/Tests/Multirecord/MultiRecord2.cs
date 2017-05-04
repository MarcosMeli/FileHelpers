using System;
using NFluent;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    [TestFixture]
    public class MultiRecords2
    {

        [Test]
        public void MultpleRecordsAdvanced()
        {
            var myParserEngine = new MultiRecordEngine(typeof(RawCarrier), typeof(RawOrigin), typeof(RawDestination), typeof(RawShipment), typeof(RawShipmentDetail))
            { RecordSelector = CustomRecordSelector };
            var testString = @"Carrier|123123|117233|DA|||
Origin|61|8120|Manufacturing A |Field Road||DALLAS|TX|9|US
Destination|LO|0222|||ADINC| TURNPIKE||NT|NY|11003|US|56
Shipment|M00||       9.00|Inches|       4.00|Inches|       7.00|Inches|2.000|lb|08/01/2010|15:43|U20|U0||90|1|          2|0001|||
ShipmentDetail|LM000|805289078029|ORIGINAL|FR||NR|              2.000|0067|2001";

            var result = myParserEngine.ReadString(testString);

            Check.That(result.Length).IsEqualTo(5);

        }

        private static Type CustomRecordSelector(MultiRecordEngine engine, string line)
        {
            line = line.ToLower();
            if (line.StartsWith("carrier"))
            {
                return typeof(RawCarrier);
            }
            if (line.StartsWith("origin"))
            {
                return typeof(RawOrigin);
            }
            if (line.StartsWith("destination"))
            {
                return typeof(RawDestination);
            }
            if (line.StartsWith("shipment|"))
            {
                return typeof(RawShipment);
            }
            if (line.StartsWith("shipmentdetail|"))
            {
                return typeof(RawShipmentDetail);
            }
            return null;
        }

        public enum DtoRecordType
        {
            Undefined,
            Carrier,
            Origin,
            Destination,
            Shipment,
            ShipmentDetail
        };

        [DelimitedRecord("|")]
        public class RawCarrier
        {
            public DtoRecordType DtoRecordType;
            //public string RecordTypeString;
            public string Field1;
            public string Field2;
            public string Field3;
            [FieldOptional]
            public string Field4;
            [FieldOptional]
            public string Field5;
            [FieldOptional]
            public string Field6;
            [FieldOptional]
            public string Field7;
        }
        [DelimitedRecord("|")]
        public class RawOrigin
        {
            public DtoRecordType DtoRecordType;
            //public string RecordTypeString;
            public string Field1;
            public string Field2;
            public string Field3;
            public string Field4;
            public string Field5;
            public string Field6;
            public string Field7;
            public string Field8;
            public string Field9;
            [FieldOptional]
            public string Field10;
        }
        [DelimitedRecord("|")]
        public class RawDestination
        {
            public DtoRecordType DtoRecordType;
            //public string RecordTypeString;
            public string Field1;
            public string Field2;
            public string Field3;
            public string Field4;
            public string Field5;
            public string Field6;
            public string Field7;
            public string Field8;
            public string Field9;
            public string Field10;

            [FieldOptional]
            public string Field11;
            [FieldOptional]
            public string Field12;
            [FieldOptional]
            public string Field13;
        }

        [DelimitedRecord("|")]
        public class RawShipment
        {
            public DtoRecordType DtoRecordType;
            //public string RecordTypeString;
            public string Field1;
            public string Field2;
            public string Field3;
            public string Field4;
            public string Field5;
            public string Field6;
            public string Field7;
            public string Field8;
            public string Field9;
            public string Field10;
            public string Field11;
            public string Field12;
            public string Field13;
            public string Field14;
            public string Field15;
            public string Field16;
            public int Field17;
            public int Field18;
            [FieldOptional]
            public string Field19;
            [FieldOptional]
            public string Field20;
            [FieldOptional]
            public string Field21;
            [FieldOptional]
            public string Field22;
            [FieldOptional]
            public string Field23;
        }
        [DelimitedRecord("|")]
        public class RawShipmentDetail
        {
            public DtoRecordType DtoRecordType;
            //public string RecordTypeString;
            public string Field1;
            public string Field2;
            public string Field3;
            public string Field4;
            public string Field5;
            public string Field6;
            public string Field7;
            public string Field8;
            public int Field9;
            [FieldOptional]
            public string Field10;
        }
    }
}