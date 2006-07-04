using System;
using System.Data;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class RunTimeClass
	{
		FileHelperEngine engine;
		FileHelperAsyncEngine asyncEngine;

		public string mClass = 
		@"	[FixedLengthRecord]
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
			}
		";

		public string mClassVbNet = 
			@"	<FixedLengthRecord> _
			Public Class SampleType
			
				<FieldFixedLength(8), _
				 FieldConverter(ConverterKind.Date, ""ddMMyyyy"") > _
				public Field1 As DateTime

				<FieldFixedLength(3), _
				 FieldAlign(AlignMode.Left, "" ""c), _
				 FieldTrim(TrimMode.Both)> _
				public Field2 As String

				<FieldFixedLength(3), _
				 FieldAlign(AlignMode.Right, ""0""c), _
				 FieldTrim(TrimMode.Both) > _
				public Field3 As Integer
			
			End Class
		";

		[Test]
		public void ReadFile()
		{
			Type t = ClassBuilder.ClassFromString(mClass);

			engine = new FileHelperEngine(t);

			DataTable dt = engine.ReadFileAsDT(TestCommon.TestPath(@"Good\test1.txt"));

			Assert.AreEqual(4, dt.Rows.Count);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(1314, 12, 11), dt.Rows[0][0]);
			Assert.AreEqual("901", dt.Rows[0][1]);
			Assert.AreEqual(234, dt.Rows[0][2]);

			Assert.AreEqual(new DateTime(1314, 11, 10), dt.Rows[1][0]);
			Assert.AreEqual("012", dt.Rows[1][1]);
			Assert.AreEqual(345, dt.Rows[1][2]);

		}

		[Test]
		public void ReadFileVbNet()
		{
			Type t = ClassBuilder.ClassFromString(mClassVbNet, NetLenguage.VbNet);

			engine = new FileHelperEngine(t);

			DataTable dt = engine.ReadFileAsDT(TestCommon.TestPath(@"Good\test1.txt"));

			Assert.AreEqual(4, dt.Rows.Count);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(1314, 12, 11), dt.Rows[0][0]);
			Assert.AreEqual("901", dt.Rows[0][1]);
			Assert.AreEqual(234, dt.Rows[0][2]);

			Assert.AreEqual(new DateTime(1314, 11, 10), dt.Rows[1][0]);
			Assert.AreEqual("012", dt.Rows[1][1]);
			Assert.AreEqual(345, dt.Rows[1][2]);

		}

		[Test]
		public void ReadFileAsync()
		{
			
			Type t = ClassBuilder.ClassFromString(mClass, "SampleType");

			asyncEngine = new FileHelperAsyncEngine(t);

			object[] res = TestCommon.ReadAllAsync(asyncEngine, @"Good\test1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

		}

		[Test]
		public void ReadFileAsyncVbNet()
		{
			
			Type t = ClassBuilder.ClassFromString(mClassVbNet, "SampleType", NetLenguage.VbNet);

			asyncEngine = new FileHelperAsyncEngine(t);

			object[] res = TestCommon.ReadAllAsync(asyncEngine, @"Good\test1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

		}

		
		
		[Test]
		public void ReadFileEncDec()
		{
			ClassBuilder.ClassToBinaryFile("temp.fhc", mClass);

			Type t = ClassBuilder.ClassFromBinaryFile("temp.fhc");
			File.Delete("temp.fhc");

			engine = new FileHelperEngine(t);

			DataTable dt = engine.ReadFileAsDT(TestCommon.TestPath(@"Good\test1.txt"));

			Assert.AreEqual(4, dt.Rows.Count);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(1314, 12, 11), dt.Rows[0][0]);
			Assert.AreEqual("901", dt.Rows[0][1]);
			Assert.AreEqual(234, dt.Rows[0][2]);

			Assert.AreEqual(new DateTime(1314, 11, 10), dt.Rows[1][0]);
			Assert.AreEqual("012", dt.Rows[1][1]);
			Assert.AreEqual(345, dt.Rows[1][2]);

		}

		[Test]
		public void ReadFileClassInFileEnc()
		{
			Type t = ClassBuilder.ClassFromBinaryFile(TestCommon.TestPath(@"Classes\SampleBinaryClass.fhc"));

			engine = new FileHelperEngine(t);

			DataTable dt = engine.ReadFileAsDT(TestCommon.TestPath(@"Good\test1.txt"));

			Assert.AreEqual(4, dt.Rows.Count);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(1314, 12, 11), dt.Rows[0][0]);
			Assert.AreEqual("901", dt.Rows[0][1]);
			Assert.AreEqual(234, dt.Rows[0][2]);

			Assert.AreEqual(new DateTime(1314, 11, 10), dt.Rows[1][0]);
			Assert.AreEqual("012", dt.Rows[1][1]);
			Assert.AreEqual(345, dt.Rows[1][2]);

		}

		[Test]
		public void ReadFileClassInFile()
		{
			Type t = ClassBuilder.ClassFromBinaryFile(TestCommon.TestPath(@"Classes\SampleClass.cs"));

			engine = new FileHelperEngine(t);

			DataTable dt = engine.ReadFileAsDT(TestCommon.TestPath(@"Good\test1.txt"));

			Assert.AreEqual(4, dt.Rows.Count);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(1314, 12, 11), dt.Rows[0][0]);
			Assert.AreEqual("901", dt.Rows[0][1]);
			Assert.AreEqual(234, dt.Rows[0][2]);

			Assert.AreEqual(new DateTime(1314, 11, 10), dt.Rows[1][0]);
			Assert.AreEqual("012", dt.Rows[1][1]);
			Assert.AreEqual(345, dt.Rows[1][2]);
		}

		[Test]
		public void ReadFileClassInFileVbNet()
		{
			Type t = ClassBuilder.ClassFromBinaryFile(TestCommon.TestPath(@"Classes\SampleClass.vb"), NetLenguage.VbNet);


            engine = new FileHelperEngine(t);

			DataTable dt = engine.ReadFileAsDT(TestCommon.TestPath(@"Good\test1.txt"));

			Assert.AreEqual(4, dt.Rows.Count);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(1314, 12, 11), dt.Rows[0][0]);
			Assert.AreEqual("901", dt.Rows[0][1]);
			Assert.AreEqual(234, dt.Rows[0][2]);

			Assert.AreEqual(new DateTime(1314, 11, 10), dt.Rows[1][0]);
			Assert.AreEqual("012", dt.Rows[1][1]);
			Assert.AreEqual(345, dt.Rows[1][2]);
		}

		[Test]
		public void FullClassBuilding()
		{
			DelimitedClassBuilder cb = new DelimitedClassBuilder("MyCustomers", ",");
			cb.IgnoreFirstLines = 2;
			cb.IgnoreEmptyLines = true;

			cb.AddField("Field1", typeof(DateTime));
			cb.LastField.TrimMode = TrimMode.Both;

			cb.AddField("Field2", typeof(string));
			cb.LastField.FieldQuoted = true;
			cb.LastField.QuoteChar = '"';


			cb.AddField("Field3", typeof(int));
			cb.LastField.AlignMode = AlignMode.Right;
			
			engine = new FileHelperEngine(cb.CreateType());

			DataTable dt = engine.ReadFileAsDT(TestCommon.TestPath(@"Good\test1.txt"));

			Assert.AreEqual(4, dt.Rows.Count);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

		}

		[Test]
		public void FullClassBuildingFixed()
		{
			FixedClassBuilder cb = new FixedClassBuilder("MyCustomers");
			cb.IgnoreFirstLines = 2;
			cb.IgnoreEmptyLines = true;

			cb.AddField("Field1", 8, typeof(DateTime));
			cb.LastField.TrimMode = TrimMode.Both;

			cb.AddField("Field2", 3, typeof(string));

			cb.AddField("Field3", 3, typeof(int));
			cb.LastField.AlignMode = AlignMode.Right;
			
			engine = new FileHelperEngine(cb.CreateType());

			DataTable dt = engine.ReadFileAsDT(TestCommon.TestPath(@"Good\test1.txt"));

			Assert.AreEqual(4, dt.Rows.Count);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

		}
	}
}