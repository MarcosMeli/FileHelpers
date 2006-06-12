using System;
using System.Collections;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class ExcelCsv
	{

		FileHelperEngine engine;

		[Test]
		public void ReadExcelCsv1()
		{
			engine = new FileHelperEngine(typeof (ExcelCsv1Type));

			ExcelCsv1Type[] res = (ExcelCsv1Type[]) TestCommon.ReadTest(engine, @"Good\ExcelCsv1.txt");

			Assert.AreEqual(4, res.Length);

			Assert.AreEqual("AllwaysOnTop", res[0].OrganizationName);
			Assert.AreEqual("COMPUTERS, HARDWARE", res[1].OrganizationName);
			Assert.AreEqual("4S Consulting, Inc.", res[2].OrganizationName);
			Assert.AreEqual("SmartSolutions", res[3].OrganizationName);

			Assert.AreEqual("Test 1,", res[0].TestField);
			Assert.AreEqual("Test, 2", res[1].TestField);
			Assert.AreEqual(" Test 3", res[2].TestField);
			Assert.AreEqual("Test 4", res[3].TestField);
			
		}

		[Test]
		public void ReadExcelCsv2()
		{
			engine = new FileHelperEngine(typeof (ExcelCsv2Type));

			ExcelCsv2Type[] res = (ExcelCsv2Type[]) TestCommon.ReadTest(engine, @"Good\ExcelCsv2.txt");

			Assert.AreEqual(4, res.Length);

			Assert.AreEqual("AllwaysOnTop", res[0].OrganizationName);
			Assert.AreEqual("COMPUTERS, HARDWARE", res[1].OrganizationName);
			Assert.AreEqual("4S Consulting, Inc.", res[2].OrganizationName);
			Assert.AreEqual("SmartSolutions", res[3].OrganizationName);

			Assert.AreEqual("Test 1,", res[0].TestField);
			Assert.AreEqual("Test, 2", res[1].TestField);
			Assert.AreEqual(" Test 3", res[2].TestField);
			Assert.AreEqual("Test 4", res[3].TestField);
			
		}

		[Test]
		public void WriteExcelCsv1()
		{
			ArrayList arr = new ArrayList();
			ExcelCsv1Type record;

			record = new ExcelCsv1Type();
			record.OrganizationName = "AllwaysOnTop";
			record.TestField = "Test 1,";
			arr.Add(record);

			record = new ExcelCsv1Type();
			record.OrganizationName = "COMPUTERS, HARDWARE";
			record.TestField = "Test, 2";
			arr.Add(record);

			record = new ExcelCsv1Type();
			record.OrganizationName = "4S Consulting, Inc.";
			record.TestField = " Test 3";
			arr.Add(record);

			record = new ExcelCsv1Type();
			record.OrganizationName = "SmartSolutions";
			record.TestField = "Test 4";
			arr.Add(record);

			engine = new FileHelperEngine(typeof (ExcelCsv1Type));

			string tmp = engine.WriteString(arr.ToArray());
			ExcelCsv1Type[] res = (ExcelCsv1Type[]) engine.ReadString(tmp);
            
			Assert.AreEqual(4, res.Length);

			Assert.AreEqual("AllwaysOnTop", res[0].OrganizationName);
			Assert.AreEqual("COMPUTERS, HARDWARE", res[1].OrganizationName);
			Assert.AreEqual("4S Consulting, Inc.", res[2].OrganizationName);
			Assert.AreEqual("SmartSolutions", res[3].OrganizationName);

			Assert.AreEqual("Test 1,", res[0].TestField);
			Assert.AreEqual("Test, 2", res[1].TestField);
			Assert.AreEqual(" Test 3", res[2].TestField);
			Assert.AreEqual("Test 4", res[3].TestField);
			
		}

		[Test]
		public void WriteExcelCsv2()
		{
			ArrayList arr = new ArrayList();
			ExcelCsv2Type record;

			record = new ExcelCsv2Type();
			record.OrganizationName = "AllwaysOnTop";
			record.TestField = "Test 1,";
			arr.Add(record);

			record = new ExcelCsv2Type();
			record.OrganizationName = "COMPUTERS, HARDWARE";
			record.TestField = "Test, 2";
			arr.Add(record);

			record = new ExcelCsv2Type();
			record.OrganizationName = "4S Consulting, Inc.";
			record.TestField = " Test 3";
			arr.Add(record);

			record = new ExcelCsv2Type();
			record.OrganizationName = "SmartSolutions";
			record.TestField = "Test 4";
			arr.Add(record);

			engine = new FileHelperEngine(typeof (ExcelCsv2Type));

			string tmp = engine.WriteString(arr.ToArray());
			ExcelCsv2Type[] res = (ExcelCsv2Type[]) engine.ReadString(tmp);
            
			Assert.AreEqual(4, res.Length);

			Assert.AreEqual("AllwaysOnTop", res[0].OrganizationName);
			Assert.AreEqual("COMPUTERS, HARDWARE", res[1].OrganizationName);
			Assert.AreEqual("4S Consulting, Inc.", res[2].OrganizationName);
			Assert.AreEqual("SmartSolutions", res[3].OrganizationName);

			Assert.AreEqual("Test 1,", res[0].TestField);
			Assert.AreEqual("Test, 2", res[1].TestField);
			Assert.AreEqual(" Test 3", res[2].TestField);
			Assert.AreEqual("Test 4", res[3].TestField);
			
		}

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void ReadExcelCsv3()
		{
			engine = new FileHelperEngine(typeof (ExcelCsv3Type));
			TestCommon.ReadTest(engine, @"Good\ExcelCsv2.txt");
		}

	}

	[DelimitedRecord(",")]
	public sealed class ExcelCsv1Type
	{

		public int Id;
	
		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string OrganizationName;

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string TestField;

	} 

	[DelimitedRecord(",")]
	public sealed class ExcelCsv2Type
	{

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string OrganizationName;

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string TestField;

	} 

	[DelimitedRecord(",")]
	public sealed class ExcelCsv3Type
	{

		[FieldQuoted('"')]
		public string OrganizationName;

		[FieldQuoted('"')]
		public string TestField;

	} 
}
