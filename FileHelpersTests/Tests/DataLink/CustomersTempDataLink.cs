#if ! MINI

using System;
using FileHelpers.DataLink;

namespace FileHelpersTests
{
	public class CustomersTempLinkProvider : AccessStorage
	{
		private string mAccessFileName = @"..\data\TestData.mdb";

		#region "  Constructors  "

		public CustomersTempLinkProvider()
		{
		}

		public CustomersTempLinkProvider(string fileName)
		{
			mAccessFileName = fileName;
		}

		#endregion

		#region "  RecordType  "

		public override Type RecordType
		{
			get { return typeof (CustomersVerticalBar); }
		}

		#endregion

		#region "  FillRecord  "

		protected override object FillRecord(object[] fields)
		{
			CustomersVerticalBar record = new CustomersVerticalBar();

			record.CustomerID = (string) fields[0];
			record.CompanyName = (string) fields[1];
			record.ContactName = (string) fields[2];
			record.ContactTitle = (string) fields[3];
			record.Address = (string) fields[4];
			record.City = (string) fields[5];
			record.Country = (string) fields[6];

			return record;
		}

		#endregion

		#region "  GetSelectSql  "

		protected override string GetSelectSql()
		{
			return "SELECT * FROM CustomersTemp";
		}

		#endregion

		#region "  GetInsertSql  "

		protected override string GetInsertSql(object record)
		{
			CustomersVerticalBar obj = (CustomersVerticalBar) record;

			return String.Format("INSERT INTO CustomersTemp (Address, City, CompanyName, ContactName, ContactTitle, Country, CustomerID) " +
				" VALUES ( \"{0}\" , \"{1}\" , \"{2}\" , \"{3}\" , \"{4}\" , \"{5}\" , \"{6}\"  ); ",
			                     obj.Address,
			                     obj.City,
			                     obj.CompanyName,
			                     obj.ContactName,
			                     obj.ContactTitle,
			                     obj.Country,
			                     obj.CustomerID
				);

		}

		#endregion

		public override string MdbFileName
		{
			get { return mAccessFileName; }
		}
	}
}

#endif