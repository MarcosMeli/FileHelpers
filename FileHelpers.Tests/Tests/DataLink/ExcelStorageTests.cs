#if ! MINI

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpers.Tests.Tests.DataLink
{
    [TestFixture]
    [Category("Excel")]
    public class ExcelStorageTests
    {
        [Test]
        [Ignore]
        public void ReadExcelStorageWithNoEmptyRows_ShouldReadAll()
        {
            ExcelXlsType[] res = ReadFromExcelStorage("ExcelWithNoEmptyRows.xlsx", stopAfterEmptyRows: 1);
            AssertExpectedResults(res, expectedResultCount: 4);
        }

        [Test]
        [Ignore]
        public void ReadExcelStorageWithOneEmptyRows_StopAfterOneEmpty_ShouldStopEarly()
        {
            var res = ReadFromExcelStorage("ExcelWithOneEmptyRows.xlsx", stopAfterEmptyRows: 1);
            AssertExpectedResults(res, expectedResultCount: 2);
        }

        [Test]
        [Ignore]
        public void ReadExcelStorageWithOneEmptyRows_StopAfterTwoEmpty_ShouldNotStopEarly()
        {
            ExcelXlsType[] res = ReadFromExcelStorage("ExcelWithOneEmptyRows.xlsx", stopAfterEmptyRows: 2);
            AssertExpectedResults(res, expectedResultCount: 4);
        }

        [Test]
        [Ignore]
        public void ReadExcelStorageWithTwoEmptyRows_StopAfterTwoEmpty_ShouldStopEarly()
        {
            ExcelXlsType[] res = ReadFromExcelStorage("ExcelWithTwoEmptyRows.xlsx", stopAfterEmptyRows: 2);
            AssertExpectedResults(res, expectedResultCount: 2);
        }

        [Test]
        [Ignore]
        public void ReadExcelStorageWithTwoEmptyRows_StopAfterThreeEmpty_ShouldNotStopEarly()
        {
            ExcelXlsType[] res = ReadFromExcelStorage("ExcelWithTwoEmptyRows.xlsx", stopAfterEmptyRows: 3);
            AssertExpectedResults(res, expectedResultCount: 4);
        }

        [Test]
        [Ignore]
        public void ReadExcelStorageWithNoEmptyRows_ShouldHandleStressTest()
        {
            // This will not perform well as it looks 100 rows ahead on each row for empty rows
            // not an expected use case but thought I'd test the edge case anyway
            ExcelXlsType[] res = ReadFromExcelStorage("ExcelWithNoEmptyRows.xlsx", stopAfterEmptyRows: 100);
            AssertExpectedResults(res, expectedResultCount: 4);
        }

        [Test]
        [Ignore]
        public void ReadExcelStorageWithCustomSheets_ReturnsAllSheets()
        {
            var provider = new ExcelStorage(typeof (ExcelXlsType));
            provider.FileName = TestCommon.GetPath("Excel", "ExcelWithCustomSheets.xlsx");

            Assert.AreEqual(3, provider.Sheets.Count);
            Assert.AreEqual("TestSheet1", provider.Sheets[0]);
            Assert.AreEqual("Test-Sheet-2", provider.Sheets[1]);
            Assert.AreEqual("Test Sheet 3", provider.Sheets[2]);
        }

        private static void AssertExpectedResults(ExcelXlsType[] res, int expectedResultCount)
        {
            Assert.AreEqual(expectedResultCount, res.Length);

            Assert.AreEqual("AllwaysOnTop", res[0].OrganizationName);
            Assert.AreEqual("COMPUTERS, HARDWARE", res[1].OrganizationName);
            Assert.AreEqual("Test 1,", res[0].TestField);
            Assert.AreEqual("Test, 2", res[1].TestField);

            // cases where there is no blank row (or blank row was skipped)
            if (expectedResultCount != 2) {
                Assert.AreEqual("4S Consulting, Inc.", res[2].OrganizationName);
                Assert.AreEqual("SmartSolutions", res[3].OrganizationName);
                Assert.AreEqual(" Test 3", res[2].TestField);
                Assert.AreEqual("Test 4", res[3].TestField);
            }
        }

        private static ExcelXlsType[] ReadFromExcelStorage(string fileName, int stopAfterEmptyRows)
        {
            var provider = new ExcelStorage(typeof (ExcelXlsType));

            provider.FileName = TestCommon.GetPath("Excel", fileName);
            provider.SheetName = "Sheet1";
            provider.StartRow = 1;

            provider.ExcelReadStopAfterEmptyRows = stopAfterEmptyRows;

            return (ExcelXlsType[]) provider.ExtractRecords();
        }

        [DelimitedRecord("|")]
        public sealed class ExcelXlsType
        {
            public int Id;

            [FieldQuoted('"', QuoteMode.OptionalForBoth)]
            public string OrganizationName;

            [FieldQuoted('"', QuoteMode.OptionalForBoth)]
            public string TestField;
        }
    }
}

#endif