using System;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class NullWriters
	{
		FileHelperEngine engine;
		FileHelperAsyncEngine asyncEngine;

		[Test]
		public void WriteNull()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res = new SampleType[3];
			res[0] = new SampleType();
			res[1] = new SampleType();
			res[2] = new SampleType();

			string tempo = engine.WriteString(res);
			res = (SampleType[]) engine.ReadString(tempo);

			Assert.AreEqual(3, res.Length);
			Assert.AreEqual(3, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(DateTime.MinValue, res[0].Field1);
			Assert.AreEqual("", res[0].Field2);
			Assert.AreEqual(0, res[0].Field3);

		}

		[Test]
		public void WriteNullAsync()
		{
			asyncEngine = new FileHelperAsyncEngine(typeof (SampleType));

			asyncEngine.BeginWriteFile("tempNull.txt");

			asyncEngine.WriteNext(new SampleType());
			asyncEngine.WriteNext(new SampleType());
			asyncEngine.WriteNext(new SampleType());

			asyncEngine.Close();

			asyncEngine.BeginReadFile("tempNull.txt");
			SampleType[] res = (SampleType[]) asyncEngine.ReadNexts(5000);
			asyncEngine.Close();

			Assert.AreEqual(3, res.Length);
			Assert.AreEqual(3, asyncEngine.TotalRecords);
			Assert.AreEqual(0, asyncEngine.ErrorManager.ErrorCount);

			Assert.AreEqual(DateTime.MinValue, res[0].Field1);
			Assert.AreEqual("", res[0].Field2);
			Assert.AreEqual(0, res[0].Field3);

			if (File.Exists("tempNull.txt")) File.Delete("tempNull.txt");
		}


        [Test]
        public void ReadNullableTypes()
        {
            engine = new FileHelperEngine(typeof(NullableType));

            NullableType[] res;
            res = (NullableType[])TestCommon.ReadTest(engine, "Good", "NullableTypes1.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
            Assert.AreEqual("901", res[0].Field2);
            Assert.AreEqual(234, res[0].Field3);

            Assert.AreEqual(null, res[1].Field1);
            Assert.AreEqual("012", res[1].Field2);
            Assert.AreEqual(345, res[1].Field3);


            Assert.AreEqual(null, res[2].Field3);

        }


        [Test]
        public void WriteNullableTypes1()
        {
            engine = new FileHelperEngine(typeof(NullableType));

            System.Collections.Generic.List<NullableType> toWrite = new System.Collections.Generic.List<NullableType>();

            NullableType record;

            record = new NullableType();
            record.Field1 = new DateTime(1314, 12, 11);
            record.Field2 = "901";
            record.Field3 = 234;
            toWrite.Add(record);

            record = new NullableType();
            record.Field1 = null;
            record.Field2 = "012";
            record.Field3 = null;
            toWrite.Add(record);

            record = new NullableType();
            record.Field1 = new DateTime(1316, 5, 6);
            record.Field2 = "111";
            record.Field3 = 4;
            toWrite.Add(record);

            NullableType[] res = (NullableType[]) engine.ReadString(engine.WriteString(toWrite));
            
            Assert.AreEqual(3, res.Length);
            Assert.AreEqual(3, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
            Assert.AreEqual("901", res[0].Field2);
            Assert.AreEqual(234, res[0].Field3);

            Assert.IsNull(res[1].Field1);
            Assert.AreEqual("012", res[1].Field2);
            Assert.IsNull(res[1].Field3);

            Assert.AreEqual(new DateTime(1316, 5, 6), res[2].Field1);

            Assert.AreEqual("",
                            engine.WriteString(toWrite).Split(new string[] {"\r\n"}, StringSplitOptions.None)[1].
                                Substring(0, 8).Trim());

        }

        
        [FixedLengthRecord]
        public class NullableType
        {
            [FieldFixedLength(8)]
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime? Field1;

            [FieldFixedLength(3)]
            [FieldTrim(TrimMode.Both)]
            public string Field2;

            [FieldFixedLength(3)]
            public int? Field3;
        }

        [DelimitedRecord("|")]
        private sealed class TestOrder
        {
            public int OrderID;

            public DateTime? OrderDate;

            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime? RequiredDate;

            public int? ShipVia;
        }


        [Test]
        public void WriteNullableTypes2()
        {
            System.Collections.Generic.List<TestOrder> orders = new System.Collections.Generic.List<TestOrder>();

            TestOrder or1 = new TestOrder();
            or1.OrderID = 1;
            or1.OrderDate = null;
            or1.RequiredDate = new DateTime(2007, 1, 2);
            or1.ShipVia = null;
            orders.Add(or1);

            TestOrder or2 = new TestOrder();
            or2.OrderID = 2;
            or2.OrderDate = new DateTime(2007, 2, 1);
            or2.RequiredDate = null;
            or2.ShipVia = 1;
            orders.Add(or2);

            FileHelperEngine<TestOrder> fileHelperEngine = new FileHelperEngine<TestOrder>();
            TestOrder[] res = fileHelperEngine.ReadString(fileHelperEngine.WriteString(orders));

            Assert.IsNull(res[0].OrderDate);
            Assert.IsNull(res[1].RequiredDate);
            Assert.IsNull(res[0].ShipVia);
        }



	}
}