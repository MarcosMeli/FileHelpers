using System;
using System.Data;
using System.IO;
using FileHelpers;
using FileHelpers.RunTime;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
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

			DataTable dt = engine.ReadFileAsDT(Common.TestPath(@"Good\test1.txt"));

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
			Type t = ClassBuilder.ClassFromString(mClassVbNet, NetLanguage.VbNet);

			engine = new FileHelperEngine(t);

			DataTable dt = engine.ReadFileAsDT(Common.TestPath(@"Good\test1.txt"));

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

			object[] res = Common.ReadAllAsync(asyncEngine, @"Good\test1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

		}

		[Test]
		public void ReadFileAsyncVbNet()
		{
			
			Type t = ClassBuilder.ClassFromString(mClassVbNet, "SampleType", NetLanguage.VbNet);

			asyncEngine = new FileHelperAsyncEngine(t);

			object[] res = Common.ReadAllAsync(asyncEngine, @"Good\test1.txt");

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

			DataTable dt = engine.ReadFileAsDT(Common.TestPath(@"Good\test1.txt"));

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
			Type t = ClassBuilder.ClassFromBinaryFile(Common.TestPath(@"Classes\SampleBinaryClass.fhc"));

			engine = new FileHelperEngine(t);

			DataTable dt = engine.ReadFileAsDT(Common.TestPath(@"Good\test1.txt"));

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
			Type t = ClassBuilder.ClassFromSourceFile(Common.TestPath(@"Classes\SampleClass.cs"));

			engine = new FileHelperEngine(t);

			DataTable dt = engine.ReadFileAsDT(Common.TestPath(@"Good\test1.txt"));

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
			Type t = ClassBuilder.ClassFromSourceFile(Common.TestPath(@"Classes\SampleClass.vb"), NetLanguage.VbNet);

            engine = new FileHelperEngine(t);

			DataTable dt = engine.ReadFileAsDT(Common.TestPath(@"Good\test1.txt"));

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
			DelimitedClassBuilder cb = new DelimitedClassBuilder("Customers", ",");
			cb.IgnoreFirstLines = 1;
			cb.IgnoreEmptyLines = true;
			
			cb.AddField("Field1", typeof(DateTime));
			cb.LastField.TrimMode = TrimMode.Both;
			cb.LastField.QuoteMode = QuoteMode.AlwaysQuoted;
			cb.LastField.FieldNullValue = DateTime.Today;

			cb.AddField("Field2", typeof(string));
			cb.LastField.FieldQuoted = true;
			cb.LastField.QuoteChar = '"';

			cb.AddField("Field3", typeof(int));
			
			engine = new FileHelperEngine(cb.CreateRecordClass());

			DataTable dt = engine.ReadFileAsDT(Common.TestPath(@"Good\test2.txt"));

			Assert.AreEqual(4, dt.Rows.Count);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
			
			Assert.AreEqual("Hola", dt.Rows[0][1]);
			Assert.AreEqual(DateTime.Today, dt.Rows[2][0]);
			
		}

		[Test]
		public void FullClassBuildingFixed()
		{
			FixedLengthClassBuilder cb = new FixedLengthClassBuilder("Customers");

			cb.AddField("Field1", 8, typeof(DateTime));
			cb.LastField.Converter.Kind = ConverterKind.Date;
			cb.LastField.Converter.Arg1 = "ddMMyyyy";
			cb.LastField.FieldNullValue = DateTime.Now;
			
			
			cb.AddField("Field2", 3, typeof(string));
			cb.LastField.AlignMode = AlignMode.Right;
			cb.LastField.AlignChar = ' ';
			
			cb.AddField("Field3", 3, typeof(int));
			 
			cb.LastField.AlignMode = AlignMode.Right;
			cb.LastField.AlignChar = '0';
			cb.LastField.TrimMode = TrimMode.Both;
			
			engine = new FileHelperEngine(cb.CreateRecordClass());

			DataTable dt = engine.ReadFileAsDT(Common.TestPath(@"Good\test1.txt"));

			Assert.AreEqual(4, dt.Rows.Count);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		public ClassBuilder CommonCreate()
		{
			FixedLengthClassBuilder cb = new FixedLengthClassBuilder("Customers");

			cb.AddField("Field1", 8, typeof(DateTime));
			cb.LastField.Converter.Kind = ConverterKind.Date;
			cb.LastField.Converter.Arg1 = "ddMMyyyy";
			cb.LastField.FieldNullValue = DateTime.Now;
			
			cb.AddField("Field2", 3, typeof(string));
			
			cb.LastField.AlignMode = AlignMode.Right;
			cb.LastField.AlignChar = ' ';
			
			cb.AddField("Field3", 3, typeof(int));
			 
			cb.LastField.AlignMode = AlignMode.Right;
			cb.LastField.AlignChar = '0';
			cb.LastField.TrimMode = TrimMode.Both;
			
			return cb;
		}

	
		private void ValidateType(Type t)
		{
			engine = new FileHelperEngine(t);

			DataTable dt = engine.ReadFileAsDT(Common.TestPath(@"Good\test1.txt"));

			Assert.AreEqual(4, dt.Rows.Count);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
		public void SaveLoadSourceFile()
		{
			ClassBuilder cb = CommonCreate();
			cb.SaveToSourceFile("tempclass.cs");
			ValidateType(ClassBuilder.ClassFromSourceFile("tempclass.cs"));
		}

		[Test]
		public void SaveLoadSourceFileVbNet()
		{
			ClassBuilder cb = CommonCreate();
			cb.SaveToSourceFile("tempclass.cs", NetLanguage.VbNet);
			ValidateType(ClassBuilder.ClassFromSourceFile("tempclass.cs", NetLanguage.VbNet));
		}

		[Test]
		public void SaveLoadBinaryFile()
		{
			ClassBuilder cb = CommonCreate();
			cb.SaveToSourceFile("tempclass.cs");
			ValidateType(ClassBuilder.ClassFromSourceFile("tempclass.cs"));
		}

		[Test]
		public void SaveLoadBinaryFileVbNet()
		{
			ClassBuilder cb = CommonCreate();
			cb.SaveToBinaryFile("tempclass.cs", NetLanguage.VbNet);
			ValidateType(ClassBuilder.ClassFromBinaryFile("tempclass.cs", NetLanguage.VbNet));
		}


		[Test]
		public void SaveLoadXmlFileFixed()
		{
			FixedLengthClassBuilder cb = new FixedLengthClassBuilder("Customers");

			cb.AddField("Field1", 8, typeof(DateTime));
			cb.LastField.Converter.Kind = ConverterKind.Date;
			cb.LastField.Converter.Arg1 = "ddMMyyyy";
			cb.LastField.FieldNullValue = DateTime.Now;
			
			cb.AddField("FieldSecond", 3, typeof(string));
			cb.LastField.AlignMode = AlignMode.Right;
			cb.LastField.AlignChar = ' ';
			
			cb.AddField("Field33", 3, typeof(int));
			 
			cb.LastField.AlignMode = AlignMode.Right;
			cb.LastField.AlignChar = '0';
			cb.LastField.TrimMode = TrimMode.Both;
			
			cb.SaveToXml(@"c:\runtime.xml");

			FixedLengthClassBuilder loaded = (FixedLengthClassBuilder) ClassBuilder.LoadFromXml(@"c:\runtime.xml");

			Assert.AreEqual("Field1", loaded.FieldByIndex(0).FieldName);
			Assert.AreEqual("FieldSecond", loaded.FieldByIndex(1).FieldName);
			Assert.AreEqual("Field33", loaded.FieldByIndex(2).FieldName);
			
			Assert.AreEqual("System.DateTime", loaded.FieldByIndex(0).FieldType);
			Assert.AreEqual("System.String", loaded.FieldByIndex(1).FieldType);
			Assert.AreEqual("System.Int32", loaded.FieldByIndex(2).FieldType);
			
			Assert.AreEqual(ConverterKind.Date, loaded.FieldByIndex(0).Converter.Kind);
			Assert.AreEqual("ddMMyyyy", loaded.FieldByIndex(0).Converter.Arg1);
			
			Assert.AreEqual(AlignMode.Right, loaded.FieldByIndex(1).AlignMode);
			Assert.AreEqual(' ', loaded.FieldByIndex(1).AlignChar);
		}

		[Test]
		public void SaveLoadXmlFileDelimited()
		{
			DelimitedClassBuilder cb = new DelimitedClassBuilder("Customers", ",");
			cb.IgnoreFirstLines = 1;
			cb.IgnoreEmptyLines = true;
			
			cb.AddField("Field1", typeof(DateTime));
			cb.LastField.TrimMode = TrimMode.Both;
			cb.LastField.QuoteMode = QuoteMode.AlwaysQuoted;
			cb.LastField.FieldNullValue = DateTime.Today;

			cb.AddField("FieldTwo", typeof(string));
			cb.LastField.FieldQuoted = true;
			cb.LastField.QuoteChar = '"';

			cb.AddField("Field333", typeof(int));

			cb.SaveToXml(@"c:\runtime.xml");
			
			DelimitedClassBuilder loaded = (DelimitedClassBuilder) ClassBuilder.LoadFromXml(@"c:\runtime.xml");
			
			Assert.AreEqual("Field1", loaded.FieldByIndex(0).FieldName);
			Assert.AreEqual("FieldTwo", loaded.FieldByIndex(1).FieldName);
			Assert.AreEqual("Field333", loaded.FieldByIndex(2).FieldName);
			
			Assert.AreEqual("System.DateTime", loaded.FieldByIndex(0).FieldType);
			Assert.AreEqual("System.String", loaded.FieldByIndex(1).FieldType);
			Assert.AreEqual("System.Int32", loaded.FieldByIndex(2).FieldType);
			
			Assert.AreEqual(QuoteMode.AlwaysQuoted, loaded.FieldByIndex(0).QuoteMode);
			Assert.AreEqual(false, loaded.FieldByIndex(0).FieldQuoted);
			
			Assert.AreEqual('"', loaded.FieldByIndex(1).QuoteChar);
			Assert.AreEqual(true, loaded.FieldByIndex(1).FieldQuoted);
		}

	}
}