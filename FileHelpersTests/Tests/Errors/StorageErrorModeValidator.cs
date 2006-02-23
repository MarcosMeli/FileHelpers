using System.IO;
using FileHelpers;
using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpersTests.Errors
{
	[TestFixture]
	public class StorageErrorModeValidator
	{
		[Test]
		public void OneColumnIgnore()
		{
			ExcelStorage provider = new ExcelStorage(typeof (OneColumnType), 1, 1);
			provider.ErrorManager.ErrorMode = ErrorMode.IgnoreAndContinue;
			provider.FileName = @"..\data\Excel\OneColumnError.xls";

			object[] res = provider.ExtractRecords();

			Assert.AreEqual(36, res.Length);
			Assert.AreEqual(0, provider.ErrorManager.ErrorCount);
		}


		[Test]
		public void OneColumnSave()
		{
			ExcelStorage provider = new ExcelStorage(typeof (OneColumnType), 1, 1);
			provider.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
			provider.FileName = @"..\data\Excel\OneColumnError.xls";

			object[] res = provider.ExtractRecords();

			Assert.AreEqual(36, res.Length);
			Assert.AreEqual(4, provider.ErrorManager.ErrorCount);
			Assert.AreEqual(8, provider.ErrorManager.LastErrors[0].LineNumber);
			Assert.AreEqual(16, provider.ErrorManager.LastErrors[1].LineNumber);
			Assert.AreEqual(20, provider.ErrorManager.LastErrors[2].LineNumber);
			Assert.AreEqual(28, provider.ErrorManager.LastErrors[3].LineNumber);
		}
		
		
		[DelimitedRecord("|")]
			internal class OneColumnType
		{
			public int CustomerCode;
		}


	}

}
