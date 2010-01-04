using System;
using System.Data;
using System.IO;
using FileHelpers;
using FileHelpers.Options;
using FileHelpers.Dynamic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class CsvEngineTests
	{

		[Test]
		public void ReadFileComma()
		{
			string file = TestCommon.GetPath("Good", "RealCsvComma1.txt");
			string classname = "CustomerComma";
			char delimiter = ',';

			RunTest(file, delimiter, classname);
		}

        
        [Test]
        public void ReadFileHeader1()
        {
            string file = TestCommon.GetPath("Good", "RealCsvComma1.txt");
            string classname = "CustomerComma";
            char delimiter = ',';
        
            CsvOptions options = new CsvOptions(classname, delimiter, file);
            options.HeaderLines = 0;
            
            CsvEngine engine = new CsvEngine(options);

            Assert.AreEqual(classname, engine.RecordType.Name);

            DataTable dt = engine.ReadFileAsDT(file);

            Assert.AreEqual(21, dt.Rows.Count);
            Assert.AreEqual(21, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual("Field_0", dt.Columns[0].ColumnName);
        }

        [Test]
        public void ReadFileHeader2()
        {
            string file = TestCommon.GetPath("Good", "RealCsvComma1.txt");
            string classname = "CustomerComma";
            char delimiter = ',';

            CsvOptions options = new CsvOptions(classname, delimiter, file);
            options.HeaderLines = 2;

            CsvEngine engine = new CsvEngine(options);

            Assert.AreEqual(classname, engine.RecordType.Name);

            DataTable dt = engine.ReadFileAsDT(file);

            Assert.AreEqual(19, dt.Rows.Count);
            Assert.AreEqual(19, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual("CustomerID", dt.Columns[0].ColumnName);
        }

		[Test]
		public void ReadFileComma2()
		{
			string file = TestCommon.GetPath("Good", "RealCsvComma2.txt");
			string classname = "CustomerComma";
			char delimiter = ',';
			char delimiterHdr = '|';

			RunTest(file, delimiter, delimiterHdr, classname);
		}

		[Test]
		public void ReadFileVerticalBar()
		{

			string file = TestCommon.GetPath("Good", "RealCsvVerticalBar1.txt");
			string classname = "CustomerVerticalBar";
			char delimiter = '|';

			RunTest(file, delimiter, classname);
		}

		[Test]
		public void ReadFileVerticalBar2()
		{
			string file = TestCommon.GetPath("Good", "RealCsvVerticalBar2.txt");
			string classname = "CustomerVerticalBar";
			char delimiter = '|';
			char delimiterHdr = ',';

			RunTest(file, delimiter, delimiterHdr, classname);
		}

		[Test]
		public void DualEngine()
		{
			string file = TestCommon.GetPath("Good", "RealCsvVerticalBar2.txt");
			string classname = "CustomerVerticalBar";
			char delimiter = '|';

			CsvEngine engine = new CsvEngine(classname, delimiter, file);
			CsvEngine engine2 = new CsvEngine(classname, delimiter, file);

		}

		private void RunTest(string file, char delimiter, string classname)
		{
			RunTest(file, delimiter, delimiter, classname);
		}

		private void RunTest(string file, char delimiter, char delimiterHdr, string classname)
		{
			CsvOptions options = new CsvOptions(classname, delimiter, file);
			options.HeaderDelimiter = delimiterHdr;
			CsvEngine engine = new CsvEngine(options);
	
			Assert.AreEqual(classname, engine.RecordType.Name);
	
			DataTable dt = engine.ReadFileAsDT(file);
	
			Assert.AreEqual(20, dt.Rows.Count);
			Assert.AreEqual(20, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual("CustomerID", dt.Columns[0].ColumnName);
		}

		[Test]
		
		public void BadName()
		{
            Assert.Throws<FileHelpersException>(
                () => new CsvEngine("Ops, somerrors", '|', TestCommon.GetPath("Good", "RealCsvVerticalBar1.txt")));
		}

		[Test]
		public void ReadWithCommon()
		{
			string file = TestCommon.GetPath("Good", "RealCsvComma1.txt");
			string classname = "CustomerComma";
			char delimiter = ',';

			DataTable dt = CsvEngine.CsvToDataTable(file, classname, delimiter);
			Assert.AreEqual(20, dt.Rows.Count);

			Assert.AreEqual("CustomerID", dt.Columns[0].ColumnName);
		}

	}
}