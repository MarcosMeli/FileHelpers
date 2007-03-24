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


#if NET_2_0

        [Test]
        public void ReadNullableTypes()
        {
            engine = new FileHelperEngine(typeof(NullableType));

            NullableType[] res;
            res = (NullableType[])Common.ReadTest(engine, @"Good\NullableTypes1.txt");

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
            [FieldAlign(AlignMode.Right, '0')]
            public int? Field3;
        }

#endif

	}
}