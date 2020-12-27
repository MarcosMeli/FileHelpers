using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using FileHelpers;
using FileHelpers.Converters;
using FileHelpers.ExcelNPOIStorage;
using NPOI.XSSF.UserModel;

namespace OurTest
{
    internal class Program
    {
        private static void Main()
        {
            //General export of excel with date time columns
            var provider = new ExcelNPOIStorage(typeof(RaRecord))
            {
                FileName = Directory.GetCurrentDirectory() + @"\test.xlsx",
                StartRow = 0,
                StartColumn = 0
            };

            var records = new List<RaRecord>
            {
                new RaRecord()
                {
                    Level = 123.123m,
                    Name = "Dickie",
                    Startdate = DateTime.Now
                }
            };

            var values = new List<int>
            {
                1,
                2,
                3
            };

            records.Add(new RaRecord()
            {
                Level = null,
                Name = "Bavo",
                Project = "too many",
                Startdate = DateTime.Now,
                ListOfIds = string.Join(",", values.Select(n => n.ToString(CultureInfo.InvariantCulture)).ToArray())
            });

            provider.HeaderRows = 4;
            provider.InsertRecords(records.ToArray());

            var res = (RaRecord[])provider.ExtractRecords();

            TestBlankFields();
            TestExtractRecordsUsingStream();
        }

        private static void TestBlankFields()
        {
            var provider = new ExcelNPOIStorage(typeof(RaRecord))
            {
                FileName = Directory.GetCurrentDirectory() + @"\testBlankFields.xlsx",
                StartColumn = 0
            };

            var workbook = new XSSFWorkbook(provider.FileName);
            var row = workbook.GetSheet("Sheet2").GetRow(0);
            var firstLineFields = row.Cells.Select(c => c.StringCellValue.Trim());
            var unusedPropertyNames = provider.FieldFriendlyNames.Except(firstLineFields).ToList();

            foreach (var propertyName in unusedPropertyNames)
            {
                provider.RemoveField(propertyName);
            }

            var res = (RaRecord[])provider.ExtractRecords();
        }

        private static void TestExtractRecordsUsingStream()
        {
            var provider = new ExcelNPOIStorage(typeof(RaRecord))
            {
                StartColumn = 0
            };
            var stream = new FileStream(Directory.GetCurrentDirectory() + @"\test.xlsx", FileMode.Open, FileAccess.Read);
            var res = provider.ExtractRecords(stream).Cast<RaRecord>();
        }
    }

    [DelimitedRecord("|")]
    public class RaRecord
    {
        [FieldOrder(1)]
        public string Name;
        [FieldOrder(2)]
        public string Project;
        [FieldOrder(3)]
        [SingleConverter]
        public decimal? Level;
        [FieldOrder(4)]
        [DateTimeConverter("dd-MMM-yyyy")]
        public DateTime? Startdate;
        [FieldOrder(5)]
        public string ListOfIds;
    }
}
