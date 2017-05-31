using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using FileHelpers;
using FileHelpers.Dynamic;
using FileHelpers.ExcelNPOIStorage;
using NPOI.XSSF.UserModel;

namespace OurTest
{  
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Dynamic Records
            var cb = new DelimitedClassBuilder("Customer", "|")
            {
                IgnoreFirstLines = 1,
                IgnoreEmptyLines = true
            };

            cb.AddField("BirthDate", typeof(DateTime));
            cb.LastField.TrimMode = TrimMode.Both;
            cb.LastField.FieldNullValue = DateTime.Today;

            cb.AddField("Name", typeof(string));
            cb.LastField.FieldQuoted = true;
            cb.LastField.QuoteChar = '"';

            cb.AddField("Age", typeof(int));           

            var providerWithDynamicRecord = new ExcelNPOIStorage(cb.CreateRecordClass())
            {                
                FileName =Directory.GetCurrentDirectory()+@"\testDynamicRecords.xlsx"
            };

            providerWithDynamicRecord.StartRow = 1;
            providerWithDynamicRecord.StartColumn = 1;

            dynamic dynamicRecord = Activator.CreateInstance(providerWithDynamicRecord.RecordType);

            dynamicRecord.Name = "Jonh";
            dynamicRecord.Age = 1;
            dynamicRecord.BirthDate = DateTime.Now;

            var valuesList = new List<dynamic>
            {
                dynamicRecord,
                dynamicRecord
            };

            var columnsHeaders = ((System.Reflection.TypeInfo)(dynamicRecord.GetType())).DeclaredFields.Select(x => x.Name).ToList();
            providerWithDynamicRecord.ColumnsHeaders = columnsHeaders;
            providerWithDynamicRecord.InsertRecords(valuesList.ToArray());            

            //General export of excel with date time columns
            var provider = new ExcelNPOIStorage(typeof (RaRecord))
            {
                FileName = Directory.GetCurrentDirectory() + @"\test.xlsx"
            };

            provider.StartRow = 0;
            provider.StartColumn = 0;

            var records = new List<RaRecord>();

            records.Add(new RaRecord()
            {
                Level = 123.123m,
                Name = "Dickie",
                Startdate = DateTime.Now
            });

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
    }
    
    [DelimitedRecord("|")]
    public class RaRecord
    {
        [FieldOrder(1)]        
        public string Name;
        [FieldOrder(2)]        
        public string Project;
        [FieldOrder(3)]  
        [FieldConverter(ConverterKind.Decimal)]
        public decimal? Level;
        [FieldOrder(4)]
        [FieldConverter(ConverterKind.Date, "dd-MMM-yyyy")] 
        public DateTime? Startdate;
        [FieldOrder(5)]
        public string ListOfIds;
    }
}
