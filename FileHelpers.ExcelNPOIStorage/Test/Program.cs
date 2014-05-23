using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using FileHelpers.DataLink;
using FileHelpers.ExcelNPOIStorage;

namespace OurTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /*var provider = new ExcelStorage(typeof(RaRecord)) {
                StartRow = 2,
                StartColumn = 1,
                SheetName = "Sheet2",
                FileName = "test.xlsx"
            };*/
            var provider = new ExcelNPOIStorage(typeof (RaRecord)) {
                SheetName = "SheetBavo",
                FileName = "test.xlsx"
            };
            provider.StartRow = 1;
            provider.StartColumn = 0;

            var records = new List<RaRecord>();
            records.Add(new RaRecord() {
                Level = 123.123m,
                Name = "Dickie"
            });
            records.Add(new RaRecord() {
                Level = null,
                Name = "Bavo",
                Project = "too many",
                Startdate = DateTime.Now
            });

            provider.InsertRecords(records.ToArray());
            //var res = (RaRecord[])provider.ExtractRecords();
        }

//        static void Main(string[] args)
//        {
//            var provider = new ExcelStorage(typeof(RaRecord));
//
//            provider.StartRow = 2;
//            //provider.StartColumn = 2;
//
//            provider.FileName = "test.new.xlsx";
//            provider.TemplateFile = "test.xlsx";
//            provider.SheetName = "TEST";
//            var records = new RaRecord[] {
//                new RaRecord{Level = 1, Name = "Bavo", Project = "Eandis"}, 
//                new RaRecord{Name = "Michiel", Project = "Eandis", Startdate = DateTime.Now}, 
//                new RaRecord{Name = "", Startdate = DateTime.Now.AddDays(10)}, 
//            };
//
//            provider.InsertRecords(records);
//        }
    }


    [DelimitedRecord("|")]
    public class RaRecord
    {
        public string Name;
        public string Project;
        public decimal? Level;
        public DateTime? Startdate;
    }
}