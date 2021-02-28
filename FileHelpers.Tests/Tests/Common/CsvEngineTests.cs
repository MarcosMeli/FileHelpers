#if !NETCOREAPP
using System.Data;
using System.IO;
using FileHelpers.Options;
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

            var options = new CsvOptions(classname, delimiter, file);
            options.HeaderLines = 0;

            var engine = new CsvEngine(options);

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

            var options = new CsvOptions(classname, delimiter, file);
            options.HeaderLines = 2;

            var engine = new CsvEngine(options);

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
        public void GivenDataTableWithColumns_WhenHeaderExportOptionIsOn_ThenHeaderIsInFile()
        {
            DataTable presidents = TwoPresidents();

            var optionsWithHeader = new CsvOptions("Temp", ',', 2)
            {
                IncludeHeaderNames = true
            };

            string path = TestCommon.GetPath("Good", "presidents.txt");
            CsvEngine.DataTableToCsv(presidents, path, optionsWithHeader);

            string[] lines = File.ReadAllLines(path);
            Assert.That(lines.Length, Is.EqualTo(3));
        }

        [Test]
        public void GivenFileWithHeader_WhenImportingWithExportHeaders_ThenImportWillSkipFirstLine()
        {
            // Arrange
            DataTable presidents = TwoPresidents();
            var optionsWithHeader = new CsvOptions("Temp", ',', 2)
            {
                IncludeHeaderNames = true
            };
            string path = TestCommon.GetPath("Good", "presidents.txt");
            CsvEngine.DataTableToCsv(presidents, path, optionsWithHeader);

            // Act
            DataTable freshPresidents = CsvEngine.CsvToDataTable(path, optionsWithHeader);

            // Assert
            Assert.That(freshPresidents.Columns.Count, Is.EqualTo(2));
        }

        [Test]
        public void GivenDataTableWithColumns_WhenHeaderExportOptionIsOff_ThenOnlyDataIsInFile()
        {
            DataTable presidents = TwoPresidents();
            var optionsWithHeader = new CsvOptions("Temp", ',', 2)
            {
                IncludeHeaderNames = false
            };

            string path = TestCommon.GetPath("Good", "presidents.txt");
            CsvEngine.DataTableToCsv(presidents, path, optionsWithHeader);

            var lines = File.ReadAllLines(path);
            Assert.That(lines.Length, Is.EqualTo(2));
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

        [Test]
        public void ReadWithCommaAndBlankLines()
        {
            string file = TestCommon.GetPath("Good", "RealCsvCommaBlankLines1.txt");
            string classname = "CustomerComma";
            char delimiter = ',';

            DataTable dt = CsvEngine.CsvToDataTable(file, classname, delimiter, true, ignoreEmptyLines: true);
            Assert.AreEqual(20, dt.Rows.Count);

            Assert.AreEqual("CustomerID", dt.Columns[0].ColumnName);
        }

        private static DataTable TwoPresidents()
        {
            var data = new DataTable("Presidents");
            data.Columns.Add("Firstname");
            data.Columns.Add("Lastname");
            data.Rows.Add("Angela", "Merkel");
            data.Rows.Add("Theresa", "May");
            return data;
        }

        private static void RunTest(string file, char delimiter, string classname)
        {
            RunTest(file, delimiter, delimiter, classname);
        }

        private static void RunTest(string file, char delimiter, char delimiterHdr, string classname)
        {
            var options = new CsvOptions(classname, delimiter, file)
            {
                HeaderDelimiter = delimiterHdr
            };
            var engine = new CsvEngine(options);

            Assert.AreEqual(classname, engine.RecordType.Name);

            DataTable dt = engine.ReadFileAsDT(file);

            Assert.AreEqual(20, dt.Rows.Count);
            Assert.AreEqual(20, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual("CustomerID", dt.Columns[0].ColumnName);
        }
    }
}
#endif
