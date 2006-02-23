#if ! MINI

using System.Data.OleDb;
using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpersTests
{
	[TestFixture]
	public class DataLink
	{
		FileDataLink mLink;

		[Test]
		public void OrdersDbToFile()
		{
			mLink = new FileDataLink(new OrdersLinkProvider());
			mLink.ExtractToFile(@"..\data\temp.txt");
			int extractNum = mLink.LastExtractedRecords.Length;

			OrdersFixed[] records = (OrdersFixed[]) mLink.FileHelperEngine.ReadFile(@"..\data\temp.txt");

			Assert.AreEqual(extractNum, records.Length);
		}

		[Test]
		public void CustomersDbToFile()
		{
			mLink = new FileDataLink(new CustomersDataSotrage());
			mLink.ExtractToFile(@"..\data\temp.txt");
			int extractNum = mLink.LastExtractedRecords.Length;

			CustomersVerticalBar[] records = (CustomersVerticalBar[]) mLink.FileHelperEngine.ReadFile(@"..\data\temp.txt");

			Assert.AreEqual(extractNum, records.Length);
		}

		[Test]
		public void CustomersFileToDb()
		{
			mLink = new FileDataLink(new CustomersTempLinkProvider());
			ClearData(((AccessStorage) mLink.DataStorage).MdbFileName, "CustomersTemp");

			int count = Count(((AccessStorage) mLink.DataStorage).MdbFileName, "CustomersTemp");
			Assert.AreEqual(0, count);

			mLink.InsertFromFile(@"..\data\UpLoadCustomers.txt");

			count = Count(((AccessStorage) mLink.DataStorage).MdbFileName, "CustomersTemp");
			Assert.AreEqual(91, count);

			ClearData(((AccessStorage) mLink.DataStorage).MdbFileName, "CustomersTemp");
		}

		[Test]
		public void OrdersFileToDb()
		{
			mLink = new FileDataLink(new OrdersTempLinkProvider());
			ClearData(((AccessStorage) mLink.DataStorage).MdbFileName, "OrdersTemp");

			int count = Count(((AccessStorage) mLink.DataStorage).MdbFileName, "OrdersTemp");
			Assert.AreEqual(0, count);

			mLink.InsertFromFile(@"..\data\UpLoadOrders.txt");

			count = Count(((AccessStorage) mLink.DataStorage).MdbFileName, "OrdersTemp");
			Assert.AreEqual(830, count);

			ClearData(((AccessStorage) mLink.DataStorage).MdbFileName, "OrdersTemp");
		}

		private const string AccessConnStr = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Database Password=;Data Source=""<BASE>"";Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False";

		public void ClearData(string fileName, string table)
		{
			string conString = AccessConnStr.Replace("<BASE>", fileName);
			OleDbConnection conn = new OleDbConnection(conString);
			OleDbCommand cmd = new OleDbCommand("DELETE FROM " + table, conn);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
			int count = Count(((AccessStorage) mLink.DataStorage).MdbFileName, "OrdersTemp");
		}

		public int Count(string fileName, string table)
		{
			string conString = AccessConnStr.Replace("<BASE>", fileName);
			OleDbConnection conn = new OleDbConnection(conString);
			OleDbCommand cmd = new OleDbCommand("SELECT COUNT (*) FROM " + table, conn);
			conn.Open();
			int res = (int) cmd.ExecuteScalar();
			conn.Close();
			return res;
		}

	}
}

#endif